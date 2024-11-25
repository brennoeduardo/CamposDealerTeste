<template>
    <v-dialog v-model="showDialog" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ titleDialog }}</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="valid">

            <v-select v-model="newSale.IdCliente" :items="clientes" item-title="nmCliente" item-value="idCliente" label="Cliente" :rules="[rules.required]" />

            <v-select v-model="newSale.idProduto" :items="produtos" item-title="dscProduto" item-value="idProduto" label="Produto" :rules="[rules.required]" />
            
            <v-text-field v-model="newSale.qtdVenda" type="number" label="Quantidade" :rules="[rules.required]" />

            <v-text-field
              v-model="newSale.vlrUnitarioVenda"
              label="Preço"
              type="number"
              :rules="[rules.required, rules.positivePrice]"
            />

          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn @click="cancel">Cancelar</v-btn>
          <v-btn @click="saveSale" color="primary" :disabled="!valid">{{ nameButtonConfirm }}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </template>
  
<script lang="ts" setup>

import { toast } from 'vue3-toastify';

import { defineProps, defineEmits, ref, computed, watch, onMounted } from 'vue';

import axios from 'axios';

interface Cliente {
  IdCliente: number;
  nmCliente: string;
}

interface Produto {
  idProduto: number;
  dscProduto: string;
}

interface Sale {
  IdCliente: Cliente;
  idProduto: Produto;
  quantidade: number;
  vlrUnitarioVenda: number;
}

const props = defineProps<{
  sale?: Sale;
  modelValue: boolean;
}>();

const hasSale = computed(() => !!props.sale);

const titleDialog = computed(() => hasSale.value ? 'Editar Venda' : 'Adicionar Venda');
const nameButtonConfirm = computed(() => hasSale.value ? 'Editar' : 'Adicionar');

const emit = defineEmits(['update:modelValue', 'addSale', 'editSale']);

const valid = ref(false);
const showDialog = ref(props.modelValue);

const template_sale = () => {
  return {
    IdCliente: null,
    idProduto: null, 
    qtdVenda: 0,
    vlrUnitarioVenda: 0,
  };
}

const newSale = ref<Sale>(template_sale());

const clientes = ref([]);
const produtos = ref([]);

const rules = {
  required: (value: any) => !!value || 'Este campo é obrigatório.',
  positivePrice: (value: number) => value > 0 || 'O preço deve ser maior que zero.',
};

watch(() => props.modelValue, (newValue) => {
  updateDialogVisibility(newValue);
}, { immediate: true });

function updateDialogVisibility(visible: boolean) {
  if (!visible) resetForm();
  showDialog.value = visible;
}

function cancel() {
  updateDialogVisibility(false);
  emit('update:modelValue', false);
}

function saveSale() {

  if(!newSale.value.IdCliente || !newSale.value.idProduto || !newSale.value.qtdVenda || !newSale.value.vlrUnitarioVenda) {
    return toast.error('Preencha todos os campos obrigatórios.');
  }

  const saleData = {
    IdCliente: newSale.value.IdCliente,
    idProduto: newSale.value.idProduto,
    qtdVenda: newSale.value.qtdVenda, 
    vlrUnitarioVenda: newSale.value.vlrUnitarioVenda, 
  };

  emit('addSale', saleData);

  resetForm();
  closeDialog();
}
function resetForm() {
  valid.value = false;
  newSale.value = template_sale();
}

function closeDialog() {
  updateDialogVisibility(false);
  emit('update:modelValue', false);
}

const getClients = async () => {
  const response = await axios.get('http://localhost:5249/api/clientes');
  clientes.value = response.data;
};

const getProducts = async () => {
  const response = await axios.get('http://localhost:5249/api/produtos');
  produtos.value = response.data;
};

onMounted(() => {
  getClients();
  getProducts();
});

</script>
  