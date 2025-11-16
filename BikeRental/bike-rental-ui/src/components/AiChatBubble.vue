<template>
  <!-- Floating bubble -->
  <div class="ai-chat-bubble">
    <v-btn
      icon
      size="large"
      color="deep-orange-darken-2"
      elevation="8"
      @click="toggleOpen"
    >
      <v-icon>mdi-robot-outline</v-icon>
    </v-btn>
  </div>

  <!-- Chat window -->
  <div v-if="open" class="ai-chat-window">
    <v-card elevation="10" class="d-flex flex-column fill-height">
      <!-- Header -->
      <v-card-title class="d-flex align-center justify-space-between py-2 px-3">
        <div class="d-flex align-center ga-2">
          <v-icon size="20">mdi-robot-outline</v-icon>
          <span class="text-subtitle-2 font-weight-medium">AI Assistant</span>
        </div>
        <v-btn
          icon
          variant="text"
          size="small"
          @click="toggleOpen"
        >
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </v-card-title>

      <v-divider />

      <!-- Messages -->
      <div ref="messagesContainer" class="chat-messages pa-3">
        <div
          v-for="(m, idx) in messages"
          :key="idx"
          class="mb-2"
          :class="m.role === 'user' ? 'text-right' : 'text-left'"
        >
          <div
            class="d-inline-block px-3 py-2 rounded-lg text-body-2"
            :class="m.role === 'user'
              ? 'bg-deep-orange-darken-2 text-white user-bubble'
              : 'bg-surface-variant assistant-bubble'"
          >
            {{ m.content }}
          </div>
        </div>

        <!-- Typing indicator -->
        <div v-if="loading" class="d-flex align-center ga-2 mt-2">
          <v-progress-circular indeterminate size="16" width="2" />
          <span class="text-caption text-medium-emphasis">Thinking...</span>
        </div>
      </div>

      <v-divider />

      <!-- Input -->
      <v-card-actions class="pa-2">
        <v-text-field
          v-model="draft"
          placeholder="Ask me something..."
          variant="outlined"
          density="comfortable"
          class="flex-grow-1"
          hide-details
          @keydown.enter.prevent="send"
        >
          <template #append-inner>
            <v-btn
              icon
              size="small"
              :disabled="!draft.trim() || loading"
              @click="send"
              color="deep-orange-darken-2"
            >
              <v-icon>mdi-send</v-icon>
            </v-btn>
          </template>
        </v-text-field>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script setup lang="ts">
import { ref, nextTick } from 'vue'

type ChatMessage = {
  role: 'user' | 'assistant'
  content: string
}

const open = ref(false)
const draft = ref('')
const loading = ref(false)
const messages = ref<ChatMessage[]>([
  {
    role: 'assistant',
    content: 'Hi! I am your AI assistant. How can I help you today?',
  },
])

const messagesContainer = ref<HTMLElement | null>(null)

function toggleOpen() {
  open.value = !open.value
  if (open.value) {
    // small delay to ensure DOM is rendered
    nextTick(() => scrollToBottom())
  }
}

function scrollToBottom() {
  if (!messagesContainer.value) return
  const el = messagesContainer.value
  el.scrollTop = el.scrollHeight
}

import { ChatBotApi } from "@/api/chatbot.api";

async function send() {
  const text = draft.value.trim()
  if (!text || loading.value) return

  messages.value.push({ role: 'user', content: text })
  draft.value = ''
  loading.value = true
  await nextTick()
  scrollToBottom()

  try {
    const reply = await ChatBotApi.sendMessage(text)

    messages.value.push({
      role: 'assistant',
      content: reply,
    })
  } catch (err) {
    console.error(err)
    messages.value.push({
      role: 'assistant',
      content: 'Oops, something went wrong talking to the AI backend.',
    })
  } finally {
    loading.value = false
    await nextTick()
    scrollToBottom()
  }
}

</script>

<style scoped>
.ai-chat-bubble {
  position: fixed;
  right: 24px;
  bottom: 24px;
  z-index: 2000;
}

.ai-chat-window {
  position: fixed;
  right: 24px;
  bottom: 90px;
  width: 360px;
  max-width: calc(100vw - 32px);
  height: 460px;
  z-index: 1999;
}

.chat-messages {
  flex: 1;
  overflow-y: auto;
  max-height: 100%;
}
.user-bubble {
  border-bottom-right-radius: 0;
}

.assistant-bubble {
  border-bottom-left-radius: 0;
}
</style>
