<script setup>
import { toRaw } from 'vue';
let selectedItem;
const props = defineProps({
  displayDataSource: Array,
  title: String,
  buttonText: String,
  buttonType: String,
});
const emit = defineEmits(['buttonClicked']);

async function ChangeSelection(value) {
  selectedItem = value;
  emit('buttonClicked', selectedItem);
}
</script>

<template>
  <div id="tableDiv">
    <h2>{{ title }} </h2>
    <table class="table">
      <thead>
        <tr>
          <th v-if="props.displayDataSource && props.displayDataSource.length > 0" v-for="property in Object.keys(displayDataSource[0])" scope="col">{{ property }}</th>
          <th v-else>BRAK DANYCH</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in displayDataSource">
          <td v-for="value in Object.values(item)">{{ value }} </td>
          <td><button :class=buttonType @click="ChangeSelection(toRaw(item))">{{ buttonText }}</button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
td {
  text-align: center;
}

th {
  text-align: center;
}

h2 {
  padding: 20px;
}

#tableDiv {
  margin-left: 5%;
  margin-right: 5%;
  padding: 50px;
}

table {
  table-layout: fixed;
}

thead {
  background-color: dimgray;
  color: whitesmoke;
}

button {
  opacity: 0.8;
  color: whitesmoke;
}

button:hover {
  cursor: pointer;
  opacity: 1;
}
</style>
