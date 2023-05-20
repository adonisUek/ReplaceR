<script setup>
import { ref, reactive, onMounted, toRaw } from 'vue'
import GridComponent from '../components/GridComponent.vue';
import TextboxComponent from '../components/TextboxComponent.vue';
import common from '../common.js'
import axios from 'axios';
import {GetMyActivities, GetActivities} from '../api'
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
    const myActivities = GetActivities(4);
    const response = await axios.get(myActivities.path, myActivities.params);
    users.value = response.data;
    console.log(response)
  } catch (error) {
    console.error(error);
  }
});
</script>

<template>
  <GridComponent v-if="users !== null"  :display-data-source=users title="TestowyGrid" button-text="Wybierz"
    :button-type=common.buttonType.Accept @button-clicked="e => log(e)"></GridComponent>
    <div v-else class="d-flex justify-content-center align-items-center vh-100">
      <img src='../assets/progressBar.gif'  alt="Ładowanie danych..."/>
    </div>
  <TextboxComponent label="Hasło" placeholder="Podaj hasło" tooltip="Hasło nie może być krótsze niż 6 znaków"
    :is-password=true @text-changed="e => pwd = e"></TextboxComponent>
  <button @click="log(item1)"></button>
</template>

<style scoped></style>
