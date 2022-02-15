
__webpack_public_path__ = '~/'

import Vue from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify'
import router from './router'
import { config } from '../config'
import "./bootstrap"

Vue.config.productionTip = true;

Vue.prototype.appConfig = config;


new Vue({
    vuetify,
    router,
    render: h => h(App)
}).$mount('#app');
