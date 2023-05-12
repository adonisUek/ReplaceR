<script setup>
import MenuComponent from './components/MenuComponent.vue';
import common from './common';
</script>

<template>
  <MenuComponent :visible=common.menuVisible></MenuComponent>
  <router-view/>
</template>

<style scoped>

</style>
