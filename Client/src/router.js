import { createRouter, createWebHistory } from "vue-router";
import MainContent from './views/MainContent.vue'
export default createRouter({
    history: createWebHistory(),
    routes:
    [
        {
            path: '/',
            name: 'Main',
            component: () => MainContent
        },
        {
            path: '/available',
            name: 'AvailableActivities',
            component: () => import('./views/AvailableActivitiesView.vue')
        },
        {
            path: '/createActivity',
            name: 'CreateActivity',
            component: () => import('./views/CreateActivityView.vue')
        },
        {
            path: '/createUser',
            name: 'CreateUser',
            component: () => import('./views/CreateUserView.vue')
        },
        {
            path: '/editUser',
            name: 'EditUser',
            component: () => import('./views/EditUserView.vue')
        },
        {
            path: '/log',
            name: 'LogIn',
            component: () => import('./views/LoginView.vue')
        },
        {
            path: '/my',
            name: 'My',
            component: () => import('./views/MyActivitiesView.vue')
        },
    ]
})