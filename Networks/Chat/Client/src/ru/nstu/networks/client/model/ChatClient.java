package ru.nstu.networks.client.model;

import java.io.*;
import java.net.InetAddress;
import java.net.Socket;

public class ChatClient implements Model {
    private String username;
    private InetAddress serverAddress;
    private int port;

    private boolean isRunning;
    private boolean isConfigured;

    private BufferedReader in;
    private PrintWriter out;

    public ChatClient() {
    }

    public void setUsername(String username) {
        if (username == null) {
            throw new NullPointerException("Error: client username must be specified");
        }

        this.username = username;
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
        if (isRunning) {
            throw new RuntimeException("Error: it is forbidden to reconfigure a running model");
        }

        setServerAddress(serverAddress);
        setPort(port);
        setUsername(name);
        isConfigured = true;
    }

    @Override
    public void start() {
        if (isRunning) {
            throw new RuntimeException("Error: re-running the model is prohibited");
        }

        if (!isConfigured) {
            throw new RuntimeException("Error: the model is not configured");
        }

        try (Socket socket = new Socket(serverAddress, port)) {
            try (BufferedReader in = new BufferedReader(
                    new InputStreamReader(socket.getInputStream()));
                 PrintWriter out = new PrintWriter(
                         socket.getOutputStream(), true)) {
                this.in = in;
                this.out = out;
                out.println(username);

                isRunning = true;

                while (isRunning) {
                    try {
                        Thread.sleep(10000);
                    } catch (InterruptedException e) {
                        System.out.println(e.getMessage());
                        System.exit(7);
                    }
                }
            } catch (IOException e) {
                System.out.println("An error occurred while creating an I/O stream: "
                        + e.getMessage());
                System.exit(4); //TODO: create a constant
            }
        } catch (IOException e) {
            System.out.println("An I/O error occurred while creating the socket: "
                    + e.getMessage());
            System.exit(3); //TODO: create a constant
        }
    }

    @Override
    public boolean isRunning() {
        return isRunning;
    }

    @Override
    public String receiveMessage() throws IOException {
        validateForIllegalCall();

        try {
            return in.readLine();
        } catch (IOException e) {
            System.out.println("An error occurred while reading from the socket. "
                    + e.getMessage());
            throw e;
        }
    }

    @Override
    public void sendMessage(String message) {
        validateForIllegalCall();
        out.println(message);
    }

    @Override
    public void stop() {
        validateForIllegalCall();
        isRunning = false;
    }

    private void validateForIllegalCall() {
        if (!isRunning) {
            throw new RuntimeException("Calling the method is prohibited: the model is not running");
        }
    }
}