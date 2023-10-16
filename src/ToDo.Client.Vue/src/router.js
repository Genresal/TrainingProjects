import { createRouter, createWebHistory } from 'vue-router';
import TablePage from '@/views/TablePage.vue'; // Update the import path
//import HomePage from '@/views/HomePage.vue'; // Create a new Vue component for the home page

const routes = [
    /*{
        path: '/',
        component: HomePage, // Set the component for the home page
    },*/
    {
        path: '/table',
        component: TablePage,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;