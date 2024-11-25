<template>
  <v-app>
    <v-app-bar app color="primary">
      <v-toolbar-title>Campos Dealer</v-toolbar-title>
      <v-spacer></v-spacer>
      
      <v-btn v-for="page in pages" :key="page.title" :to="page.to" text>
        <v-icon>{{ page.icon }}</v-icon>
        {{ page.title }}
      </v-btn>

    </v-app-bar>

    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script lang="ts" setup>

import { ref } from 'vue'

const pages = ref([
  { title: 'Home', icon: 'mdi-home', to: '/' },
  { title: 'Clientes', icon: 'mdi-account', to: '/clients' },
  { title: 'Vendas', icon: 'mdi-cart', to: '/sales' },
  { title: 'Produtos', icon: 'mdi-package-variant-closed', to: '/products' },
])

</script>
