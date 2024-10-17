package ru.nstu.networks.client.controller;

import ru.nstu.networks.client.model.Model;
import ru.nstu.networks.client.view.View;

import java.net.InetAddress;

public class Controller {
    private final Model model;
    private final View view;

    public Controller(Model model, View view) {
        this.model = model;
        this.view = view;
    }

    public void startModel(InetAddress serverAddress, int port, String username) {
        model.configure(serverAddress, port, username);
        model.start();
    }

    public void stopModel() {
        model.stop();
    }
}