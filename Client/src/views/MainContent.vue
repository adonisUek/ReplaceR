<script setup>
import common from '../common.js'
common.menuVisible = false;
</script>

<template>
  <h1> Witamy w aplikacji Replacer</h1>
  <div class="d-flex justify-content-center align-items-center vh-100 button">
    <div class="button">
      <button :class=common.buttonType.Accept><router-link :to="{ name: 'LogIn' }">Zaloguj się</router-link></button>
    </div>
    <div class="button">
      <button :class=common.buttonType.Info><router-link :to="{ name: 'CreateUser' }">Załóż konto</router-link></button>
    </div>
  </div>
</template>

<style scoped>
h1 {
  text-align: center;
  vertical-align: center;
  margin-top: 10%;
}

.button {
  margin-top: 2%;
  padding: 10px;
  display: block;
}

a {
  text-decoration: none;
  color: whitesmoke;
}

a:hover {
  color: white;
}
</style>
