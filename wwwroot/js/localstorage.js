export function saveUserId(UserId) {
    localStorage.setItem("UserId", UserId);
}

export function saveUserName(UserName) {
    localStorage.setItem("UserName", UserName);
}

export function saveUserRole(UserRole) {
    localStorage.setItem("UserRole", UserRole);
}

export function saveUserRoleId(UserRoleId) {
    localStorage.setItem("UserRoleId", UserRoleId);
}

export function getUserId() {
    return localStorage.getItem("UserId");
}

export function getUserName() {
    return localStorage.getItem("UserName");
}

export function getUserRole() {
    return localStorage.getItem("UserRole");
}

export function getUserRoleId() {
    return localStorage.getItem("UserRoleId");
}

export function clearSession(){
    localStorage.clear();
}


export function getStoreId(value) {
    var storeid = $('#textsearch [value="' + value + '"]').data('value');
    return storeid;
}



export function updateCanvas() {
    var canvas = document.getElementById("ctlSignature");
    canvas.height = 130;
}


