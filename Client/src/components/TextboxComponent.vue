<script setup>
const props = defineProps({
  label: String,
  placeholder: String,
  tooltip: String,
  isPassword: Boolean,
  startedValue: String,
});
let inputText = props.startedValue ? props.startedValue : "";
const emit = defineEmits(['textChanged']);
function onInput(e)
{
  inputText = e.target.value
  emit('textChanged', inputText);
}

const blockEnter = (event) => {
  event.preventDefault();
};

const inputType = props.isPassword ? "password" : "text";
</script>
<template>
  <form>
    <div class="form-group" id="textboxDiv">
      <label>{{ label }}</label>
      <input :type=inputType class="form-control textbox" :value=startedValue v-model="inputText" :placeholder=placeholder @input="onInput" @keydown.enter="blockEnter">
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
