package ru.nstu.networks.chat.message;

import ru.nstu.networks.chat.user.User;

import java.io.Serializable;

public abstract class Message implements Serializable {
    protected final User sender;
    protected final String time;
    protected final String text;

    public Message(User sender, String time, String text) {
        this.sender = sender;
        this.time = time;
        this.text = text;
    }

    public abstract String getType();

    public User getSender() {
        return sender;
    }

    public String getDispatchTime() {
        return time;
    }

    public String getText() {
        return text;
    }

    public String toString() {
        return text;
    }
}