<script setup>
import TextboxComponent from '../components/TextboxComponent.vue';
import common from '../common.js'
import daneTestowe from '../daneTestowe';
import { toRaw } from 'vue';

common.menuVisible = false;
let login = "Admin";
let password = "Admin";
const user = toRaw(daneTestowe.userToLog);
console.log(user);
function log(login1, haslo1)
{
  login1=login;
  haslo1=password;
  console.log("login "+ login1);
  console.log("hasło "+ haslo1);
  if(login1 === daneTestowe.userToLog.Login && haslo1 === daneTestowe.userToLog.Password){
    return true;
  }
  else return false;
}

//todo: przy wejściu na ekran logowania następuje czyszczenie local storage
</script>

<template>
  <div class="d-flex justify-content-center align-items-center vh-100">
    <div>
      <TextboxComponent :is-password=false label="Login" placeholder="Wpisz login..." @text-changed="e=>login=e"></TextboxComponent>
      <TextboxComponent :is-password=true label="Hasło" placeholder="Wpisz hasło..." @text-changed="e=>password=e"></TextboxComponent>
      <div class="button">
        <button :class=common.buttonType.Accept>
          <router-link v-if="login === user.Login && password === user.Password" :to="{name: 'My'}">Zaloguj</router-link>
          <router-link v-else :to="{name: 'NotFound'}">Zaloguj</router-link>
        </button>
      </div>
      <div class="button">
        <small class="form-text text-muted">Nie posiadasz jeszcze konta?</small>
        <button :class=common.buttonType.Info><router-link :to="{name: 'CreateUser'}">Załóż konto</router-link></button>
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
a { 
  text-decoration: none;
  color: whitesmoke; 
  }
  a:hover{
    color: white;
  }
</style>
