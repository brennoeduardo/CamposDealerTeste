<template>
  <v-container>
    <v-row>
      <v-col>
        <h1>Vendas</h1>
      </v-col>
      <v-divider class="ma-3"></v-divider>
    </v-row>

    <v-row justify="space-between">
      <v-col cols="8">
        <v-text-field
          v-model="search"
          label="Pesquisar por Cliente ou Produto"
          clearable
          variant="outlined"
          density="compact"
        />
      </v-col>
      <v-col cols="2">
        <v-btn @click="showModal.addSale = true" color="primary">Adicionar Venda</v-btn>
      </v-col>
    </v-row>

    <v-data-table
      class="border-table"
      :headers="headers"
      :items="filteredSales"
      item-key="id"
      :search="search"
      no-data-text="Nenhuma venda encontrada."
    >
      <template v-slot:items="props">
        <v-tr :key="props.item.id">
          <v-td>{{ props.item.clientName }}</v-td>
          <v-td>{{ props.item.productDescription }}</v-td>
          <v-td>{{ props.item.price }}</v-td>
        </v-tr>
      </template>

      <template v-slot:item.actions="{ item }">
        <v-icon class="me-2" size="small" @click="editSale(item)">mdi-pencil</v-icon>
        <v-icon size="small" @click="showConfirmDelete(item)">mdi-delete</v-icon>
      </template>
    </v-data-table>

    <AddSale v-model="showModal.addSale" :sale="saleToEdit" @addSale="addNewSale" @edit-sale="editSale" />
    <Confirm v-model="showModal.confirm" message="Tem certeza que deseja excluir a venda?" @confirm="deleteSale" />
  </v-container>
</template>

<script lang="ts" setup>

import { toast } from 'vue3-toastify';

import AddSale from '../components/Add/Sale.vue';
import Confirm from '../components/Confirm.vue';
import { defineComponent, ref, computed, onMounted } from 'vue';

import axios from 'axios';

const sales = ref([]);

const showModal = ref({
  addSale: false,
  confirm: false,
});

const search = ref('');
const saleToEdit = ref(null);
const saleToDelete = ref(null);

const headers = ref([
  { text: 'Cliente', value: 'clientName', title: 'Cliente' },
  { text: 'Produto', value: 'productDescription', title: 'Produto' },
  { text: 'Preço', value: 'price', title: 'Preço' },
  { text: 'Ações', value: 'actions', sortable: false, align: 'center', title: 'Ações' },
]);

const filteredSales = computed(() => {
  return sales.value.filter((sale) =>
    sale.clientName.toLowerCase().includes(search.value.toLowerCase()) ||
    sale.productDescription.toLowerCase().includes(search.value.toLowerCase())
  );
});

const loadSales = async () => {
  try {
    const response = await axios.get('http://localhost:5249/api/vendas');
    sales.value = response.data;
  } catch (error) {
    toast.error('Erro ao carregar vendas!');
    console.error('Erro ao carregar vendas:', error);
  }
};

const addNewSale = async (newSale) => {
  try {

    const response = await axios.post('http://localhost:5249/api/vendas', newSale, {       
      headers: {
        'Content-Type': 'application/json'
      }}
    );

    closeAddSaleModal();

    toast.success('Venda adicionada com sucesso!');
    loadSales();
  } catch (error) {
    toast.error('Erro ao adicionar venda!');
    console.error('Erro ao adicionar venda:', error);
  }
};

const updateSale = async () => {
  if (saleToEdit.value) {
    try {
      const response = await axios.put(`http://localhost:5249/api/vendas/${saleToEdit.value.id}`, saleToEdit.value);
      const index = sales.value.findIndex(sale => sale.id === response.data.id);
      if (index !== -1) sales.value[index] = response.data;
      toast.success('Venda editada com sucesso!');
      closeAddSaleModal();
    } catch (error) {
      toast.error('Erro ao editar venda!');
      console.error('Erro ao editar venda:', error);
    }
  }
};

const deleteSale = async () => {
  if (saleToDelete.value) {
    try {
      const response = await axios.delete(`http://localhost:5249/api/vendas/${saleToDelete.value.id}`);
      if (response.status === 204) {
        sales.value = sales.value.filter(sale => sale.id !== saleToDelete.value.id);
        toast.success('Venda excluída com sucesso!');
      } else {
        throw new Error('Erro ao excluir venda');
      }
      closeConfirmModal();
    } catch (error) {
      toast.error('Erro ao excluir venda!');
      console.error('Erro ao excluir venda:', error);
    }
  }
};

onMounted(() => {
  loadSales();
});

function showConfirmDelete(sale: any) {
  showModal.value.confirm = true;
  saleToDelete.value = sale;
}

function editSaleDetails(sale: any) {
  saleToEdit.value = sale;
  showModal.value.addSale = true;
}

function closeAddSaleModal() {
  showModal.value.addSale = false;
  saleToEdit.value = null;
}

function closeConfirmModal() {
  showModal.value.confirm = false;
  saleToDelete.value = null;
}

function initialize() {}
</script>

<style scoped>
.border-table {
  border: 1px solid #e0e0e0;
  border-radius: 4px;
}
</style>
