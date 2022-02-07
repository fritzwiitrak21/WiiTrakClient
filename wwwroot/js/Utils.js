function consoleLog(message) {
    console.log(message);
}

function focusInput(id) {
    if (document.getElementById(id) !== null) {
        document.getElementById(id).focus();
    }
}

function showAlert(message) {
    alert(message);
}

function BlazorFocusElement(element) {
    if (element instanceof HTMLElement) {
        element.focus();
    }
}

