<script lang="ts" setup>
import Button from 'primevue/button';
import FloatLabel from 'primevue/floatlabel';
import InputText from 'primevue/inputtext';
import Message from 'primevue/message';
import { useToast } from 'primevue/usetoast';
import { z } from 'zod';
import { Form, FormField } from '@primevue/forms';
import { zodResolver } from '@primevue/forms/resolvers/zod';

const toast = useToast();

const resolver = zodResolver(
  z.object({
    email: z.email({ message: 'Invalid email address' }),
    password: z
      .string()
      .min(6, { message: 'Password must be at least 6 characters' })
  })
);

const onFormSubmit = ({ valid }: { valid: boolean }) => {
  if (valid) {
    toast.add({
      severity: 'success',
      summary: 'Form is submitted.',
      life: 3000
    });
  }
};
</script>

<template>
  <Form :resolver @submit="onFormSubmit" class="form">
    <FormField
      v-slot="$field"
      name="email"
      initialValue=""
      class="flex flex-col gap-1"
    >
      <FloatLabel>
        <InputText type="email" id="email" class="input" />
        <label for="email">Email</label>
      </FloatLabel>
      <Message
        v-if="$field?.invalid"
        severity="error"
        size="small"
        variant="simple"
        >{{ $field.error?.message }}</Message
      >
    </FormField>
    <FormField
      v-slot="$field"
      name="password"
      initialValue=""
      class="flex flex-col gap-1"
    >
      <FloatLabel>
        <InputText type="password" id="password" class="input" />
        <label for="password">Password</label>
      </FloatLabel>
      <Message
        v-if="$field?.invalid"
        severity="error"
        size="small"
        variant="simple"
        >{{ $field.error?.message }}</Message
      >
    </FormField>

    <div>
      <Button type="submit" label="Sign In" class="submit-button" />
      <div class="link-container">
        <router-link to="/register" class="link">
          Create an account
        </router-link>
        <router-link to="/forgot-password" class="link">
          Forgot password?
        </router-link>
      </div>
    </div>
  </Form>
</template>

<style lang="scss" scoped>
.form {
  display: flex;
  flex-direction: column;
  gap: 2rem;
  width: 100%;
}

.input {
  width: 100%;
}

.submit-button {
  display: block;
  width: 100%;
}

.link-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 0.5rem;
}

.link {
  display: block;
  text-decoration: none;
  font-size: 0.875rem;
  font-weight: 600;
  color: var(--p-primary-500);

  &:last-child {
    text-align: right;
  }
}
</style>
