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

function loadJs(sourceUrl) {
	if (sourceUrl.Length == 0) {
		console.error("Invalid source URL");
		return;
	}

	var tag = document.createElement('script');
	tag.src = sourceUrl;
	tag.type = "text/javascript";

	tag.onload = function () {
		console.log("Script loaded successfully");
	}

	tag.onerror = function () {
		console.error("Failed to load script");
	}

	document.body.appendChild(tag);
}