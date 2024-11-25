<template>
    <v-container>

        <v-row>
            <v-col>
                <h1>Clientes</h1>
            </v-col>
            <v-divider class="ma-3"></v-divider>
        </v-row>

        <v-row justify="space-between">
            <v-col cols="8">
                <v-text-field v-model="search" label="Pesquisar por Nome" clearable variant="outlined" density="compact" />
            </v-col>
            <v-col cols="2">
                <v-btn @click="showModal.addClient = true" color="primary">Adicionar Cliente</v-btn>
            </v-col>
        </v-row>
  
        <v-data-table
            class="border-table"
            :headers="headers"
            :items="filteredClients"
            item-key="id"
            :search="search"
            no-data-text="Nenhum cliente encontrado."
        >
            <template v-slot:items="props">
            <v-tr :key="props.item.id">
                <v-td>{{ props.item.nmCliente }}</v-td>
                <v-td>{{ props.item.cidade }}</v-td>
            </v-tr>
            </template>

            <template v-slot:item.actions="{ item }">
            <v-icon class="me-2" size="small" @click="editClientDetails(item)">mdi-pencil</v-icon>
            <v-icon size="small" @click="showConfirm(item)">mdi-delete</v-icon>
            </template>
    
        </v-data-table>
  
        <AddClient v-model="showModal.addClient" :client="clientToEdit" @addClient="handleClient" @editClient="" />
        <Confirm v-model="showModal.confirm" message="Tem certeza que deseja excluir o cliente?" @confirm="deleteClient" />

        </v-container>
</template>
  
<script lang="ts" setup>

import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";

import AddClient from '../components/Add/Client.vue';
import Confirm from '../components/Confirm.vue';
import axios from 'axios';  

import { defineComponent, ref, computed, onMounted } from 'vue';

const clients = ref([]);
const showModal = ref({
  addClient: false,
  editClient: false,
  confirm: false,
});

const newClient = ref({ nmCliente: '', cidade: '' });
const search = ref('');
const valid = ref(false);
const clientToEdit = ref(null);
const clientToDelete = ref(null);

const rules = {
  required: (value: any) => !!value || 'Este campo é obrigatório.',
};

const headers = ref([
  { text: 'Nome', value: 'nmCliente', title: 'Nome' },
  { text: 'Cidade', value: 'cidade', title: 'Cidade' },
  { text: 'Ações', value: 'actions', sortable: false, align: 'center', title: 'Ações' },
]);

const filteredClients = computed(() => {
  const searchTerm = search.value.trim().toLowerCase(); 
  return clients.value.filter((client) =>
    client.nmCliente.toLowerCase().includes(searchTerm)
  );
});

const loadClients = async () => {
  try {
    const response = await axios.get('http://localhost:5249/api/clientes'); 

    clients.value = response.data;
  } catch (error) {
    toast.error('Erro ao carregar clientes!');
    console.error('Erro ao carregar clientes:', error);
  }
};

const addNewClient = async (client) => {
  try {

    const response = await axios.post('http://localhost:5249/api/clientes', client, {
      headers: {
        'Content-Type': 'application/json'
      }
    });

    clients.value.push(response.data);  
    closeAddClientModal();

    toast.success("Cliente adicionado com sucesso"); 

    loadClients();
  } catch (error) {
    toast.error('Erro ao adicionar cliente!');
    console.error('Erro ao adicionar cliente:', error);
  }
};

const deleteClient = async () => {
  if (clientToDelete.value) {
    try {

      const response = await axios.delete(`http://localhost:5249/api/clientes/${clientToDelete.value.idCliente}`); 
      
      if(response.status !== 204) throw new Error('Erro ao excluir cliente');

      closeConfirmModal();

      toast.success('Cliente excluído com sucesso!');

      loadClients();
      
    } catch (error) {
      toast.error('Erro ao excluir cliente!');
      console.error('Erro ao excluir cliente:', error);
    }
  }
};

const editClientDetails = async (client: any) => {
  clientToEdit.value = client;
  newClient.value = { ...client };
  showModal.value.addClient = true;
};

const updateClient = async () => {
  if (clientToEdit.value) {

    try {

      const response = await axios.put(`http://localhost:5249/api/clientes/${clientToEdit.value.idCliente}`, clientToEdit.value); 
      const index = clients.value.findIndex(client => client.idCliente === response.data.idCliente);

      if (index !== -1) clients.value[index] = response.data; 

      toast.success('Cliente editado com sucesso!');
      
      closeAddClientModal();
    } catch (error) {
      toast.error('Erro ao editar cliente!');
      console.error('Erro ao editar cliente:', error);
    }
  }
};

const handleClient = () => {
  if (clientToEdit.value) return updateClient();
  return addNewClient(newClient.value);
};

const cancel = () => {
  updateDialogVisibility(false);
  emit('update:modelValue', false);  
}

const closeAddClientModal = () => {
  showModal.value.addClient = false;
  newClient.value = { name: '', city: '' };
};

const closeConfirmModal = () => {
  showModal.value.confirm = false;
  clientToDelete.value = null;
};

const updateDialogVisibility = (visible: boolean) => {
  if (!visible) resetForm();
  showModal.value.addClient = visible;
};

const showConfirm = (client: any) => {
  clientToDelete.value = client;
  showModal.value.confirm = true;
};

const resetForm = () => newClient.value = { name: '', city: '' };

onMounted(() => {
  loadClients();
});

</script>
  
<style scoped>

.border-table{
    border: 1px solid #e0e0e0;
    border-radius: 4px;
}

</style>
  