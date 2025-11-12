<!-- src/App.vue -->
<template>
  <v-app>
    <v-layout>
      <v-navigation-drawer permanent color="primary" :width="220">
        <v-list-item title="BikeRental" subtitle="Web UI" />
        <v-divider />
        <v-list nav>
          <v-list-item to="/bikes"          title="Bikes"          prepend-icon="mdi-bicycle" />
          <v-list-item to="/manufacturers"  title="Manufacturers"  prepend-icon="mdi-factory" />
          <v-list-item to="/users"          title="Users"          prepend-icon="mdi-account" />
        </v-list>
      </v-navigation-drawer>

      <v-main>
        <router-view />
      </v-main>
    </v-layout>
  </v-app>
</template>

<script setup lang="ts">
// nothing needed
</script>
