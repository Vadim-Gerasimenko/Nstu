package ru.nstu.networks.client.view;

import ru.nstu.networks.client.controller.Controller;

public interface View {
    void start();

    void setController(Controller controller);
}