package ru.nstu.networks.chat.message;

import ru.nstu.networks.chat.user.User;

public class InfoMessage extends Message {
    private static final String type = "[INFO]";

    public InfoMessage(User sender, String time, String text) {
        super(sender, time, text);
    }

    @Override
    public String getType() {
        return type;
    }

    @Override
    public String toString() {
        return type + " " + time + ": " + text;
    }
}