"""
An echo server with special commands.
Author: Matan Weinman
"""
import socket as s
import _thread as t
import re, select, time, random
import datetime as dt


def find_free_port():
    """
    Finds a free port.
    :return: The free port number.
    """
    test_socket = s.socket(s.AF_INET, s.SOCK_STREAM)
    test_socket.bind(('', 0))
    port = test_socket.getsockname()[1]
    test_socket.close()
    return port


ADDR = s.gethostbyname(s.gethostname())
PORT = find_free_port()
PORT_SERVER_ADDR = (s.gethostbyname(s.gethostname()), 55555)  # Change this if the port server is on a different computer!
BUFFER = 1024
QUOTES = {
    'hello there': 'General Kenobi!',
    'i am your father': "That's not true... That's impossible!",
    'event': 'Delegate!',
    'shut up': "That's not קהילתי!",
    'idan': 'Idan Gur!!!!'
}

is_running = True
name = ''
password = ''
messages = {}
recv = {}
names = {None: 'SERVER'}

read_sockets = []
write_sockets = []
chat_users = []


def valid_input(text, valid_lst=None, valid_re=None):
    """
    Makes sure a valid input is entered.
    :param text: The text to be printed during input.
    :param valid_lst: A list, containing possible inputs.
    :param valid_re: A regular expression, describing a correct input.
    :return: None if no list or regex were given, else a valid input.
    """
    var = input(text)
    if valid_lst is not None:
        while var not in valid_lst:
            print('The input is not a valid answer.')
            var = input(text)
    elif valid_re is not None:
        while not valid_re.findall(var):
            print('The input is not valid.')
            var = input(text)
    else:
        return None
    return var


def shutdown(server_socket):
    """
    Shuts down the server.
    :param server_socket: The server socket.
    """
    global is_running, name

    is_running = False
    print('Server is shutting down.')
    port_server_socket = s.socket(s.AF_INET, s.SOCK_STREAM)
    try:
        port_server_socket.connect(PORT_SERVER_ADDR)
    except s.error:
        pass  # The server is closed, so I don't have to remove the address from it.
    else:
        port_server_socket.send(('del:' + name).encode())
        port_server_socket.recv(BUFFER)
    port_server_socket.close()
    server_socket.close()
    print('Server closed.')


def send_timeout(client):
    """
    A function called from a separate thread for each client, and sends him a timeout message if he hasn't sent a
    message in a certain, increasing period of time.
    :param client: The client socket.
    """
    global is_running, recv, chat_users

    sleep = 10
    while is_running and client in messages.keys():
        start = time.time()
        while is_running and time.time() - start <= sleep:
            if client in messages.keys() and recv[client]:
                recv[client] = False
                start = time.time()
                continue
        if client in messages.keys() and not recv[client] and client not in chat_users:
            sleep += 10
            messages[client].append('timeout'.encode())


def close_client(sock):
    """
    Disconnects a client and destroys all evidence to his existence.
    :param sock: The client socket.
    """
    global messages, recv, names, read_sockets, write_sockets, chat_users
    del messages[sock]
    del recv[sock]
    del names[sock]
    if sock in chat_users: chat_users.remove(sock)
    read_sockets.remove(sock)
    write_sockets.remove(sock)
    sock.close()


def chat(client_socket, message):
    """
    Sends a message to all chat users.
    :param client_socket: The sender socket.
    :param message: The message sent.
    """
    global chat_users, messages
    for sock in chat_users:
        if sock != client_socket:
            messages[sock].append(('%s: %s' % (names[client_socket], message)).encode())


def communicate(server_socket, client_socket, data):
    """
    Handles commands and receiving of messages.
    :param server_socket: The server socket.
    :param client_socket: The client socket.
    :param data: The data received.
    """
    global name, password, messages, read_sockets, write_sockets, QUOTES, chat_users, names

    if client_socket in chat_users:
        if data == 'exit':
            chat_users.remove(client_socket)
            print(names[client_socket] + ' has left the chat. (%s online)' % len(chat_users))
            chat(None, ('There are %s clients online.' % (len(chat_users) - 1)))
        else: chat(client_socket, data)
        return

    lower_data = data.lower()

    if re.findall(r'^quit:[\w\d]+$', lower_data):  # Closing the server.
        select.select([], [client_socket], [])
        if data.split(':')[1] == password:
            close_client(client_socket)
            shutdown(server_socket)
            quit()
        else:
            messages[client_socket].append('The password was incorrect.'.encode())
        return

    elif lower_data == 'exit' or not data:  # Leaving the server.
        print('Sadly, ' + names[client_socket] + ' has disconnected.')
        close_client(client_socket)
        return

    elif re.findall(r'^change:[\w\d]+:[\w\d]+$', lower_data):  # Changing the server's admin password.
        if data.split(':')[1] == password:
            new_pass = data.split(':')[2]
            password = new_pass
            messages[client_socket].append('Password changed.'.encode())
        else:
            messages[client_socket].append('Wrong password.'.encode())
        return

    elif lower_data == 'chat':  # Joining a chat.
        chat(None, ('There are %s clients online.' % len(chat_users)))
        chat_users.append(client_socket)
        print(names[client_socket] + ' has joined the chat! (%s online)' % len(chat_users))
        messages[client_socket].append(('There are %s clients online.' % (len(chat_users) - 1)).encode())
        return

    elif re.findall(r'^name:[\w\d]+([\w\d\-_ ]+)?$', lower_data):  # Changing the client's name.
        new_name = data.split(':')[1]
        if new_name in names.values():
            messages[client_socket].append('The name you chose is already in use.'.encode())
        else:
            print("'%s' will now be called '%s'." % (names[client_socket], new_name))
            names[client_socket] = new_name
            messages[client_socket].append('Your name has been changed!'.encode())
        return

    elif lower_data == 'connected':  # Showing a list of all connected users.
        messages[client_socket].append(str([i for i in names.values() if i != 'SERVER']).encode())
        print("Sent info about connected devices to '%s'." % names[client_socket])
        return

    print(names[client_socket] + ' says: ' + data)

    if lower_data.startswith('broadcast '):  # Broadcasting to all connected clients.
        for sock in [i for i in messages.keys() if i != client_socket]:
            messages[sock].append((("BROADCAST: '%s': " % names[client_socket]) + ' '.join(data.split(' ')[1:])).encode())
    elif lower_data.strip(' .,!?') in QUOTES.keys():  # Special messages!
        messages[client_socket].append(QUOTES[lower_data.strip(' .,!?')].encode())
    # Even more special messages!
    elif lower_data.startswith('nslookup'):
        messages[client_socket].append('Do I look like a DNS server to you?!'.encode())
    elif lower_data == 'time':  # Displaying the server's time.
        messages[client_socket].append(str(dt.datetime.now().strftime("%d-%m-%Y %H:%M:%S")).encode())
    else:  # Echo server.
        messages[client_socket].append(data.encode())


def update_port_server():
    """
    Adds the server name and address to the port server.
    """
    global name

    port_server_socket = s.socket(s.AF_INET, s.SOCK_STREAM)
    try:
        port_server_socket.connect(PORT_SERVER_ADDR)
    except s.error:
        print('The port server did not respond.')
    else:
        while True:
            name = valid_input('Please enter the server name: ', valid_re=re.compile(r'^[\w\d]+[\w\d _-]?$'))
            select.select([], [port_server_socket], [])
            port_server_socket.send(':'.join([str(i) for i in ('add', name, ADDR, PORT)]).encode())
            select.select([port_server_socket], [], [])
            try:
                data = port_server_socket.recv(BUFFER).decode()
            except s.error:
                print('The port server did not respond.')
                break
            else:
                if not data:
                    print('The port server did not respond.')
                    break
                if data == 'used':
                    print("The name '" + name + "' is already in use. Please pick a different name.")
                elif data == 'no':
                    print('There was an error. Please try again.')
                elif data == 'ok':
                    print("The server is now listed on the port server under the name '" + name + "'.")
                    break

    port_server_socket.close()


def main():
    """
    The main function.
    """
    global password, messages, names, read_sockets, write_sockets, recv

    password = valid_input('Please enter the admin password: ', valid_re=re.compile(r'^[\w\d]+$'))
    print('The server IP address is ' + ADDR + ' and the port number is ' + str(PORT) + '.')

    enter_name = valid_input('Would you like to add the server to the port server? [y/n] ', valid_lst=['y', 'n'])
    if enter_name == 'y':
        update_port_server()

    server_socket = s.socket(s.AF_INET, s.SOCK_STREAM)
    server_socket.bind((ADDR, PORT))
    server_socket.listen(3)
    read_sockets.append(server_socket)
    print('Listening for clients...')

    while is_running:
        readable, writeable,_ = select.select(read_sockets, write_sockets, [])
        for socket in readable:
            if socket == server_socket:
                try: sock, addr = server_socket.accept()
                except s.error: quit()
                print('Connected to ' + str(addr) + '.')
                messages[sock] = []
                names[sock] = str(addr)
                recv[sock] = False
                t.start_new_thread(send_timeout, (sock,))
                read_sockets.append(sock)
                write_sockets.append(sock)
            else:
                data = ''
                next_sock = False
                while True:
                    try:
                        new_data = socket.recv(BUFFER)
                    except s.error:
                        print('Sadly, ' + names[socket] + ' has disconnected.')
                        close_client(socket)
                        next_sock = True
                        break
                    data += new_data.decode()
                    if len(new_data) < BUFFER: break
                    # In case there was a single message in the length of the buffer size:
                    if sock.fileno() != -1 and not select.select([sock], [], [], 0.2)[0]: break
                if next_sock: continue
                communicate(server_socket, socket, data)
                recv[socket] = True
        for socket in writeable:
            if socket in messages and messages[socket]:
                mes = messages[socket][0]
                # 1 in a 1000 chance the server will respond in binary!
                if socket not in chat_users and random.randint(1, 1000) == 42:  # The meaning of life!
                    mes = ''.join(format(i, 'b') for i in mes).encode()
                socket.send(mes)
                del mes
                if messages[socket]:
                    messages[socket] = messages[socket][1:]


if __name__ == '__main__':
    main()
