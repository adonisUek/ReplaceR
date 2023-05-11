<script setup>
import TextboxComponent from '../components/TextboxComponent.vue';
import common from '../common.js'
//todo: przy wejściu na ekran logowania następuje czyszczenie local storage
</script>

<template>
  <div class="d-flex justify-content-center align-items-center vh-100">
    <div>
      <TextboxComponent :is-password=false label="Login" placeholder="Wpisz login..."></TextboxComponent>
      <TextboxComponent :is-password=true label="Hasło" placeholder="Wpisz hasło..."></TextboxComponent>
      <div class="button">
        <button :class=common.buttonType.Accept>Zaloguj</button>
      </div>
      <div class="button">
        <small class="form-text text-muted">Nie posiadasz jeszcze konta?</small>
        <button :class=common.buttonType.Info>Załóż konto</button>
      </div>
      </div>  
    </div>
</template>

<style scoped>
button {
    margin: 0 auto;
    display: block;
    width: 100%;
}
.button
{
  margin-top: 2%;
  padding: 10px;
}
</style>
