<script setup>
import { ref, reactive, onMounted, toRaw } from 'vue'
import GridComponent from '../components/GridComponent.vue';
import TextboxComponent from '../components/TextboxComponent.vue';
import common from '../common.js'
import axios from 'axios';
common.menuVisible = true;
const users = ref(null);
const userId = 1;
const isScriptLoaded = ref(false);

function log(text) {
  console.log(text);
  location.reload();//do przeładowania strony jeszcze raz
}
/*function DisplayTest(callback, parameter) {
  callback.apply(this, parameter);
  return callback(parameter);
}

function Add(one) {
  return Number(one);
}
console.log(DisplayTest(Add, [1]));*/
//
onMounted(async () => {
  try {
    const response = await axios.get(`http://localhost:5141/api/activity/my/${userId}`);
    //const response = await axios.get(`http://localhost:5141/api/activity/my/${userId}`,{ params: {id: 1} });
    users.value = response.data;
  } catch (error) {
    console.error(error);
  }
});

</script>

<template>
  <p>{{ users }} </p>
  <GridComponent v-if="users !== null"  :display-data-source=users title="TestowyGrid" button-text="Wybierz"
    :button-type=common.buttonType.Accept @button-clicked="e => log(e)"></GridComponent>
    <p v-else>Błąd podczas tworzenia tabeli</p>
  <TextboxComponent label="Hasło" placeholder="Podaj hasło" tooltip="Hasło nie może być krótsze niż 6 znaków"
    :is-password=true @text-changed="e => pwd = e"></TextboxComponent>
  <button @click="log(item1)"></button>
</template>

<style scoped></style>
