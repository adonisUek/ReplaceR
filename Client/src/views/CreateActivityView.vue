<script setup>
import common from '../common.js'
common.menuVisible = true;
import { ref } from 'vue';
import axios from 'axios';
import { CreateActivity } from '../api';
import { useRouter } from 'vue-router';
import TextboxComponent from '../components/TextboxComponent.vue';
const router = useRouter();
let title = ref('Utwórz nowe zajęcia');
const selectedDate = ref(new Date().toISOString().slice(0, 16));
let activity = ref(null);
activity.value = {
  name: '',
  creatorId: localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).id : 0,
  city: '',
  address: '',
};

const Accept = async () => {
  try {
    const createActivity = CreateActivity(activity.value.name, selectedDate.value, activity.value.creatorId, activity.value.city, activity.value.address);
    const response = await axios.post(createActivity.path, createActivity.params);
    console.log(response);
    router.push({ name: 'My' });
  } catch (error) {
    title.value = "Wystąpił błąd podczas tworzenia zajeć: " + error.response.data
    console.error(error);
  }
};

const disabled = ref(true);

function checkDisabled() {
  console.log(selectedDate.value + "changed")
  if (selectedDate.value === null || selectedDate.value === undefined || activity.value.name === ""
    || activity.value.city === "" || activity.value.creatorId === 0 || activity.value.address === "")
    disabled.value = true;
  else
    disabled.value = false;
}

</script>

<template>
  <div style="justify-content: center;">
    <h1>{{ title }}</h1>
    <div class="control">
      <TextboxComponent label="Nazwa zajęć" :started-value=activity.name
        :tooltip="activity.name === '' ? 'Zbyt krótka nazwa' : ''" :is-password=false
        @text-changed="e => { activity.name = e; checkDisabled() }"></TextboxComponent>
    </div>
    <div class="control">
      <TextboxComponent label="Adres" :started-value=activity.address
        :tooltip="activity.address === '' ? 'Adres zbyt krótki' : ''" :is-password=false
        @text-changed="e => { activity.address = e; checkDisabled() }"></TextboxComponent>
    </div>
    <div class="control">
      <TextboxComponent label="Miasto" :started-value=activity.city
        :tooltip="activity.city === '' ? 'Zbyt krótka nazwa miasta' : ''" :is-password=false
        @text-changed="e => { activity.city = e; checkDisabled() }"></TextboxComponent>
    </div>
    <div>
      <label class="control tbDiv">Termin zajęć</label>
      <input class="form-control control tbDiv" type="datetime-local" v-model="selectedDate" @change="checkDisabled()" />
    </div>
    <div class="tbDiv button">
      <button :class=common.buttonType.Accept :disabled="disabled" @click="Accept">Dodaj zajęcia</button>
    </div>
  </div>
</template>

<style scoped>
h1 {
  text-align: center;
  padding: 3%;
}
.control {
  display: block;
  justify-content: center;
  align-items: center;
  max-width: 300px;
  padding: 10px;
}

.tbDiv {
  margin-left: 1%;
  margin-right: 1%;
}

.button {
  margin-top: 2%;
  padding: 10px;
}
</style>
