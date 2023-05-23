<script setup>
import { ref, toRaw, onMounted } from 'vue';
import common from '../common.js'
import TextboxComponent from '../components/TextboxComponent.vue';
import { GetUser } from '../api';
import axios from 'axios';
let user = ref(null);
common.menuVisible = true;


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

</script>


<template>
  <div class="d-flex justify-content-center align-items-center vh-100">
    <div>
      <h1>Edycja użytkownika</h1>
      <p>{{ user }}</p>
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
    </div>
  </div>
</template>

<style scoped>
h1 {
  text-align: center;
}

.tb {
  display: grid;
  justify-content: center;
  align-items: center;
  margin-bottom: 10px;
  margin-top: 5px;
}
</style>
