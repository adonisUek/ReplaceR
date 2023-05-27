<script setup>
import { ref, toRaw, onMounted } from 'vue';
import common from '../common.js'
import TextboxComponent from '../components/TextboxComponent.vue';
import { CreateUser } from '../api';
import axios from 'axios';
import { useRouter } from 'vue-router';
let user = ref(null);
user.value = {
  login: '',
  password: '',
  firstName: '',
  lastName: '',
  mailAddress: '',
  phoneNumber: '',
  address: '',
  isEmailNotificationsAllowed: false
};
let title = ref('Utwórz użytkownika');
common.menuVisible = false;
const router = useRouter();
const disabled = ref(true);

function checkDisabled() {
  if (user.value.login === '' || user.value.password.length < 4 || user.value.firstName === '' || user.value.lastName === ''
    || JSON.stringify(user.value.mailAddress).includes('@') === false || user.value.phoneNumber.length < 6 || user.value.address === '')
    disabled.value = true;
  else
    disabled.value = false;
}

const Accept = async () => {
  try {
    const createUser = CreateUser(user.value.login, user.value.password, user.value.firstName, user.value.lastName, user.value.mailAddress, user.value.phoneNumber, user.value.address, user.value.isEmailNotificationsAllowed);
    const response = await axios.post(createUser.path, createUser.params);
    console.log(response);
    router.push({ name: 'UserAdded' });
  } catch (error) {
    title.value = "Wystąpił błąd podczas tworzenia użytkownika: " + error.response.data
    console.error(error);
  }
};
</script>

<template>
  <div class="main">
    <h1>{{ title }}</h1>
    <div class="tb">
      <TextboxComponent label="Login" :started-value=user.login :tooltip="user.login === '' ? 'Zbyt krótki login' : ''"
        :is-password=false @text-changed="e => { user.login = e; checkDisabled() }"></TextboxComponent>
    </div>
    <div class="tb">
      <TextboxComponent label="Hasło" :started-value=user.password
        :tooltip="user.password.length < 4 ? 'Hasło musi mieć min. 4 znaki' : ''" :is-password=true
        @text-changed="e => { user.password = e; checkDisabled() }"></TextboxComponent>
    </div>
    <div class="tb">
      <TextboxComponent label="Imię" :started-value=user.firstName
        :tooltip="user.firstName === '' ? 'Zbyt krótkie imię' : ''" :is-password=false
        @text-changed="e => { user.firstName = e; checkDisabled() }"></TextboxComponent>
    </div>
    <div class="tb">
      <TextboxComponent label="Nazwisko" :started-value=user.lastName
        :tooltip="user.lastName === '' ? 'Zbyt krótkie nazwisko' : ''" :is-password=false
        @text-changed="e => { user.lastName = e; checkDisabled() }"></TextboxComponent>
    </div>
    <div class="tb">
      <TextboxComponent label="Adres e-mail"
        :tooltip="JSON.stringify(user.mailAddress).includes('@') === false ? 'Nieprawidłowy email' : ''"
        :started-value=user.mailAddress :is-password=false @text-changed="e => { user.mailAddress = e; checkDisabled() }">
      </TextboxComponent>
    </div>
    <div class="tb">
      <TextboxComponent label="Numer telefonu" :tooltip="user.phoneNumber.length < 6 ? 'Nieprawidłowy nr tel' : ''"
        :started-value=user.phoneNumber :is-password=false @text-changed="e => { user.phoneNumber = e; checkDisabled() }">
      </TextboxComponent>
    </div>
    <div class="tb">
      <TextboxComponent v-if="user !== null && user.address !== null" label="Adres" :started-value=user.address
        :tooltip="user.address.length === 0 ? 'Nieprawidłowy adres' : ''" :is-password=false
        @text-changed="e => { user.address = e; checkDisabled() }"></TextboxComponent>
    </div>
    <div class="cb">
      <input type="checkbox" v-model="user.isEmailNotificationsAllowed">
      <label>Zgoda na powiadomienia e-mail od aplikacji</label>
    </div>
    <div class="button">
      <button :class=common.buttonType.Accept :disabled=disabled @click="Accept">Dodaj użytkownika</button>
    </div>
  </div>
</template>

<style scoped>
h1 {
  text-align: center;
  margin-top: 2%;
  margin-bottom: 1%;
}

.button {
  margin-top: 2%;
  padding: 10px;
}

input,
label {
  display: inline-block;
}

.cb {
  margin-top: 20px;
  text-align: center;
  align-items: center;
  justify-content: center;
}

button {
  margin: 0 auto;
  display: block;
  width: 200px;
}

.main {
  display: block;
  margin-left: auto;
  margin-right: auto;
}

.tb {
  justify-content: center;
  display: flex;
  min-width: 150px;
  margin: 0 auto;
  align-items: center;
  margin-bottom: 10px;
  margin-top: 5px;
}
</style>
