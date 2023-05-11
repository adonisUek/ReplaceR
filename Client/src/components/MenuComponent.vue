<script setup>
import { reactive, ref } from 'vue'
const items = reactive([
  {
    "link": 'My',
    "text": "Moje zapisy",
  },
  {
    "link": 'CreateActivity',
    "text": "Utwórz zamianę",
  },
  {
    "link": 'My',
    "text": "Moje zamiany",
  },
   {
     "link": 'AvailableActivities',
    "text": "Dostępne zamiany",
  },
   {
     "link": 'EditUser',
    "text": "Edycja konta",
  },
   {
     "link": 'LogIn',
    "text": "Wyloguj",
  }
]);
</script>
<template>
  <nav class="navbar navbar-expand-md bg-light">

<div class="container-fluid justify-content-center">
  <ul class="navbar-nav nav-fill w-100">
    <li v-for="item in items" class="nav-item">
      <router-link :to="{name: item.link}">{{item.text}}</router-link>
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
