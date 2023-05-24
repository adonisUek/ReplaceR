<script setup>
import { ref, onMounted, toRefs, computed } from 'vue'
import GridComponent from '../components/GridComponent.vue';
import common from '../common.js'
import axios from 'axios';
import { GetActivities } from '../api'
common.menuVisible = true;
const activities = ref(null);
let cities = ref([]);
const displayedData = [];

function log(activity) {
  console.log(activity);
  console.log(uniqueArray);
  try {
    //const updateActivity = UpdateActivity(activity.id, 1,2, 1, 1)
    //axios.put(updateActivity.path, updateActivity.params);
  }
  catch (error) {
    console.error(error);
  }
  //location.reload();//do przeładowania strony jeszcze raz
}

onMounted(async () => {
  try {
    const localStorageData = localStorage.getItem('user');
    const availableActivities = GetActivities(JSON.parse(localStorageData).id);
    const response = await axios.get(availableActivities.path, availableActivities.params);
    activities.value = response.data;
    console.log(response)
    const activitiesArray = toRefs(activities.value);
    activitiesArray.forEach(activity => {
      console.log(activity.value);
      const dataAktywności = new Date(activity.value.date);
      const displayedActivity = {
        id: activity.value.id,
        Nazwa: activity.value.name,
        Data: `${dataAktywności.getDate().toString().padStart(2, '0')}-${(dataAktywności.getMonth() + 1).toString().padStart(2, '0')}-${dataAktywności.getFullYear()}, ${dataAktywności.getHours().toString().padStart(2, '0')}:${dataAktywności.getMinutes().toString().padStart(2, '0')}`,
        Adres: activity.value.address,
        Miasto: activity.value.city,
        Twórca: `${activity.value.creator.firstName} ${activity.value.creator.lastName} (${activity.value.creator.login})`,
      }
      if (!cities.value.some(city => city === activity.value.city))
        cities.value.push(activity.value.city);
      displayedData.push(displayedActivity);
    });
  } catch (error) {
    console.error(error);
  }
});
</script>

<template>
  <select v-model="cities">
    <option v-for="city in cities" :key="city">{{ city }}</option>
  </select>
  <GridComponent v-if="activities !== null" :display-data-source=displayedData title="Dostępne zajęcia"
    button-text="Wybierz" :button-type=common.buttonType.Accept @button-clicked="e => log(e)"></GridComponent>
  <div v-else class="d-flex justify-content-center align-items-center vh-100">
    <img src='../assets/progressBar.gif' alt="Ładowanie danych..." />
  </div>
</template>

<style scoped></style>
