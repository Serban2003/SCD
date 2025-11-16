package com.example.BikeRental.ChatBot;

import com.example.BikeRental.BikeUtils.BikeRepository;
import com.example.BikeRental.UserUtils.UserRepository;
import com.example.BikeRental.ManufacturerUtils.ManufacturerRepository;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClient;
import reactor.core.publisher.Mono;

import java.util.List;
import java.util.Map;

@Service
public class ChatBotService {

    private final WebClient webClient;
    private final String ollamaModel;


    public ChatBotService(
            @Value("${chatbot.ollamaUrl:http://localhost:11434}") String ollamaUrl,
            @Value("${chatbot.ollamaModel:llama3:3b}") String ollamaModel
    ) {
        this.webClient = WebClient.builder()
                .baseUrl(ollamaUrl)
                .build();
        this.ollamaModel = ollamaModel;
    }

    public Mono<String> callOllama(String prompt) {
        return webClient.post()
                .uri("/api/chat")
                .bodyValue(Map.of(
                        "model", ollamaModel,
                        "messages", List.of(
                                Map.of("role", "user", "content", prompt)
                        ),
                        "stream", false
                ))
                .retrieve()
                .bodyToMono(Map.class)
                .map(res -> {
                    Map<String, Object> message = (Map<String, Object>) res.get("message");
                    return (String) message.get("content");
                });
    }
}
