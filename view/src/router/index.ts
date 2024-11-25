import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'

import HomePage from '../views/HomePage.vue';
import ClientsPage from '../views/ClientsPage.vue';
import ProductsPage from '../views/ProductsPage.vue';
import SalesPage from '../views/SalesPage.vue';

const routes = [
  { path: '/', name: 'home', component: HomePage },
  { path: '/clients', name: 'clients', component: ClientsPage },
  { path: '/products', name: 'products', component: ProductsPage },
  { path: '/sales', name: 'sales', component: SalesPage },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
