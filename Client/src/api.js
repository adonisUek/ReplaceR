const baseUrl = "http://localhost:5141/api/"

//ACTIVITIES

//get
export function GetActivities(id) {
    return {
        path: `${baseUrl}activity`,
        params: { params: { userId: id } }
    }
}

//get
export function GetActivityById(id) {
    return {
        path: `${baseUrl}activity/${id}`,
        params: null
    }
}

//get
export function GetMyActivities(id) {
    return {
        path: `${baseUrl}activity/my/${id}`,
        params: null
    }
}

//post
export function CreateActivity(name, date, creatorId, city, address) {
    return {
        path: `${baseUrl}activity`,
        params: {name:name, date:date, creatorId:creatorId, city:city, address:address}
    }
}

//put
export function UpdateActivity(id, oldStatusId, newStatusId, oldUserId, newUserId) {
    return {
        path: `${baseUrl}activity/${id}`,
        params: {oldStatusId:oldStatusId, newStatusId:newStatusId, oldUserId:oldUserId, newUserId:newUserId}
    }
}

//delete
export function DeleteActivity(id) {
    return {
        path: `${baseUrl}activity/${id}`,
        params: null
    }
}

//---------------------------------------------------//
//USERS

//get
export function AuthenticateUser(login, password) {
    return {
        path: `${baseUrl}users/auth`,
        params: { params: { login: login, password: password } }
    }
}

//get
export function GetUser(id) {
    return {
        path: `${baseUrl}users/${id}`,
        params: null
    }
}

//post
export function CreateUser(login, password, firstName, lastName, mailAddress, phoneNumber, address, isEmailNotificationsAllowed) {
    return {
        path: `${baseUrl}users`,
        params: { login:login, password:password, firstName:firstName, lastName:lastName, mailAddress:mailAddress, phoneNumber:phoneNumber, address:address, isEmailNotificationsAllowed:isEmailNotificationsAllowed}
    }
}

//put
export function UpdateUser(id, firstName, lastName, mailAddress, phoneNumber, address, isEmailNotificationsAllowed) {
    return {
        path: `${baseUrl}users/${id}`,
        params: {firstName:firstName, lastName:lastName, mailAddress:mailAddress, phoneNumber:phoneNumber, address:address, isEmailNotificationsAllowed:isEmailNotificationsAllowed}
    }
}

//delete
export function DeleteUser(id) {
    return {
        path: `${baseUrl}users/${id}`,
        params: null
    }
}