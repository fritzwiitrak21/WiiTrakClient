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

export function saveUserPassword(Password) {
    localStorage.setItem("UserPassword", Password);
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

export function getUserPassword() {
    
    return localStorage.getItem("UserPassword");
}

export function clearSession(){
    localStorage.clear();
}

export function getStoreId(value) {
    return $('#textsearch [value="' + value + '"]').data('value');
}

export function getTextBoxValue() {
    return $('input[type="text"].inputselect').val();
}

export function addValidationClass() {
    $('input[type="text"].inputselect').addClass("mud-input-error");
    $('#targetlabel').addClass("mud-input-error");
}
export function removeValidationClass() {
    $('input[type="text"].inputselect').removeClass("mud-input-error");
    $('#targetlabel').removeClass("mud-input-error");
}

export function addTextValidationClass() {
    $('input[type="text"].inputtext').addClass("mud-input-error");
    $('#NoOfCartLabel').addClass("mud-input-error");
}
export function removeTextValidationClass() {
    $('input[type="text"].inputtext').removeClass("mud-input-error");
    $('#NoOfCartLabel').removeClass("mud-input-error");
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
  
}
export function getCoordinates() {
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
    
    return $('#SuccessMessage').text();
}
export function addValidationCountycodeClass() {
    $('input[type="text"].inputselect').addClass("mud-input-error");
    $('input[type="text"].inputselect').addClass("countycode");
    $('#targetlabel').addClass("mud-input-error");
    $('input[type="text"].inputselect').addClass("my-6");
    $('input[type="text"].inputselect').removeClass("my-10");
}
export function removeValidationCountycodeClass() {
    $('input[type="text"].inputselect').removeClass("mud-input-error");
    $('input[type="text"].inputselect').removeClass("countycode");
    $('#targetlabel').removeClass("mud-input-error");
    $('input[type="text"].inputselect').removeClass("my-10");
    $('input[type="text"].inputselect').addClass("my-6");
}
export function BlazorDownloadFile(filename, content) {
    const file = new File([content], filename, { type: "application/octet-stream" });
    const exportUrl = URL.createObjectURL(file);
    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = exportUrl;
    a.download = filename;
    a.target = "_self";
    a.click();
    URL.revokeObjectURL(exportUrl);
}
export function ShowRadioListClass() {
    $('#RadioList').addClass("ShowRadioList");
    $('#RadioList').removeClass("HideRadioList");
}
export function ShowdivPreviewClass() {
    $('#divPreview').addClass("ShowPreview");
    $('#divPreview').removeClass("HidePreview");
}
export function HidedivPreviewClass() {
    $('#divPreview').removeClass("ShowPreview");
    $('#divPreview').addClass("HidePreview");
}
