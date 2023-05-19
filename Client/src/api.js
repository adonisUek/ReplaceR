const baseUrl = "http://localhost:5141/api/"


//activities

export function GetActivities() {
    return `${baseUrl}activity`
}

export function GetActivityById(id) {
    return `${baseUrl}activity/${id}`
}

export function GetMyActivities(id) {
    return `${baseUrl}activity/my/${id}`
}

export function CreateActivity() {
    return `${baseUrl}activity`
}

export function UpdateActivity() {
    return `${baseUrl}activity`
}

export function DeleteActivity(id) {
    return `${baseUrl}activity/my/${id}`
}

//users
export function AuthenticateUser() {
    return `${baseUrl}users/auth`
}

export function GetUser() {
    return `${baseUrl}users`
}

export function CreateUser() {
    return `${baseUrl}users`
}

export function UpdateUser() {
    return `${baseUrl}users`
}

export function DeleteUser(id) {
    return `${baseUrl}users/${id}`
}

