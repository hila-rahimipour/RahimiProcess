Created by Matan Weinman
February 2022

REQUIRED FILES: server.py, client.py
OPTIONAL FILES: port_server.py
REQUIRED PYTHON LIBRARIES: socket, select, _thread, tkinter, re, random, time, datetime (They are all built-in)

Run the files.
First, in the server console, enter the server's admin password. If the port server is active, you will be optionally prompted for a server name.
Then, in the client window, you can choose whether to connect by manually typing the server IP and port, or to connect via a server name.
Connecting by a server name will not work unless the port server is active.

After establishing a connection, type a message to the server in the entry field and click the 'send' button. Alternatively, you can press Enter.
You will '>>' appear in the window, followed by your message.
The server or other clients' messages will begin with '<NAME> says:'.

There is a 1 in a 1000 chance that the server will respond in binary. If you have received it, lucky you!