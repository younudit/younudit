import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';

Vue.use(Vuetify);

export default new Vuetify({
    theme: {
        themes: {
            light: {
                primary: '#5ea841',
                secondary: '#009ccd',
                accent: '#ecaf3d',
                error: '#b71c1c',
            },
            dark: {
                primary: '#5ea841',
                secondary: '#009ccd',
                accent: '#ecaf3d',
                error: '#b71c1c',
            },
        },
    },

});
