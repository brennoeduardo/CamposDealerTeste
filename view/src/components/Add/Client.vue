<template>
    <v-dialog v-model="showDialog" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ titleDialog }}</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="valid">
            <v-text-field
              v-model="newClient.nmCliente"
              label="Nome"
              variant="outlined"
              :rules="[rules.required]"
            />
            <v-text-field
              v-model="newClient.cidade"
              label="Cidade"
              variant="outlined"
              :rules="[rules.required]"
            />
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn @click="cancel">Cancelar</v-btn>
          <v-btn @click="handleClient" color="primary" :disabled="!valid">{{ nameButtonConfirm }}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
</template>

<script lang="ts" setup>

import { toast } from 'vue3-toastify';

import { defineProps, defineEmits, ref, watch, computed } from 'vue';

interface Client {
  nmCliente: string;
  cidade: string;
}

const props = defineProps<{
  client?: Client;  
  modelValue: boolean;
}>();

const hasClient = computed(() => !!props.client);

const titleDialog = computed(() => hasClient.value ? 'Editar Cliente' : 'Adicionar Cliente');
const nameButtonConfirm = computed(() => hasClient.value ? 'Editar' : 'Adicionar');

const emit = defineEmits(['update:modelValue', 'addClient']);

const valid = ref(false);

const showDialog = ref(props.modelValue);

const newClient = ref<Client>({ nmCliente: '', cidade: '' });

const rules = { required: (value: any) => !!value || 'Este campo é obrigatório.', };

watch(() => props.modelValue, (newValue) => {
  updateDialogVisibility(newValue);
}, { immediate: true });

function updateDialogVisibility(visible: boolean) {
  if(!visible) resetForm();
  showDialog.value = visible;
}

function cancel() {
  updateDialogVisibility(false);
  emit('update:modelValue', false);  
}

function handleClient() {
  if (newClient.value.nmCliente && newClient.value.cidade) {
    emit('addClient', newClient.value);
  } else {
    toast.error('Preencha todos os campos obrigatórios.');
  }
  resetForm();
  closeDialog();
}

function resetForm() {
  newClient.value = { nmCliente: '', cidade: '' };
}

function closeDialog() {
  updateDialogVisibility(false);
  emit('update:modelValue', false);  
}

watch(() => props.client, (value) => {
  if(value) newClient.value = value;  
}, { immediate: true });

</script>