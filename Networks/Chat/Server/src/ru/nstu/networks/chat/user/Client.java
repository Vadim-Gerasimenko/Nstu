package ru.nstu.networks.chat.user;

import ru.nstu.networks.chat.message.InfoMessage;
import ru.nstu.networks.chat.message.Message;
import ru.nstu.networks.chat.message.UserMessage;
import ru.nstu.networks.chat.server.ServerTime;
import ru.nstu.networks.chat.message.WarningMessage;

import java.io.*;
import java.net.InetAddress;
import java.net.Socket;
import java.net.SocketException;
import java.util.List;

public class Client extends Thread implements User {
    private String name;
    private final Socket socket;
    private final List<Client> activeClients;

    public Client(Socket socket, List<Client> activeClients) {
        this.socket = socket;
        this.activeClients = activeClients;
    }

    @Override
    public Socket getSocket() {
        return socket;
    }

    @Override
    public InetAddress getAddress() {
        return socket.getInetAddress();
    }

    @Override
    public void run() {
        activeClients.add(this);

        try (BufferedReader in = new BufferedReader(
                new InputStreamReader(socket.getInputStream()))) {
            name = in.readLine();
            Message connectionMessage = new InfoMessage(null, ServerTime.getTime(),
                    "User \"" + name + "\" from " + socket.getRemoteSocketAddress() + " connected.");

            System.out.println(connectionMessage);
            sendMessage(connectionMessage);

            try {
                String line;

                while ((line = in.readLine()) != null) {
                    Message message = new UserMessage(this, ServerTime.getTime(), line);

                    System.out.println(message);
                    sendMessage(message);
                }
            } catch (SocketException e) {
                Message disconnectionMessage = new InfoMessage(null, ServerTime.getTime(),
                        "User \"" + name + "\" from " + socket.getRemoteSocketAddress() + " disconnected.");

                System.out.println(disconnectionMessage);
                sendMessage(disconnectionMessage);
            } finally {
                socket.close();
                activeClients.remove(this);
            }
        } catch (IOException e) {
            System.out.println(new WarningMessage(null, ServerTime.getTime(),
                    "An error occurred while creating an I/O stream: " + e.getMessage()));
        }
    }

    @Override
    public void sendMessage(Message message) {
        synchronized (activeClients) {
            for (Client client : activeClients) {
                try {
                    PrintWriter out = new PrintWriter(client.getSocket().getOutputStream(), true);
                    out.println(message);
                } catch (IOException e) {
                    System.out.println(new WarningMessage(null, ServerTime.getTime(),
                            "An error occurred while creating an I/O stream: " + e.getMessage()));
                }
            }
        }
    }

    @Override
    public String toString() {
        return name + " from " + getAddress();
    }
}