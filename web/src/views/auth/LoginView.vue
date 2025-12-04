<script lang="ts" setup>
import Card from 'primevue/card';

import LoginForm from '@/components/forms/LoginForm.vue';
</script>

<template>
  <main>
    <div class="card-container">
      <Card class="text-center card">
        <template #title>Welcome to Board Game Tracker</template>
        <template #subtitle>Sign in to continue</template>
        <template #content>
          <LoginForm />
        </template>
      </Card>
    </div>
  </main>
</template>

<style lang="scss" scoped>
.text-center {
  text-align: center;
}

.card-container {
  background-image: linear-gradient(
    180deg,
    var(--p-primary-500) 10%,
    transparent 30%
  );
  max-width: 34.5rem;
  border-radius: var(--p-card-border-radius);
  padding: 0.3rem;
  margin: 0 auto;
}

.card {
  padding: 2rem;
}

.form {
  margin-top: 2rem;
}
</style>
