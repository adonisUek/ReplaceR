<script setup>
import { ref, reactive } from 'vue'
import GridComponent from './GridComponent.vue';
const items = reactive([
  {
    "text": "Moje zapisy",
    "link": "link",
    "test": "test"
  },
  {
    "text": "Utwórz zamianę",
    "link": "link",
    "test": "test"

  },
  {
    "text": "Moje zamiany",
    "link": "link",
    "test": "test"
  },
  {
    "text": "Dostępne zamiany",
    "link": "link",
    "test": "test"
  },
  {
    "text": "Edycja konta",
    "link": "link",
    "i": "link",
  },
  {
    "text": "Wyloguj",
    "link": "link",
    "test": "test"
  }
]);
</script>

<template>
  <GridComponent :display-data=items title="Siemanero"></GridComponent>
</template>

<style scoped>

</style>
