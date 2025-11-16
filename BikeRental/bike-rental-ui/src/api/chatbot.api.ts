import { http } from './client'

/**
 * Payload sent to the chatbot backend endpoint.
 */
export type ChatRequest = {
  /** User's message content */
  message: string;
};

/**
 * Response structure returned by the chatbot backend.
 */
export type ChatResponse = {
  /** Generated reply from the chatbot */
  reply: string;
};

/**
 * Base path for chatbot-related API calls.
 * The backend should expose a handler at "/api/chat".
 */
const BASE = "/api/chat";

/**
 * ChatBotApi provides a simple interface for sending messages
 * to the chatbot endpoint and receiving its textual replies.
 */
export const ChatBotApi = {
  /**
   * Send a single message to the chatbot and return its reply text.
   *
   * @param message - User input message to send to the chatbot.
   * @returns Promise resolving to the chatbot's reply string.
   */
  async sendMessage(message: string): Promise<string> {
    const { reply } = await http.post<ChatResponse>(BASE, {
      message,
    });
    return reply;
  },
};
