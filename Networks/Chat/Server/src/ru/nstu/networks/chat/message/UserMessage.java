package ru.nstu.networks.chat.message;

import ru.nstu.networks.chat.user.User;

public class UserMessage extends Message {
    private static final String type = "[MESSAGE]";

    public UserMessage(User sender, String time, String text) {
        super(sender, time, text);
    }

    @Override
    public String getType() {
        return type;
    }

    @Override
    public String toString() {
        return type + " " + time + " " + sender + ": " + text;
    }
}