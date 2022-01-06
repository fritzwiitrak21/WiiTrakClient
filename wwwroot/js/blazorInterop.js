var blazorInterop = blazorInterop || {};

blazorInterop.showAlert = function(message) {
    alert(message);
};

blazorInterop.lotToConsoleTable = function(obj) {
    console.table(obj);
};

blazorInterop.showPrompt = function (message, defaultValue) {
    return prompt(message, defaultValue);
};

blazorInterop.createDriver = function (firstName, lastName) {
    return { firstName, lastName, email: firstName + lastName + "@jsinteropplay.com" };
};

blazorInterop.throwError = function () {
    throw Error("An error has occurred!");
};