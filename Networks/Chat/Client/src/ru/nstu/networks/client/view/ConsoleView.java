package ru.nstu.networks.client.view;

import ru.nstu.networks.client.controller.Controller;

import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.Scanner;

public class ConsoleView implements View {
    private Controller controller;

    public ConsoleView() {
    }

    @Override
    public void setController(Controller controller) {
        this.controller = controller;
    }

    @Override
    public void start() {
        if (controller == null) {
            throw new NullPointerException("Controller must be specified");
        }

        Scanner scanner = new Scanner(System.in);

        InetAddress serverAddress = getServerAddress(scanner);
        int port = getPort(scanner);

        System.out.print("Enter your name: ");
        String username = scanner.nextLine();

        controller.startModel(serverAddress, port, username);

        String message;

        try {
            while ((message = scanner.nextLine()) != null) {
                sendMessage(message);
            }
        } catch (Exception e) {
            System.out.println("You have disconnected from the chat.");
            System.exit(0);
        }
    }

    @Override
    public void showMessage(String message) {
        System.out.println(message);
    }

    @Override
    public void sendMessage(String message) {
        controller.sendMessage(message);
    }

    private InetAddress getServerAddress(Scanner scanner) {
        while (true) {
            try {
                System.out.print("Enter the server address: ");
                InetAddress serverAddress = InetAddress.getByName(scanner.nextLine());

                if (serverAddress == null || serverAddress.getHostAddress().isEmpty()) {
                    throw new UnknownHostException();
                }

                return serverAddress;
            } catch (UnknownHostException e) {
                System.out.println("Error: Unknown server address. Try again.");
            }
        }
    }

    private int getPort(Scanner scanner) {
        while (true) {
            try {
                System.out.print("Enter the port number: ");
                int port = scanner.nextInt();

                if (port < 1024 || port > 49151) {
                    throw new IllegalArgumentException("Ports available to users from 1024 to 49151."
                            + " Current port: " + port);
                }

                return port;
            } catch (IllegalArgumentException e) {
                System.out.println("Error: " + e.getMessage() + ". Try again.");
            } catch (Exception e) {
                System.out.println("An error occurred while reading int number from console");
            } finally {
                scanner.nextLine();
            }
        }
    }
}