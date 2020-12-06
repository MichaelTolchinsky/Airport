import { createApp } from 'vue'
import App from './App.vue'
import AirportHub from '@/plugins/airport-hub';

createApp(App).use(AirportHub).mount('#app');
