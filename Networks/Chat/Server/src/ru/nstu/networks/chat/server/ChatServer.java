package ru.nstu.networks.chat.server;

import ru.nstu.networks.chat.message.ErrorMessage;
import ru.nstu.networks.chat.message.InfoMessage;
import ru.nstu.networks.chat.user.Client;

import java.io.IOException;
import java.net.*;
import java.util.*;

public class ChatServer implements Server {
    private final int port;
    private final InetAddress INET_ADDRESS;
    private final List<Client> activeClients;
    private boolean isActive = true;

    public ChatServer(int port) {
        if (port < 1024 || port > 49151) {
            throw new IllegalArgumentException("Ports available to users from 1024 to 49151."
                    + " Current port: " + port);
        }

        System.out.println(new InfoMessage(null,
                ServerTime.getTime(), "Attempt to create a new server."));

        validatePort(port);

        this.port = port;
        INET_ADDRESS = getInetAddress();
        activeClients = new LinkedList<>();

        System.out.println(new InfoMessage(null,
                ServerTime.getTime(), "A new server has been created at " + INET_ADDRESS
                + ", assigned port: " + port));
    }

    @Override
    public void start() {
        try (ServerSocket serverSocket = new ServerSocket(port)) {
            System.out.println(new InfoMessage(null,
                    ServerTime.getTime(), "Server started at " + INET_ADDRESS
                    + ", port: " + serverSocket.getLocalPort()));

            while (isActive) {
                try {
                    Socket socket = serverSocket.accept();
                    new Client(socket, activeClients).start();
                } catch (IOException e) {
                    System.out.println(new ErrorMessage(null, ServerTime.getTime(),
                            "An I/O error occurred during connection establishment."));
                }
            }
        } catch (IOException e) {
            System.out.println(new ErrorMessage(null, ServerTime.getTime(),
                    "An I/O error occurred while opening a socket: " + e.getMessage()));
            System.exit(1);  //TODO: const
        }
    }

    @Override
    public void stop() {
        isActive = false;
        System.exit(0); //TODO: const
    }

    private static InetAddress getInetAddress() {
        try (DatagramSocket socket = new DatagramSocket()) {
            socket.connect(InetAddress.getByName("8.8.8.8"), 10000);
            return socket.getLocalAddress();
        } catch (Exception e) {
            System.out.println(new ErrorMessage(null,
                    ServerTime.getTime(), "Failed to get server inet-address."));
        }

        System.exit(5); //TODO: status
        return null;
    }

    private static void validatePort(int port) {
        if (port < 1024 || port > 49151) {
            throw new IllegalArgumentException(
                    "The port number must be in the range from 1024 to 49151. "
                            + "Current port number: " + port);
        }
    }
}