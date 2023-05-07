<script setup>
import { reactive, ref } from 'vue'
const items = reactive([
  {
    "text": "Moje zapisy",
    "link": "link",
  },
  {
    "text": "Utwórz zamianę",
    "link": "link",
  },
  {
    "text": "Moje zamiany",
    "link": "link",
  },
   {
    "text": "Dostępne zamiany",
    "link": "link",
  },
   {
    "text": "Edycja konta",
    "link": "link",
  },
   {
    "text": "Wyloguj",
    "link": "link",
  }
]);
</script>
<template>
  <nav class="navbar navbar-expand-md bg-light">

<div class="container-fluid justify-content-center">
  <ul class="navbar-nav nav-fill w-100">
    <li v-for="item in items" class="nav-item">
      <a class="nav-link" href="#">{{item.text}}</a>
    </li>
  </ul>
</div>

</nav>
</template>

<style scoped>
.container-fluid
{
  color: white;
}

ul li:hover
{
  cursor: pointer;
}

ul {
  border-left: 1px solid black;
}

ul li {
  border-right: 1px solid black;
}
</style>
