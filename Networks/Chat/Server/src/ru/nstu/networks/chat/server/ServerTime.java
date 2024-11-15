package ru.nstu.networks.chat.server;

import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;

public final class ServerTime {
    public static String getTime() {
        return LocalDateTime.now(ZoneId.of("Europe/Moscow"))
                .format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss:SS"));
    }
}