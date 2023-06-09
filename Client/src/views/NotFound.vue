<script setup>
import common from '../common.js'
common.menuVisible = localStorage.getItem('user') !== null;
</script>

<template>
  <div id="c1" class="justify-content-center align-items-center vh-100">
      <h1>Niestety, wystąpił błąd lub ta strona nie istnieje</h1>
      <button :class=common.buttonType.Delete><router-link :to="{ name: 'LogIn' }">Wyjdź z aplikacji</router-link></button>
  </div>
</template>

<style scoped>
#c1 {
  text-align: center;
  padding: 20%;
}

button {
  margin: 0 auto;
  margin-top: 30px;
  display: block;
}

a {
  text-decoration: none;
  color: whitesmoke;
}

a:hover {
  color: white;
}</style>
