package ru.nstu.networks.client.model;

import java.io.IOException;
import java.net.InetAddress;

public interface Model {
    void configure(InetAddress serverAddress, int port, String name);

    void start();

    boolean isRunning();

    String receiveMessage() throws IOException;

    void sendMessage(String message);

    void stop();
}