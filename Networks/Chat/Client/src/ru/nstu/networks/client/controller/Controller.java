package ru.nstu.networks.client.controller;

import ru.nstu.networks.client.model.Model;
import ru.nstu.networks.client.view.View;

import java.io.IOException;
import java.net.InetAddress;

public class Controller {
    private final Model model;
    private final View view;

    private boolean isModelRunning;

    public Controller(Model model, View view) {
        this.model = model;
        this.view = view;
    }

    public void startModel(InetAddress serverAddress, int port, String username) {
        if (isModelRunning) {
            throw new RuntimeException("Error: re-running the model is prohibited"); // create exception
        }

        model.configure(serverAddress, port, username);
        new Thread(model::start).start();

        do {
            isModelRunning = model.isRunning();

            try {
                Thread.sleep(100);
            } catch (InterruptedException e) {
                System.out.println(e.getMessage());
                System.exit(7);
            }
        } while (!isModelRunning);

        enableAutoUpdate();
    }

    public void stopModel() {
        validateForIllegalCall();
        model.stop();
    }

    public void sendMessage(String message) {
        validateForIllegalCall();
        model.sendMessage(message);
    }

    private void receiveMessage() {
        try {
            view.showMessage(model.receiveMessage());
        } catch (IOException e) {
            System.out.println("An error occurred: " + e.getMessage());
        }
    }

    private void enableAutoUpdate() {
        new Thread(() -> {
            while (isModelRunning) {
                receiveMessage();
            }
        }).start();
    }

    private void validateForIllegalCall() {
        if (!isModelRunning) {
            throw new RuntimeException("Calling the method is prohibited: the model is not running");
        }
    }
}