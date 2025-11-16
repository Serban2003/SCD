<!-- src/App.vue -->
<template>
  <v-app>
    <v-layout style="background-color: #212121;">
      <v-navigation-drawer permanent color="deep-orange-darken-2" :width="220">
        <v-list-item title="BikeRental" subtitle="Web UI" />
        <v-divider />
        <v-list nav>
          <v-list-item to="/bikes"          title="Bikes"          prepend-icon="mdi-bicycle" />
          <v-list-item to="/manufacturers"  title="Manufacturers"  prepend-icon="mdi-factory" />
          <v-list-item to="/users"          title="Users"          prepend-icon="mdi-account" />
        </v-list>
      </v-navigation-drawer>

      <v-main class="pb-8">
        <router-view />
      </v-main>
    </v-layout>
    <AiChatBubble />
    <app-footer />
  </v-app>
</template>

<script setup lang="ts">
// nothing needed
</script>
