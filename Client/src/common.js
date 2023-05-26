import { reactive } from 'vue'

export default reactive({
    buttonType: Object.freeze({
        Accept:   "btn btn-success",
        Delete:  "btn btn-danger",
        Warning: "btn btn-warning",
        Info:  "btn btn-primary"
    }),
    menuVisible: true
});