package com.example.BikeRental.ChatBot;

import com.example.BikeRental.BikeUtils.BikeRepository;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClient;
import reactor.core.publisher.Mono;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.util.List;
import java.util.Map;

    @Service
    public class ChatBotService {

        private final WebClient webClient;
        private final String ollamaModel;
        private final BikeRepository bikeRepository;
        private final ObjectMapper objectMapper = new ObjectMapper();

        public ChatBotService(
                @Value("${chatbot.ollamaUrl}") String ollamaUrl,
                @Value("${chatbot.ollamaModel}") String ollamaModel,
                BikeRepository bikeRepository
        ) {
            this.webClient = WebClient.builder()
                    .baseUrl(ollamaUrl)
                    .build();
            this.ollamaModel = ollamaModel;
            this.bikeRepository = bikeRepository;
        }

        public Mono<String> callOllama(String prompt) {
            List<?> bikes = bikeRepository.findAll();

            String dataJson;
            try {
                dataJson = objectMapper.writeValueAsString(bikes);
            } catch (Exception e) {
                dataJson = "[]";
            }

            Map<String, Object> request = Map.of(
                    "model", ollamaModel,
                    "messages", List.of(
                            Map.of(
                                    "role", "user",
                                    "content",
                                    "You are a bike database assistant.\n\n" +
                                            "Here is the bikes database in JSON format:\n" +
                                            "<<DATA>>\n" + dataJson + "\n<<END_DATA>>\n\n" +
                                            "You must answer ONLY based on this bikes database.\n" +
                                            "If the question does not refer to the database, reply with: <<I cannot answer>>.\n\n" +
                                            "User question:\n" +
                                            prompt
                            )
                    ),
                    "stream", false
            );
            System.out.println(request);

            return webClient.post()
                    .uri("/api/chat")
                    .bodyValue(request)
                    .retrieve()
                    .bodyToMono(Map.class)
                    .map(res -> {
                        Map<String, Object> message = (Map<String, Object>) res.get("message");
                        return (String) message.get("content");
                    });
        }
    }


