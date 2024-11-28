package ru.nstu.networks.chat.user;

import ru.nstu.networks.chat.message.Message;

import java.net.InetAddress;
import java.net.Socket;

public interface User {
    Socket getSocket();

    InetAddress getAddress();

    void sendMessage(Message message);
}