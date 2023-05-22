<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import TextboxComponent from '../components/TextboxComponent.vue';
import common from '../common.js'
import axios from 'axios';
import { AuthenticateUser } from '../api';

const apiResult = ref(null);
const errorMessage = ref(null);
common.menuVisible = false;
let login = "";
let password = "";
const router = useRouter();

const LogIn = async () => {
  try {
    const auth = AuthenticateUser(login, password);
    const response = await axios.get(auth.path, auth.params);
    console.log(response);
    router.push({name: 'Main'});
  } catch (error) {
    console.error(error);
    errorMessage.value = error.response.data
    apiResult.value = 'error';
    }
  };

//todo: przy wejściu na ekran logowania następuje czyszczenie local storage
</script>

<template>
  <div class="d-flex justify-content-center align-items-center vh-100">
    <div>
      
      <TextboxComponent :is-password=false label="Login" placeholder="Wpisz login..." @text-changed="e=>login=e"></TextboxComponent>
      <TextboxComponent :is-password=true label="Hasło" placeholder="Wpisz hasło..." @text-changed="e=>password=e"></TextboxComponent>
      <p style="text-align: center; margin-top: 10px;" v-if="errorMessage">{{ errorMessage }}</p>
      <router-link v-else :to="{ name: 'Main' }"></router-link>
      <div class="button">
        <button :class=common.buttonType.Accept @click="LogIn">Zaloguj
          <!--

            <router-link v-if="apiResult === 'success'" :to="{ name: 'Main' }"></router-link>
            <router-link v-if="apiResult === 'error'" :to="{ name: 'NotFound' }"></router-link>
          -->
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
