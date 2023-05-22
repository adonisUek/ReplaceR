<script setup>
import { ref, onMounted, toRefs} from 'vue'
import GridComponent from '../components/GridComponent.vue';
import TextboxComponent from '../components/TextboxComponent.vue';
import common from '../common.js'
import axios from 'axios';
import {GetActivities, UpdateActivity} from '../api'
common.menuVisible = true;
const activities = ref(null);
const displayedData = [];
const userId = 1;
const isScriptLoaded = ref(false);

function log(activity) {
  console.log(activity);
  try {
  const updateActivity = UpdateActivity(activity.id, 1,2, 1, 1)
  axios.put(updateActivity.path, updateActivity.params);
  }
  catch (error) {
    console.error(error);
  }
  //location.reload();//do przeładowania strony jeszcze raz
}

onMounted(async () => {
  try {
    const myActivities = GetActivities(4);
    const response = await axios.get(myActivities.path, myActivities.params);
    activities.value = response.data;
    console.log(response)
    const activitiesArray = toRefs(activities.value);
    activitiesArray.forEach(activity => {
      console.log(activity.value);
      const displayedActivity = {
        id: activity.value.id,
        Nazwa: activity.value.name,
        Data: new Date(activity.value.date),
        Twórca: `${activity.value.creator.firstName} ${activity.value.creator.lastName} (${activity.value.creator.login})`,
        NowyUzytkownik: activity.value.newUser ? `${activity.value.newUser.firstName} ${activity.value.newUser.lastName} (${activity.value.newUser.login})` : ''
      }
      displayedData.push(displayedActivity);
    });
  } catch (error) {
    console.error(error);
  }
});
</script>

<template>
  <GridComponent v-if="activities !== null"  :display-data-source=displayedData title="TestowyGrid" button-text="Wybierz"
    :button-type=common.buttonType.Accept @button-clicked="e => log(e)"></GridComponent>
    <div v-else class="d-flex justify-content-center align-items-center vh-100">
      <img src='../assets/progressBar.gif'  alt="Ładowanie danych..."/>
    </div>
  <TextboxComponent label="Hasło" placeholder="Podaj hasło" tooltip="Hasło nie może być krótsze niż 6 znaków"
    :is-password=true @text-changed="e => pwd = e"></TextboxComponent>
  <button @click="log(item1)"></button>
</template>

<style scoped></style>
