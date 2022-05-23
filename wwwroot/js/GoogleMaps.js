var map;
var MovingDlat;
var MovingDlon;
var marker;
var Timer;
var watchid;
export function getGMaps(latitude, longitude, dlat, dlon) {
    var latlng = new google.maps.LatLng(latitude, longitude);
    MovingDlat = dlat;
    MovingDlon = dlon;
    var options = {
        zoom: 15, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,

    };

    map = new google.maps.Map(document.getElementById("map"), options);

    marker = new google.maps.Marker({
        position: latlng,
        map,
        title: "Store Location",
        icon: "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/NavigationLogo.png"
    });

    setInterval(function () {
        changeMarkerPosition();
    }, 2000);
}


export function initMap(latitude, longitude, dlat, dlon) {

    var latlng = new google.maps.LatLng(latitude, longitude);
    var directionsService = new google.maps.DirectionsService();
    var directionsRenderer = new google.maps.DirectionsRenderer();
    MovingDlat = dlat;
    MovingDlon = dlon;
    var options = {
        zoom: 16, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,

    };
    map = new google.maps.Map(document.getElementById("map"), options);
    const point = { lat: MovingDlat, lng: MovingDlon };

    // create marker
    marker = new google.maps.Marker({
        position: point,
        map,
        title: "Current Position",
        icon: "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/NavigationLogo.png"
    });

    directionsRenderer.setMap(map);
    
    calcRoute(directionsService, directionsRenderer, latitude, longitude, dlat, dlon);
}

function calcRoute(directionsService, directionsRenderer, latitude, longitude, dlat, dlon) {

    var start = new google.maps.LatLng(dlat, dlon);

    var end = new google.maps.LatLng(latitude, longitude);

    var request = {
        origin: start,
        destination: end,
        travelMode: 'DRIVING'
    };
    Timer = setInterval(function () {
        
    directionsService.route(request, function (response, status) {

        if (status == 'OK') {
            directionsRenderer.setDirections(response);
            var directionsData = response.routes[0].legs[0]; // Get data about the mapped route
            if (directionsData) {
                document.getElementById('distance').innerText = " Driving distance is " + directionsData.distance.text + " (" + directionsData.duration.text + ").";
            }
            //Timer = setInterval(function () {
            //    watchid = navigator.geolocation.watchPosition(GetCoordinates);
            //    changeMarkerPosition();
            //    StopWatch();
            //}, 1100);
        }
    });
        watchid = navigator.geolocation.watchPosition(GetCoordinates);
        changeMarkerPosition();
        StopWatch();
    }, 1100);
}

function GetCoordinates(position) {
    MovingDlat = position.coords.latitude;
    MovingDlon = position.coords.longitude;
    changeMarkerPosition();
    StopWatch();
}

function changeMarkerPosition() {
    var latlng = new google.maps.LatLng(MovingDlat, MovingDlon);
    marker.setPosition(latlng);
}
export function StopWatch() {
    navigator.geolocation.clearWatch(watchid);
    watchid = null;
}
export function StopTimer() {
    console.log(Timer);
    clearTimeout(Timer);
}
window.initMap = initMap;