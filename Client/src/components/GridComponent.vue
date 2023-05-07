<script setup>
import { toRaw, ref } from 'vue';
let active = ref(false);
let selectedItem;
const props = defineProps({
  displayData: Array,
  title: String
});
function ChangeSelection(value) {
  this.active = !this.active;
  if (this.active) {
    selectedItem = value;
  }
  else {
    selectedItem = null;
  }
  console.log(selectedItem);
}
</script>

<template>
  <div id="tableDiv">
    <h2>{{ title }} </h2>
    <table class="table">
      <thead>
        <tr>
          <th v-for="property in Object.keys(displayData[0])" scope="col">{{ property }}</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in displayData" @click="ChangeSelection(toRaw(item))" :style="{
          backgroundColor: active && toRaw(item) === selectedItem ? 'yellow' : 'white',
        }">
          <td v-for="value in Object.values(item)">{{ value }} </td>
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
table:hover {
  cursor: pointer;
}
thead {
  background-color: dimgray;
  color: whitesmoke;
}
</style>
