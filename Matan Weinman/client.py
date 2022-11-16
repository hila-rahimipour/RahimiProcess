"""
A client to communicate with the server.
Author: Matan Weinman.
"""

import socket as s
import _thread as t
import re, random, select, time, _thread
import tkinter as tk

BUFFER = 1024

TIMEOUT_RESPONSES = [
        'Hey! Are you still here?',
        'Is anyone there?',
        "I'm still waiting!",
        "I don't have all day..."
    ]

root = None

messages = []
is_running = True
chat = False
prev_mes = ''


def clear_screen():
    """
    Clears the GUI from all widgets.
    """
    global root
    for i in root.winfo_children():
        i.destroy()


def get_server_info(name, addr):
    """
    Gets a server name, and tries to get its data from the port server.
    :param name: The server name.
    :param addr: The port server's address.
    :return: A tuple containing the server address, or a string describing the error.
    """
    port_server_client = s.socket(s.AF_INET, s.SOCK_STREAM)

    try: port_server_client.connect(addr)
    except s.error: return 'The port server did not respond.'

    select.select([], [port_server_client], [])
    port_server_client.send(('get:' + name).encode())
    select.select([port_server_client], [], [])
    data = port_server_client.recv(BUFFER).decode()

    if not data:
        return 'The port server did not respond.'
    if data == 'no':
        return "The name '" + name + "' doesn't exist in the server."
    else:
        port_server_client.close()
        return eval(data)


def manual_connect_screen():
    """
    Lets a user enter the server IP and port manually.
    """
    global root
    clear_screen()
    tk.Label(root, text='Please enter the server IP and port:').grid(padx=10, pady=8, sticky='NW')
    ip = tk.Entry(root, width=30)
    port = tk.Entry(root, width=30)
    tk.Label(root, text='Server IP: ').grid(sticky='NW', row=1, column=0, padx=10)
    ip.grid(padx=80, sticky='NW', row=1, column=0)
    tk.Label(root, text='Server port: ').grid(sticky='NW', row=2, column=0, pady=5, padx=10)
    port.grid(padx=80, pady=5, sticky='NW', row=2, column=0)
    ip.focus_set()
    output = tk.Label(root, foreground='red')
    btn = tk.Button(root, text='Connect', command=lambda: check_manual_format(ip.get(), port.get(), output))
    btn.grid(pady=8, padx=10, sticky='NW', row=5, column=0)
    root.bind('<Return>', btn['command'])
    tk.Button(root, text='Enter server name', command=lambda: pick_name_screen(output)).grid(sticky='NW', row=5, column=0,
                                                    padx=160, pady=8)
    output.grid(padx=5, sticky='NW')


def pick_name_screen(output):
    """
    Generates a screen to input the server name.
    :param output: An output tkinter Label.
    """
    global root

    clear_screen()

    tk.Label(root, text='Please enter the port server IP:').grid(sticky='NW', padx=10, pady=10)
    ip = tk.Entry(root, width=30)
    port = tk.Entry(root, width=30)
    port.insert(0, '55555')
    port['state'] = tk.DISABLED
    tk.Label(root, text='Port Server IP: ').grid(sticky='NW', row=1, column=0, padx=10)
    ip.grid(padx=110, sticky='NW', row=1, column=0)
    tk.Label(root, text='Port Server port: ').grid(sticky='NW', row=2, column=0, pady=5, padx=10)
    port.grid(padx=110, pady=5, sticky='NW', row=2, column=0)
    ip.focus_set()

    tk.Label(root, text='Please enter the server name:').grid(sticky='NW', padx=10, pady=7)
    name = tk.Entry(root, width=35)
    name.grid(sticky='NW', padx=10)
    output = tk.Label(root, foreground='red')
    btn = tk.Button(root, text='Connect', command=lambda: check_name_format([name.get(), ip.get(), port.get()], output))
    btn.grid(sticky='NW', pady=10, padx=10, row=7, column=0)
    root.bind('<Return>', btn['command'])
    tk.Button(root, text='Enter IP and port manually', command=manual_connect_screen).grid(sticky='NW', row=7, column=0,
                                                                                           padx=74, pady=10)
    output.grid(sticky='NW', padx=10)


def check_name_format(value_lst, output):
    """
    Checks if the data from the name pick screen is properly formatted, and if it is tries connection.
    :param value_lst: A list containing the server name and the port server's IP address and port, in this order.
    :param output: A tkinter Label, used to display error messages.
    """
    name, ip, port = value_lst

    if not re.findall(r'^([1-2]?\d{1,2}.){3}[1-2]?\d{1,2}$', ip) or not (port.isdigit() and 0 < int(port) < 65536):
        output['text'] = "The port server's address is not in the correct format."
    else:
        if re.findall(r'^[\w\d]+([\w\d\-_ ]+)?$', name): connect_to_server(port_addr=(ip, int(port)), name=name, output=output)
        else: output['text'] = 'The name is not in the correct format.'


def check_manual_format(ip, port, output):
    """
    Checks if the data from the manual address entry screen is properly formatted, and if it is tries connection.
    :param ip: The server IP.
    :param port: The server port.
    :param output: A tkinter Label, used to display error messages.
    """
    if not re.findall(r'^([1-2]?\d{1,2}.){3}[1-2]?\d{1,2}$', ip) or not (port.isdigit() and 0 < int(port) < 65536):
        output['text'] = 'The address is not in the correct format.'
    else: connect_to_server(addr=(ip, port), output=output)


def connect_to_server(port_addr=None, name=None, addr=None, output=None):
    """
    Connecting to a server, whether by a name and the port server or an explicit address.
    :param port_addr: The port server's address.
    :param name: The server name.
    :param addr: The server address.
    :param output: The tkinter Label used for outputting errors.
    """
    client_socket = s.socket(s.AF_INET, s.SOCK_STREAM)

    if addr is None:
        addr = get_server_info(name, port_addr)
        if type(addr) == str:
            if "doesn't exist in the server" in addr: output['text'] = addr
            # The only other possible outcome is that the port server isn't active.
            else: output['text'] = addr
        else:
            try: client_socket.connect(addr)
            except s.error: output['text'] = 'Could not connect to the server.'
            else: communication_screen(client_socket)
    else:
        if not re.findall(r'^([1-2]?\d{1,2}.){3}[1-2]?\d{1,2}$', addr[0]) \
                or not (addr[1].isdigit() and 0 < int(addr[1]) < 65536):
            output['text'] = 'The input is not in the correct format.'
        else:
            try: client_socket.connect((addr[0], int(addr[1])))
            except s.error: output['text'] = 'Could not connect to the server.'
            else: communication_screen(client_socket)


def communication_screen(client_socket):
    """
    Shows the communication screen with the server.
    :param client_socket: The client socket.
    """
    global root

    clear_screen()
    # Where the communication is shown.
    frame1 = tk.Frame()
    lst = tk.Listbox(frame1, width=50, selectforeground='black', selectbackground='#dedede', activestyle='none')
    scrollbar = tk.Scrollbar(frame1, command=lst.yview)
    horizontal_scrollbar = tk.Scrollbar(frame1, orient='horizontal', command=lst.xview)
    lst.config(yscrollcommand=scrollbar.set, xscrollcommand=horizontal_scrollbar.set)
    horizontal_scrollbar.pack(side=tk.BOTTOM, fill='x')
    lst.pack(side=tk.LEFT, fill='both', expand=True)
    root.bind('<Control-c>', lambda x: copy_selection(lst))  # Press Control C to copy the selected line.
    scrollbar.pack(side=tk.RIGHT, fill='y')
    frame1.pack(side=tk.TOP, padx=5, pady=5, fill='both', expand=True)

    t.start_new_thread(communicate, (client_socket, lst))  # Creates a new thread to receive and send messages.

    # Where the entry and send button are.
    data = tk.StringVar()
    frame2 = tk.Frame()
    to_send = tk.Entry(frame2, width=50, textvariable=data)
    to_send.pack(side=tk.LEFT, padx=5)
    to_send.focus_set()
    to_send.bind('<Up>', lambda x: set_prev_mes(data, to_send))
    btn = tk.Button(frame2, text='send', command=lambda: manage_write(data, client_socket, lst))
    btn.pack(side=tk.RIGHT, padx=5)
    frame2.pack(side=tk.TOP, pady=8, padx=4)
    root.bind('<Return>', lambda x: manage_write(data, client_socket, lst))


def set_prev_mes(var, entry):
    """
    Sets the entry value to the previous message sent.
    :param var: The entry's StringVar.
    :param entry: The entry object.
    """
    global prev_mes
    var.set(prev_mes)
    entry.icursor(len(prev_mes))


def copy_selection(lst):
    """
    Used to copy the highlighted value's data in the communication.
    :param lst: The listbox object.
    """
    global root
    if not lst.curselection(): return
    raw = lst.get(lst.curselection()[0])
    if raw.startswith('>> '): raw = raw[3:]
    elif raw.startswith('Server says: '):
        raw = raw[len('Server says: '):]
    elif re.findall(r"^BROADCAST: '[\w\d]+([\w\d\-_ ]+)?':", raw) or\
        re.findall(r"^BROADCAST: '\('([1-2]?\d{1,2}.){3}[1-2]?\d{1,2}', \d{1,5}\)':", raw):
        raw = ':'.join(raw.split(':')[2:]).strip()
    elif re.findall(r"^[\w\d]+([\w\d\-_ ]+)?:", raw) or \
         re.findall(r"^\('([1-2]?\d{1,2}.){3}[1-2]?\d{1,2}', \d{1,5}\):", raw):
        raw = ':'.join(raw.split(':')[1:]).strip()
    root.clipboard_clear()
    root.clipboard_append(raw)
    root.update()


def send(var):
    """
    Sends a message, and updates the tkinter listbox.
    :param var: The text var in the entry.
    """
    global messages
    if not is_running: quit()
    msg = var.get()
    messages.append(msg.encode())
    var.set('')


def manage_write(var, sock, lst):
    """
    Changes the inputted message to comply with the server commands' structure, then calls the 'send' method.
    :param var: The entry text variable.
    :param sock: The server socket.
    :param lst: The listbox to be updated.
    """
    global is_running, root, chat, prev_mes
    data = var.get().strip()
    var.set(data)
    lower_data = data.lower()

    if not data:
        if not is_running: send(var)
        lst.insert(tk.END, '>>')
        lst.see(tk.END)
        return

    prev_mes = data

    if chat and is_running:
        if data.lower() == 'exit':
            # Leaving the chat:
            chat = False
            lst.insert(tk.END, 'You have left the chat.')
            lst.see(tk.END)
            messages.append('exit'.encode())
            var.set('')
            return
        else: send(var)

    elif data.lower() == 'exit':
        is_running = False
        sock.close()
        root.destroy()
        quit()

    else:
        if lower_data == 'quit':
            # Opens a popup window to enter the server password:
            popup = tk.Toplevel()
            popup.title('quit')
            popup.wm_geometry('370x30+%s+%s' % (root.winfo_x() + 30, root.winfo_y() + 20))
            tk.Label(popup, text="Enter the server's admin password:").pack(side=tk.LEFT)
            p1 = tk.StringVar()
            password = tk.Entry(popup, textvariable=p1)
            password.pack(side=tk.LEFT)
            password.focus_set()
            btn = tk.Button(popup, text='send', command=lambda: send_quit(var, p1.get(), popup))
            btn.pack(side=tk.LEFT, padx=10)
            popup.bind('<Return>', btn['command'])

        elif lower_data == 'change':
            # Opens a popup window to enter the server password and the new one:
            popup = tk.Toplevel()
            popup.title('change')
            popup.wm_geometry('320x80+%s+%s' % (root.winfo_x() + 30, root.winfo_y() + 20))
            tk.Label(popup, text="Enter the server's admin password:").grid(sticky='NW', row=0, column=0)
            p1 = tk.StringVar()
            password = tk.Entry(popup, textvariable=p1)
            password.grid(sticky='NW', row=0, column=1)
            password.focus_set()
            tk.Label(popup, text="Enter the new password:").grid(sticky='NW', row=1, column=0)
            p2 = tk.StringVar()
            new_password = tk.Entry(popup, textvariable=p2)
            new_password.grid(sticky='NW', row=1, column=1)
            btn = tk.Button(popup, text='send', command=lambda: send_change(var, p1.get(), p2.get(), popup))
            btn.grid(sticky='NW', padx=10, pady=5)
            popup.bind('<Return>', btn['command'])

        elif lower_data == 'chat':
            # Entering a chat:
            chat = True
            send(var)
            lst.insert(tk.END, 'You have joined the chat!')
            lst.see(tk.END)

        elif lower_data == 'name':
            # Opening a popup window to change your name in the server:
            popup = tk.Toplevel()
            popup.title('name')
            popup.wm_geometry('305x30+%s+%s' % (root.winfo_x() + 30, root.winfo_y() + 20))
            tk.Label(popup, text="Enter your new name:").pack(side=tk.LEFT, padx=3)
            n = tk.StringVar()
            name = tk.Entry(popup, textvariable=n)
            name.pack(side=tk.LEFT)
            name.focus_set()
            btn = tk.Button(popup, text='send', command=lambda: send_name(var, lst, n, popup))
            btn.pack(side=tk.LEFT, padx=10)
            popup.bind('<Return>', btn['command'])

        elif lower_data.strip().startswith('name') and lower_data.strip() != 'name':
            var.set(' '.join(data.split(' ')[1:]))
            send_name(var, lst, var)

        else: send(var)


def send_quit(var, password, popup):
    """
    Sending a properly structured 'quit' message.
    :param var: The main entry variable.
    :param password: The entered password.
    :param popup: The popup window.
    """
    popup.destroy()
    if not password: return
    var.set('quit:%s' % password)
    send(var)


def send_change(var, password, new_password, popup):
    """
    Sending a properly structured 'change' message.
    :param var: The main entry variable.
    :param password: The inputted password.
    :param new_password: The new password.
    :param popup: The popup window.
    """
    popup.destroy()
    if not password or not new_password: return
    var.set('change:%s:%s' % (password, new_password))
    send(var)


def send_name(var, lst, name_var, popup=None):
    """
    Sending a properly structured 'name' message.
    :param var: The main entry variable.
    :param name_var: The name entry text variable.
    :param lst: The communication listbox.
    :param popup: The popup window (optional).
    """
    if popup is not None: popup.destroy()
    if not name_var.get(): return
    if not re.search(r'^[\w\d]+([\w\d\-_ ]+)?$', name_var.get()):
        lst.insert(tk.END, 'That name is invalid. A name can only contain letters,'
                           ' digits, spaces, underscores or hyphens.')
        lst.see(tk.END)
        name_var.set('')
    else:
        var.set('name:%s' % name_var.get())
        send(var)


def manage_read(data, sock, lst):
    """
    Checks incoming messages, and alters them if necessary.
    :param data: The data received.
    :param sock: The client socket.
    :param lst: The communication listbox.
    :return: The altered data,
    """
    global is_running, root, chat
    if not data:  # The server is offline.
        lst.insert(tk.END, 'The server was closed. Press <ENTER> to close the program.')
        lst.see(tk.END)
        is_running = False
        sock.close()
    elif chat:
        return data
    else:
        if data == 'timeout':
            data = random.choice(TIMEOUT_RESPONSES)
        # Checks if a broadcast message was sent (The weird regexes are names- Either an address or
        # a user-selected name).
        elif re.findall("^BROADCAST: '[\w\d]+([\w\d\-_ ]+)?':", data) or re.findall("^BROADCAST: '\("
                        "'([1-2]?\d{1,2}.){3}[1-2]?\d{1,2}', \d{1,5}\)':", data):
            return data
        return 'Server says: %s' % data


def communicate(sock, lst):
    """
    Read and send messages from/to the server.
    :param sock: The client socket.
    :param lst: The communication listbox.
    """
    global messages, is_running

    while is_running:
        if sock.fileno() == -1: break
        readable, writeable, _ = select.select([sock], [sock], [])
        if not is_running: break
        if readable:
            data = ''
            while True:
                try:
                    new_data = sock.recv(BUFFER)
                except s.error:
                    lst.insert(tk.END, 'The server was closed. Press <ENTER> to close the program.')
                    lst.see(tk.END)
                    is_running = False
                    return
                data += new_data.decode()
                if len(data) < BUFFER: break
                # In case there was a single message in the length of the buffer size:
                if sock.fileno() != -1 and not select.select([sock], [], [], 0.2)[0]: break
            data = manage_read(data, sock, lst)
            lst.insert(tk.END, data)
            lst.see(tk.END)
        if writeable and messages:
            msg = messages[0]
            sock.send(msg)
            msg = msg.decode()
            if re.findall(r'^quit:[\w\d]+$', msg): msg = 'Shut down the server.'
            elif re.findall(r'^change:[\w\d]+:[\w\d]+$', msg): msg = 'Change password to %s.' % msg.split(':')[2]
            elif msg.startswith('name:'): msg = 'Change my name to %s.' % ':'.join(msg.split(':')[1:])
            lst.insert(tk.END, '>> %s' % msg)
            lst.see(tk.END)
            if messages:
                messages = messages[1:]


def main():
    """
    The main function.
    """
    global root
    root = tk.Tk()
    root.title('Client')
    root.minsize(380, 250)
    manual_connect_screen()
    root.wm_geometry('380x250')
    root.mainloop()


if __name__ == '__main__':
    main()