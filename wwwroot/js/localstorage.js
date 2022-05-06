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

export function getTextBoxValue() {
    var inputtext = $('input[type="text"].inputselect').val();
    return inputtext;
}

export function addValidationClass() {
    $('input[type="text"].inputselect').addClass("mud-input-error");
    $('#targetlabel').addClass("mud-input-error");
}
export function removeValidationClass() {
    $('input[type="text"].inputselect').removeClass("mud-input-error");
    $('#targetlabel').removeClass("mud-input-error");
}

export function updateCanvas() {
    var canvas = document.getElementById("ctlSignature");
    canvas.height = 130;
}

function  getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition,showError);
        
    } 
}

function showPosition(position) {
    saveCoord(position.coords.latitude +"##"+ position.coords.longitude);
}

function saveCoord(Coord) {
    localStorage.setItem("Coord", Coord);
}

var ShowError = false;

export function getCoord(ShowErrorMessage) {
    ShowError = false;
    ShowError = ShowErrorMessage;
    
getLocation();
return localStorage.getItem("Coord");

}
function showError(error) {
    if (ShowError) {
        alert("Grant permission to access the location");
    }
  
}

export function ClearCoord() {
    localStorage.removeItem("Coord");
}

export function GetSignatureStatus() {
    return $('#ctlSignature_status').text();
}

export function getSignCompleteStatus() {
    
    return $('#SuccessMessage').text();;
}

