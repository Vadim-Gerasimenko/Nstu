package ru.nstu.networks.chat;

import ru.nstu.networks.chat.server.ChatServer;
import ru.nstu.networks.chat.server.Server;

public class Main {
    public static void main(String[] args) {
        Server server = new ChatServer(2009);
        server.start();
    }
}