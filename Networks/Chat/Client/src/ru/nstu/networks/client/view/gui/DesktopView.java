package ru.nstu.networks.client.view.gui;

import ru.nstu.networks.client.controller.Controller;
import ru.nstu.networks.client.view.View;

import javax.swing.*;

public class DesktopView implements View {
    private Controller controller;

    @Override
    public void start() {
        SwingUtilities.invokeLater(() -> {
            JFrame frame = new JFrame("Chat");
            configureFrameWithStartSettings(frame);

            JLabel inputAddressLabel = new JLabel("Server address:");
            JTextField inputAddressTextField = new JTextField(15);

            JLabel inputPortLabel = new JLabel("Port number:");
            JTextField inputPortTextField = new JTextField(15);

            JLabel inputUsernameLabel = new JLabel("Your name:");
            JTextField inputUsernameTextField = new JTextField("hello", 15);

            JPanel panel = new JPanel();
            panel.setLayout(new BoxLayout(panel, BoxLayout.PAGE_AXIS));

            panel.add(inputAddressLabel);
            panel.add(inputAddressTextField);

            panel.add(inputPortLabel);
            panel.add(inputPortTextField);

            panel.add(inputUsernameLabel);
            panel.add(inputUsernameTextField);

            frame.add(panel);
        });
    }

    @Override
    public void setController(Controller controller) {
        this.controller = controller;
    }

    @Override
    public void showMessage(String message) {

    }

    @Override
    public void sendMessage(String message) {
        controller.sendMessage(message);
    }

    private static void configureFrameWithStartSettings(JFrame frame) {
        frame.setSize(FrameConfigureConstants.FRAME_START_WIDTH, FrameConfigureConstants.FRAME_START_HEIGHT);
        frame.setResizable(FrameConfigureConstants.IS_RESIZABLE_BY_DEFAULT);
        frame.setLocationRelativeTo(FrameConfigureConstants.DEFAULT_LOCATION_RELATIVE_TO);
        frame.setDefaultCloseOperation(FrameConfigureConstants.DEFAULT_CLOSE_OPERATION);
        frame.setBackground(FrameConfigureConstants.DEFAULT_BACKGROUND_COLOR);
        frame.setVisible(FrameConfigureConstants.IS_VISIBLE_BY_DEFAULT);
    }
}