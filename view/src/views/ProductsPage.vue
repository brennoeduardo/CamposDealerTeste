<template>
    <v-container>

        <v-row>
            <v-col>
                <h1>Produtos</h1>
            </v-col>
            <v-divider class="ma-3"></v-divider>
        </v-row>
  
        <v-row justify="space-between">
            <v-col cols="8">
                <v-text-field v-model="search" label="Pesquisar por Descrição" clearable variant="outlined" density="compact" />
            </v-col>
            <v-col cols="2">
                <v-btn @click="showModal.addProduct = true" color="primary">Adicionar Produto</v-btn>
            </v-col>
        </v-row>
  
        <v-data-table
            class="border-table"
            :headers="headers"
            :items="filteredProducts"
            item-key="id"
            :search="search"
            no-data-text="Nenhum produto encontrado."
        >
            <template v-slot:items="props">
            <v-tr :key="props.item.id">
                <v-td>{{ props.item.description }}</v-td>
                <v-td>{{ props.item.price }}</v-td>
            </v-tr>
            </template>
    
            <template v-slot:item.actions="{ item }">
            <v-icon class="me-2" size="small" @click="editProductDetails(item)">mdi-pencil</v-icon>
            <v-icon size="small" @click="showConfirmDelete(item)">mdi-delete</v-icon>
            </template>
        
        </v-data-table>
  
        <AddProduct v-model="showModal.addProduct" :product="productToEdit" @add-product="addNewProduct" @edit-product="editProduct" />
        <Confirm v-model="showModal.confirm" message="Tem certeza que deseja excluir o produto?" @confirm="deleteProduct" />
    </v-container>
</template>
  
<script lang="ts" setup>

import AddProduct from '../components/Add/Product.vue';
import Confirm from '../components/Confirm.vue';

import { defineComponent, ref, computed } from 'vue';
  
const products = ref([
  { id: 1, description: 'Produto A', price: 100 },
  { id: 2, description: 'Produto B', price: 150 },
]);
  
const showModal = ref({
  addProduct: false,
  editProduct: false,
  confirm: false,
});
  
const newProduct = ref({ description: '', price: 0 });
const search = ref('');
const valid = ref(false);
const productToEdit = ref(null);
const productToDelete = ref(null);
  
const rules = {
  required: (value: any) => !!value || 'Este campo é obrigatório.',
  positivePrice: (value: number) => value > 0 || 'O preço deve ser maior que zero.',
};
  
const headers = ref([
  { text: 'Descrição', value: 'description', title: 'Descrição' },
  { text: 'Valor Unitário', value: 'valor_unitario', title: 'Preço' },
  { text: 'Ações', value: 'actions', sortable: false, align: 'center', title: 'Ações' },
]);
  
const filteredProducts = computed(() => {
  return products.value.filter((product) =>
    product.description.toLowerCase().includes(search.value.toLowerCase())
  );
});
  
function addNewProduct() {
  products.value.push({
    id: products.value.length + 1,
    ...newProduct.value,
  });
  closeAddProductModal();
}
  
function showConfirmDelete(product: any) {
  showModal.value.confirm = true;
  productToDelete.value = product;
}
  
function deleteProduct() {
  if (productToDelete.value) {
    products.value = products.value.filter((product) => product !== productToDelete.value);
    closeConfirmModal();
  }
}
  
function editProductDetails(product: any) {
  productToEdit.value = product;
  newProduct.value = { ...product };
  showModal.value.addProduct = true;
}

function closeAddProductModal() {
  showModal.value.addProduct = false;
  newProduct.value = { description: '', price: 0 };
}

function closeConfirmModal() {
  showModal.value.confirm = false;
  productToDelete.value = null;
}
  
function initialize() {}

</script>
  
<style scoped>
.border-table {
  border: 1px solid #e0e0e0;
  border-radius: 4px;
}
</style>
  