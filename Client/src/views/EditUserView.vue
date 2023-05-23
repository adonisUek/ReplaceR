<script setup>
import { ref, toRaw, onMounted } from 'vue';
import common from '../common.js'
import TextboxComponent from '../components/TextboxComponent.vue';
import { GetUser, UpdateUser } from '../api';
import axios from 'axios';
let user = ref(null);
import { useRouter } from 'vue-router';
common.menuVisible = true;
const router = useRouter();

const Accept = async () => {
  try {
    const updateUser = UpdateUser(user.value.id, user.value.firstName, user.value.lastName, user.value.mailAddress,user.value.phoneNumber,user.value.address,true);
    const response = await axios.put(updateUser.path, updateUser.params);
    console.log(response);
    router.push({ name: 'Main' });
  } catch (error) {
    console.error(error);
  }
};

onMounted(async () => {
  try {
    const localStorageData = localStorage.getItem('user');
    const getUser = JSON.parse(localStorageData);
    const myUserData = GetUser(getUser.id);
    const response = await axios.get(myUserData.path, myUserData.params);
    user.value = response.data;
    console.log(response)
  } catch (error) {
    console.error(error);
  }
});

//TODO: CHECKBOX CO DO MAILI

</script>


<template>
  <p>{{user}}</p>
  <div class="justify-content-center">
    <div v-if="user!==null && user.firstName !== null">
      <h1>Edycja użytkownika</h1>
      <div class="tb">
        <TextboxComponent v-if="user !== null && user.firstName !== null" label="Imię" :started-value=user.firstName
          :is-password=false @text-changed="e => user.firstName = e"></TextboxComponent>
      </div>
      <div class="tb">
        <TextboxComponent v-if="user !== null && user.lastName !== null" label="Nazwisko" :started-value=user.lastName
          :is-password=false @text-changed="e => user.lastName = e"></TextboxComponent>
      </div>
      <div class="tb">
        <TextboxComponent v-if="user !== null && user.mailAddress !== null" label="Adres e-mail"
          :started-value=user.mailAddress :is-password=false @text-changed="e => user.mailAddress = e"></TextboxComponent>
      </div>
      <div class="tb">
        <TextboxComponent v-if="user !== null && user.phoneNumber !== null" label="Numer telefonu"
          :started-value=user.phoneNumber :is-password=false @text-changed="e => user.phoneNumber = e"></TextboxComponent>
      </div>
      <div class="tb">
        <TextboxComponent v-if="user !== null && user.address !== null" label="Adres" :started-value=user.address
          :is-password=false @text-changed="e => user.address = e"></TextboxComponent>
      </div>
      <div class="button">
        <button :class=common.buttonType.Accept @click="Accept">Zatwierdź</button>
      </div>
    </div>
    <div v-else class="d-flex justify-content-center align-items-center vh-100">
      <img src='../assets/progressBar.gif'  alt="Ładowanie danych..."/>
    </div>
  </div>
</template>

<style scoped>
h1 {
  text-align: center;
  padding: 3%;
}
.button {
  margin-top: 2%;
  padding: 10px;
}

button {
  margin: 0 auto;
  display: block;
  width: 200px;
}

.tb {
  display: grid;
  justify-content: center;
  align-items: center;
  margin-bottom: 10px;
  margin-top: 5px;
}
</style>
