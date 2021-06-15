import Vue from 'vue';
import App from './App.vue';

import { BootstrapVue } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(BootstrapVue);


Vue.config.productionTip = false;

import router from './routes.js'

new Vue({
  router,
  render: h => h(App)
}).$mount('#app');
