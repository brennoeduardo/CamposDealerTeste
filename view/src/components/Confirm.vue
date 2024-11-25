<template>
    <v-dialog v-model="showDialog" max-width="500px">
        <v-card>
            <v-card-title>
                <span class="headline">Confirmação</span>
            </v-card-title>
            <v-card-text>
                {{ props.message }}
            </v-card-text>
            <v-card-actions>
                <v-btn @click="cancel">Cancelar</v-btn>
                <v-btn @click="confirm" color="primary">Confirmar</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script lang="ts" setup>

import { defineProps, defineEmits, ref, watch } from 'vue';

const props = defineProps<{
  modelValue: boolean; 
  message: string;  
}>();

const emit = defineEmits(['update:modelValue', 'confirm']);

const showDialog = ref(props.modelValue);

const cancel = () => emit('update:modelValue', false);

const confirm = () => {
    emit('update:modelValue', false);
    emit('confirm');
}

watch(() => props.modelValue, (newValue) => {
  showDialog.value = newValue;
}, { immediate: true });

</script>
<style scoped></style>