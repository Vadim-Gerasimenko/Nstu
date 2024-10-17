package ru.nstu.networks.client;

import ru.nstu.networks.client.controller.Controller;
import ru.nstu.networks.client.model.ChatClient;
import ru.nstu.networks.client.model.Model;
import ru.nstu.networks.client.view.ConsoleView;
import ru.nstu.networks.client.view.View;

public class Main {
    public static void main(String[] args) {
        Model model = new ChatClient();
        View view = new ConsoleView();

        Controller controller = new Controller(model, view);
        view.setController(controller);

        view.start();
    }
}