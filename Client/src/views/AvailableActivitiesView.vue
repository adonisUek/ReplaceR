<script setup>
import { ref, onMounted, toRefs, computed, toRaw } from 'vue'
import GridComponent from '../components/GridComponent.vue';
import common from '../common.js'
import axios from 'axios';
import { useRouter } from 'vue-router';
import { GetActivities, UpdateActivity } from '../api'
common.menuVisible = true;
const activities = ref(null);
let cities = ref([]);
let selectedCity = ref('');
const displayedData = [];
const router=useRouter();
let filteredDisplayedData = displayedData;

function Select(activity) {
  console.log(activity);
  console.log(selectedCity.value);
  try {
    let act = [];
    act = toRaw(activities.value);
    console.log(act);
    const userId = JSON.parse(localStorage.getItem('user')).id;
    const creatorId = act.find(a => a.id === activity.id).creator.id;
    const updateActivity = UpdateActivity(activity.id, 1, 2, creatorId, userId)
    axios.put(updateActivity.path, updateActivity.params);
    router.push('Reserved');
  }
  catch (error) {
    console.error(error);
    router.push('NotFound');
  }
}

const handleOptionChange = () => {
  if (selectedCity.value !== '')
    filteredDisplayedData = displayedData.filter(data => data.Miasto === selectedCity.value);
  return filteredDisplayedData;
};

const clearFilters = () => {
  filteredDisplayedData = displayedData;
  selectedCity.value = '';
};

onMounted(async () => {
  try {
    const localStorageData = localStorage.getItem('user');
    if (localStorageData === null || localStorageData === undefined)
      router.push({ name: 'LogIn' });
    const availableActivities = GetActivities(JSON.parse(localStorageData).id);
    const response = await axios.get(availableActivities.path, availableActivities.params);
    activities.value = response.data;
    const activitiesArray = toRefs(activities.value);
    activitiesArray.forEach(activity => {
      const dataAktywności = new Date(activity.value.date);
      const displayedActivity = {
        id: activity.value.id,
        Nazwa: activity.value.name,
        Data: `${dataAktywności.getDate().toString().padStart(2, '0')}-${(dataAktywności.getMonth() + 1).toString().padStart(2, '0')}-${dataAktywności.getFullYear()}, ${dataAktywności.getHours().toString().padStart(2, '0')}:${dataAktywności.getMinutes().toString().padStart(2, '0')}`,
        Miasto: activity.value.city,
        Adres: activity.value.address,
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
  <div id="filters">
    <div class="item">
      <label style="font-size: 20px;">Filtruj</label>
    </div>
    <div class="item">
      <label for="cities">Wybierz miasto:</label>
      <select id="cities" class="form-select" v-model="selectedCity" @change="handleOptionChange">
        <option v-for="city in cities" :value="city" :key="city">{{ city }}</option>
      </select>
    </div>
    <div class="item">
      <button :class=common.buttonType.Delete @click="clearFilters">Wyczyść filtry</button>
    </div>
  </div>
  <div class="grid">
    <GridComponent v-if="activities !== null" :display-data-source="handleOptionChange()" title="Dostępne zajęcia"
      button-text="Zarezerwuj" :button-type=common.buttonType.Accept @button-clicked="e => Select(e)"></GridComponent>
    <div v-else class="d-flex justify-content-center align-items-center vh-100">
      <img src='../assets/progressBar.gif' alt="Ładowanie danych..." />
    </div>
  </div>
</template>

<style scoped>
select {
  width: 50%;
  max-width: 300px;
  display: inline;
}

#filters {
  margin: 35px auto;
  justify-content: center;
  align-items: center;
}
.grid {
  border-top: 1px solid darkgrey;

}

label {
  padding: 5px;
}

.item {
  padding: 30px;
  display: inline;
}
</style>
