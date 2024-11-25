<template>
    <v-dialog v-model="showDialog" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ titleDialog }}</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="valid">
            <v-text-field
              v-model="newProduct.description"
              label="Descrição"
              variant="outlined"
              :rules="[rules.required]"
            />
            <v-text-field
              v-model="newProduct.price"
              label="Preço"
              variant="outlined"
              type="number"
              :rules="[rules.required, rules.positivePrice]"
            />
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn @click="cancel">Cancelar</v-btn>
          <v-btn @click="addNewProduct" color="primary" :disabled="!valid">{{ nameButtonConfirm }}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </template>
  
<script lang="ts" setup>
  
import { defineProps, defineEmits, ref, watch, computed } from 'vue';
  
interface Product {
  description: string;
  price: number;
}

const props = defineProps<{
  product?: Product;
  modelValue: boolean;
}>();
  
const hasProduct = computed(() => !!props.product);

const titleDialog = computed(() => hasProduct.value ? 'Editar Produto' : 'Adicionar Produto');
const nameButtonConfirm = computed(() => hasProduct.value ? 'Editar' : 'Adicionar');

const emit = defineEmits(['update:modelValue', 'addProduct', 'editProduct']);

const valid = ref(false);

const showDialog = ref(props.modelValue);
const newProduct = ref({ description: '', price: 0 });

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

function addNewProduct() {
  resetForm();
  closeDialog();

  if (hasProduct.value) return emit('editProduct', newProduct.value);
  emit('addProduct', newProduct.value);
}

function resetForm() {
  newProduct.value = { description: '', price: 0 };
}

function closeDialog() {
  updateDialogVisibility(false);
  emit('update:modelValue', false);
}

watch(() => props.product, (value) => {
  if (value) newProduct.value = value;
}, { immediate: true });
  
</script>
  