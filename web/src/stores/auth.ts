import { acceptHMRUpdate, defineStore } from 'pinia';

export const useAuth = defineStore('auth', {
  // TODO: Implement authentication state and actions
});

if (import.meta.hot) {
  import.meta.hot.accept(acceptHMRUpdate(useAuth, import.meta.hot));
}
