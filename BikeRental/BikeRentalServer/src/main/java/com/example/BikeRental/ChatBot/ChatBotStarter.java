package com.example.BikeRental.ChatBot;

import org.springframework.boot.context.event.ApplicationReadyEvent;
import org.springframework.context.event.EventListener;

public class ChatBotStarter {
    private final ChatBotService chatBotService;

    public ChatBotStarter(ChatBotService chatBotService) {
        this.chatBotService = chatBotService;
    }

    @EventListener(ApplicationReadyEvent.class)
    public void startup() {
        chatBotService.callOllama("Starting up.").block();
    }
}
