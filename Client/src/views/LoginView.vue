<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import TextboxComponent from '../components/TextboxComponent.vue';
import common from '../common.js'
import axios from 'axios';
import { AuthenticateUser } from '../api';

common.menuVisible = false;
localStorage.clear();
const errorMessage = ref(null);
const disabled = ref(true);
let login = "";
let password = "";
const router = useRouter();

function checkDisabled() {
  if (login === "" || password === "")
    disabled.value = true;
  else
    disabled.value = false;
}

const LogIn = async () => {
  try {
    const auth = AuthenticateUser(login, password);
    const response = await axios.get(auth.path, auth.params);
    console.log(response);
    localStorage.setItem("user", JSON.stringify(response.data));
    router.push({ name: 'AvailableActivities' });
  } catch (error) {
    console.error(error);
    errorMessage.value = error.response.data
  }
};
</script>

<template>
  <div class="d-flex justify-content-center align-items-center vh-100">
    <div>
      <TextboxComponent :is-password=false label="Login" placeholder="Wpisz login..."
        @text-changed="e => { login = e; checkDisabled() }">
      </TextboxComponent>
      <TextboxComponent :is-password=true label="Hasło" placeholder="Wpisz hasło..."
        @text-changed="e => { password = e; checkDisabled()}">
      </TextboxComponent>
      <p style="text-align: center; margin-top: 10px;" v-if="errorMessage">{{ errorMessage }}</p>
      <div class="button">
        <button :class=common.buttonType.Accept :disabled=disabled @click="LogIn">Zaloguj</button>
      </div>
      <div class="button">
        <small class="form-text text-muted">Nie posiadasz jeszcze konta?</small>
        <button :class=common.buttonType.Info><router-link :to="{ name: 'CreateUser' }">Załóż konto</router-link></button>
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

.button {
  margin-top: 2%;
  padding: 10px;
}

a {
  text-decoration: none;
  color: whitesmoke;
}

a:hover {
  color: white;
}
</style>