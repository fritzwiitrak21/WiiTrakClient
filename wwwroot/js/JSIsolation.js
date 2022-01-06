export function showAlertWithJSIsolation(message) {
    alert(message);
}

export function callStaticDotNetMethod(name) {
    let promise = DotNet.invokeMethodAsync("WiiTrakClient", "BuildEmail", name);

    promise.then(email => alert(email));
}

export function callStaticDotNetMethodWithIdentifier(firstName, lastName) {
    let promise = DotNet.invokeMethodAsync("WiiTrakClient", "BuildEmailWithLastName", firstName, lastName);

    promise.then(email => alert(email));
}

export function callDotNetInstanceMethod(dotNetObjRef) {
    dotNetObjRef.invokeMethodAsync("SetWindowSize", {
        width: window.innerWidth,
        height: window.innerHeight
    });
}


export function registerResizeHandler(dotNetObjRef) {
    function resizeHandler() {
        dotNetObjRef.invokeMethodAsync("SetWindowSize", {
            width: window.innerWidth,
            height: window.innerHeight
        });
    }

    // set initial values
    resizeHandler();

    // register event handler
    window.addEventListener("resize", resizeHandler);
}

