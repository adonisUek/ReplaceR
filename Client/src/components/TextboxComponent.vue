<script setup>
const props = defineProps({
  label: String,
  placeholder: String,
  tooltip: String,
  isPassword: Boolean
});
const inputType = props.isPassword ? "password" : "email";
</script>
<template>
  <form>
    <div class="form-group" id="textboxDiv">
      <label>{{ label }}</label>
      <input :type=inputType class="form-control textbox" :placeholder=placeholder>
      <small class="form-text text-muted">{{ tooltip }}</small>
    </div>
  </form>
</template>
    
<style scoped>
.textbox {
  max-width: 300px;
}

#textboxDiv {
  margin-left: 5%;
  margin-right: 5%;
}
</style>
