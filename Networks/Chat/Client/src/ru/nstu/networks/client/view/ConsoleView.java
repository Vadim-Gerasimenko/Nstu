package ru.nstu.networks.client.view;

import ru.nstu.networks.client.controller.Controller;

import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.Scanner;

public class ConsoleView implements View {
    private Controller controller;

    public ConsoleView() {
    }

    public void setController(Controller controller) {
        this.controller = controller;
    }

    @Override
    public void start() {
        if (controller == null) {
            throw new NullPointerException("Controller must be specified");
        }
        Scanner scanner = new Scanner(System.in);

        boolean isCorrectAddress = false;
        InetAddress serverAddress = null;

        while (!isCorrectAddress) {
            System.out.print("Enter the server address: ");

            try {
                serverAddress = InetAddress.getByName(scanner.nextLine());
                isCorrectAddress = true;
            } catch (UnknownHostException e) {
                System.out.println("Error: Unknown server address. Try again.");
            }
        }

        boolean isCorrectPort = false;
        int port = 0;

        while (!isCorrectPort) {
            System.out.print("Enter the port number: ");

            try {
                port = scanner.nextInt();

                if (port < 1024 || port > 49151) {
                    throw new IllegalArgumentException("Ports available to users from 1024 to 49151."
                            + " Current port: " + port);
                }

                isCorrectPort = true;
            } catch (IllegalArgumentException e) {
                System.out.println("Error: " + e.getMessage() + ". Try again.");
            }

            scanner.nextLine();
        }

        System.out.print("Enter your name: ");
        String username = scanner.nextLine();

        controller.startModel(serverAddress, port, username);
    }
}