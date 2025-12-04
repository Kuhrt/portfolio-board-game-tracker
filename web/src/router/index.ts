import { createRouter, createWebHistory } from 'vue-router';

import LoginView from '@/views/auth/LoginView.vue';
import HomeView from '@/views/HomeView.vue';
import NotFoundView from '@/views/NotFoundView.vue';

const routes = [
  { path: '/', component: HomeView },
  { path: '/login', component: LoginView },
  { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFoundView }
];

export const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
