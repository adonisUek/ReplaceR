<script setup>
import { reactive, ref } from 'vue'
const props = defineProps({
  visible: Boolean
});
const items = reactive([
  {
    "link": 'My',
    "text": "Zajęcia utworzone przeze mnie",
  },
  {
    "link": 'Reserved',
    "text": "Moje rezerwacje",
  },
  {
    "link": 'CreateActivity',
    "text": "Utwórz zajęcia",
  },
  {
    "link": 'AvailableActivities',
    "text": "Dostępne zajęcia",
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
  <div v-show = "props.visible">
  <nav class="navbar navbar-expand-md bg-light">
    <div class="container-fluid justify-content-center">
      <ul class="navbar-nav nav-fill w-100">
        <li v-for="item in items" class="nav-item">
          <router-link :to="{ name: item.link }">{{ item.text }}</router-link>
        </li>
      </ul>
    </div>
  </nav>
</div>
</template>

<style scoped>
.container-fluid {
  color: white;
}

ul {
  border-left: 1px solid lightgray;
}

ul li {
  border-right: 1px solid lightgray;
}

a {
  text-decoration: none;
  color: black;
}
</style>
