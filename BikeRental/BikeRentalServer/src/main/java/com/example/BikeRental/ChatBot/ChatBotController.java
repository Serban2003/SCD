package com.example.BikeRental.ChatBot;

import lombok.Data;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/chat")
@RequiredArgsConstructor
public class ChatBotController {

    private final ChatBotService chatBotService;

    @PostMapping
    public ResponseEntity<ChatResponse> chat(@RequestBody ChatRequest request) {
        if (request == null || request.getMessage() == null || request.getMessage().isBlank()) {
            return ResponseEntity.badRequest()
                    .body(new ChatResponse("Message cannot be empty."));
        }

        String answer = chatBotService.callOllama(request.getMessage()).block();

        if (answer == null) {
            answer = "Sorry, I could not generate a response.";
        }

        ChatResponse response = new ChatResponse(answer);
        return ResponseEntity.ok(response);
    }

    @Data
    public static class ChatRequest {
        private String message;
    }

    @Data
    public static class ChatResponse {
        private final String reply;
    }
}
