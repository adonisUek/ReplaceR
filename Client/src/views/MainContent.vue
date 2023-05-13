<script setup>
import { ref, reactive } from 'vue'
import GridComponent from '../components/GridComponent.vue';
import TextboxComponent from '../components/TextboxComponent.vue';
import common from '../common.js'
common.menuVisible = true;
let pwd="";
let item1 = undefined;
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

function log(text)
{
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

</script>

<template>
  <GridComponent :display-data-source=items title="TestowyGrid" button-text="Wybierz" :button-type=common.buttonType.Accept @button-clicked="e=>log(e)"></GridComponent>
  <TextboxComponent label="Hasło" placeholder="Podaj hasło" tooltip="Hasło nie może być krótsze niż 6 znaków" :is-password=true @text-changed="e=>pwd=e"></TextboxComponent>
  <button @click="log(item1)"></button>
</template>

<style scoped></style>
