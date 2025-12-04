<script setup lang="ts">
import Toast from 'primevue/toast';

import NavMenu from './components/navigation/NavMenu.vue';
</script>

<template>
  <header>
    <NavMenu />
  </header>
  <router-view></router-view>
  <Toast />
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
header {
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  top: 0.5rem;
  right: 0.5rem;
  left: 0.5rem;
  z-index: 10;
}
</style>
