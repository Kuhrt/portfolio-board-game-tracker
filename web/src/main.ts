import PrimeVue from 'primevue/config';
import ToastService from 'primevue/toastservice';
import { createApp } from 'vue';
import { definePreset } from '@primeuix/themes';
import Aura from '@primeuix/themes/aura';

import router from '@/router';

import App from './App.vue';

import 'primeicons/primeicons.css';
import './styles/main.scss';

const app = createApp(App);

const TrackerTheme = definePreset(Aura, {
  components: {
    card: {
      root: {
        borderRadius: '1.25rem'
      }
    }
  }
});

app.use(router);
app.use(PrimeVue, {
  theme: {
    preset: TrackerTheme
  }
});
app.use(ToastService);

app.mount('#app');
