package ru.nstu.networks.client.model;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;

public class ChatClient implements Model {
    private String name;
    private InetAddress serverAddress;
    private int port;

    public ChatClient() {
    }

    public void setName(String name) {
        if (name == null) {
            throw new NullPointerException("Error: client name must be specified");
        }

        this.name = name;
    }

    private void setServerAddress(InetAddress serverAddress) {
        if (serverAddress == null) {
            throw new NullPointerException("Error: server address cannot refer to null");
        }

        this.serverAddress = serverAddress;
    }

    private void setPort(int port) {
        if (port < 1024 || port > 49151) {
            throw new IllegalArgumentException("Ports available to users from 1024 to 49151."
                    + " Current port: " + port);
        }

        this.port = port;
    }

    @Override
    public void configure(InetAddress serverAddress, int port, String name) {
        setServerAddress(serverAddress);
        setPort(port);
        setName(name);
    }

    @Override
    public void start() {
        try (Socket socket = new Socket(serverAddress, port)) {
            try (PrintWriter out = new PrintWriter(
                    socket.getOutputStream(), true);
                 BufferedReader in = new BufferedReader(
                         new InputStreamReader(socket.getInputStream()));
                 BufferedReader stdIn = new BufferedReader(
                         new InputStreamReader(System.in))) {
                out.println(name);

                Thread listener = new Thread(() -> {
                    while (true) {
                        try {
                            String message = in.readLine();

                            if (message == null) {
                                throw new RuntimeException("The server is down"); //TODO: create Exception
                            }

                            System.out.println(message);
                        } catch (IOException e) {
                            System.out.println("An I/O error occurred: " + e.getMessage());
                        }
                    }
                });

                listener.start();

                String line;

                while ((line = stdIn.readLine()) != null) {
                    out.println(line);
                }
            } catch (IOException e) {
                System.out.println("An error occurred while creating an I/O stream: "
                        + e.getMessage());
                System.exit(4); //TODO: create constant
            }
        } catch (IOException e) {
            System.out.println("An I/O error occurred while creating the socket: "
                    + e.getMessage());
            System.exit(3); //TODO: create constant
        }
    }

    @Override
    public void stop() {
        System.exit(0);
    }
}