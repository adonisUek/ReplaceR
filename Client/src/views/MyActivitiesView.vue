<script setup>
import { ref, onMounted, toRefs, computed, toRaw } from 'vue'
import common from '../common.js'
import GridComponent from '../components/GridComponent.vue';
import axios from 'axios';
import { useRouter } from 'vue-router';
import { GetMyActivities, UpdateActivity, DeleteActivity } from '../api'
common.menuVisible = true;
const activities = ref(null);
const notReservedActivitiesCreatedByMe = [];
const reservedActivitiesCreatedByMe = [];
const router = useRouter();
const id = ref(null);

function Delete(activity) {
  try {
    const deleteActivity = DeleteActivity(activity.id);
    axios.delete(deleteActivity.path, deleteActivity.params);
    location.reload();
  }
  catch (error) {
    console.error(error);
    router.push('NotFound');
  }
}

function Cancel(activity) {
  try {
    let act = [];
    act = toRaw(activities.value);
    const userId = JSON.parse(localStorage.getItem('user')).id;
    const newUserId = act.find(a => a.id === activity.id).newUser.id;
    const updateActivity = UpdateActivity(activity.id, 2, 3, userId, newUserId)
    axios.put(updateActivity.path, updateActivity.params);
    router.push('AvailableActivities');
  }
  catch (error) {
    console.error(error);
    router.push('NotFound');
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
    const activitiesCreated = toRaw(activities.value).filter(act => act.creator.id === id.value && act.statusName === "Available");
    const reservedActivities = toRaw(activities.value).filter(act => act.creator.id === id.value && act.statusName === "Reserved");

    activitiesCreated.forEach(activity => {
      const dataAktywności = new Date(activity.date);
      const displayedActivity = {
        id: activity.id,
        Nazwa: activity.name,
        Data: `${dataAktywności.getDate().toString().padStart(2, '0')}-${(dataAktywności.getMonth() + 1).toString().padStart(2, '0')}-${dataAktywności.getFullYear()}, ${dataAktywności.getHours().toString().padStart(2, '0')}:${dataAktywności.getMinutes().toString().padStart(2, '0')}`,
        Adres: `${activity.address}, ${activity.city}`,
        Zarezerwował: null
      }
      notReservedActivitiesCreatedByMe.push(displayedActivity);
    });
    reservedActivities.forEach(activity => {
      const dataAktywności = new Date(activity.date);
      const displayedActivity = {
        id: activity.id,
        Nazwa: activity.name,
        Data: `${dataAktywności.getDate().toString().padStart(2, '0')}-${(dataAktywności.getMonth() + 1).toString().padStart(2, '0')}-${dataAktywności.getFullYear()}, ${dataAktywności.getHours().toString().padStart(2, '0')}:${dataAktywności.getMinutes().toString().padStart(2, '0')}`,
        Adres: `${activity.address}, ${activity.city}`,
        Zarezerwował: `${activity.newUser.firstName} ${activity.newUser.lastName} (${activity.newUser.login})`,
      }
      reservedActivitiesCreatedByMe.push(displayedActivity);
    });
  } catch (error) {
    console.error(error);
  }
});

</script>

<template>
  <div v-if="activities !== null">
    <GridComponent :display-data-source="notReservedActivitiesCreatedByMe"
      title="Jeszcze nie zarezerwowane" button-text="Usuń zajęcia" :button-type=common.buttonType.Delete
      @button-clicked="e => Delete(e)"></GridComponent>
    <GridComponent v-if="activities !== null" :display-data-source="reservedActivitiesCreatedByMe"
      title="Zarezerwowane" button-text="Odwołaj zajęcia" :button-type=common.buttonType.Warning
      @button-clicked="e => Cancel(e)"></GridComponent>
  </div>
  <div v-else class="d-flex justify-content-center align-items-center vh-100">
    <img src='../assets/progressBar.gif' alt="Ładowanie danych..." />
  </div>
</template>

<style scoped></style>
