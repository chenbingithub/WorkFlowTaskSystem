import Vue from 'vue';
import Vuex from 'vuex';

import app from './modules/app';
import user from './modules/user';
import role from './modules/role';
import permission from './modules/permission';
import organization from './modules/organization';

Vue.use(Vuex);

const store = new Vuex.Store({
    state: {
        //
    },
    mutations: {
        //
    },
    actions: {

    },
    modules: {
        app,
        user,
        role,
        permission,
        organization
    }
});

export default store;
