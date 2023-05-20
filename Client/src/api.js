const baseUrl = "http://localhost:5141/api/"


//activities
export function GetActivities(id) {
    return { 
        path: `${baseUrl}activity`,
        params: { params: { userId: id } }
    }
}

export function GetActivityById(id) {
    return `${baseUrl}activity/${id}`
}

export function GetMyActivities(id) {
    return { 
        path: `${baseUrl}activity/my/${id}`,
        params: null
    }
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

