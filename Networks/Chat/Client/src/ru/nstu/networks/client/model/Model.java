package ru.nstu.networks.client.model;

import java.net.InetAddress;

public interface Model {
    void configure(InetAddress serverAddress, int port, String name);

    void start();

    void stop();
}