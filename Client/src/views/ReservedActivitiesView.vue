<script setup>
import { ref, onMounted, toRefs, toRaw } from 'vue'
import common from '../common.js'
import GridComponent from '../components/GridComponent.vue';
import axios from 'axios';
import { useRouter } from 'vue-router';
import { GetMyActivities, UpdateActivity, DeleteActivity } from '../api'
common.menuVisible = true;
const activities = ref(null);
const reservedActivitiesByMe = [];
const router = useRouter();
const id = ref(null);

function Select(activity) {
  try {
    let act = [];
    act = toRaw(activities.value);
    const userId = JSON.parse(localStorage.getItem('user')).id;
    const creatorId = act.find(a => a.id === activity.id).creator.id;
    const updateActivity = UpdateActivity(activity.id, 2, 1, creatorId, userId)
    axios.put(updateActivity.path, updateActivity.params);
    router.push({name:'AvailableActivities'});
  }
  catch (error) {
    console.error(error);
    router.push({name:'NotFound'});
  }
}

onMounted(async () => {
  try {
    const localStorageData = localStorage.getItem('user');
    if (localStorageData === null || localStorageData === undefined)
      router.push({ name: 'LogIn' });
    id.value = JSON.parse(localStorage.getItem('user')).id;
    const myActivities = GetMyActivities(JSON.parse(localStorageData).id);
    const response = await axios.get(myActivities.path, myActivities.params);
    activities.value = response.data;
    const activitiesReservedByMe = toRaw(activities.value).filter(act => act.newUser && act.newUser.id === id.value && act.statusName === "Reserved");
    activitiesReservedByMe.forEach(activity => {
      const dataAktywności = new Date(activity.date);
      const displayedActivity = {
        id: activity.id,
        Nazwa: activity.name,
        Data: `${dataAktywności.getDate().toString().padStart(2, '0')}-${(dataAktywności.getMonth() + 1).toString().padStart(2, '0')}-${dataAktywności.getFullYear()}, ${dataAktywności.getHours().toString().padStart(2, '0')}:${dataAktywności.getMinutes().toString().padStart(2, '0')}`,
        Adres: `${activity.address}, ${activity.city}`,
        Utworzył: `${activity.creator.firstName} ${activity.creator.lastName} (${activity.creator.login})`,
      }
      reservedActivitiesByMe.push(displayedActivity);
    });
  } catch (error) {
    console.error(error);
    router.push({name:'NotFound'});
  }
});

</script>

<template>
  <div v-if="activities !== null">
    <GridComponent v-if="activities !== null" :display-data-source="reservedActivitiesByMe"
      title="Zarezerwowane przeze mnie" button-text="Anuluj rezerwację" :button-type=common.buttonType.Warning
      @button-clicked="e => Select(e)"></GridComponent>
  </div>
  <div v-else class="d-flex justify-content-center align-items-center vh-100">
    <img src='../assets/progressBar.gif' alt="Ładowanie danych..." />
  </div>
</template>

<style scoped></style>
