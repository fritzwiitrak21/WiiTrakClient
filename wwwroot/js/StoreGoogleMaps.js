var map;
var MovingDlat;
var MovingDlon;
var marker;
var storemarker;
var Timer;
var watchid;
const StoreMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/StoreMarker.png";
const DriverMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/NavigationLogo.png";


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
        title: "Current Location",
        icon: DriverMarkerIcon
    });

    setInterval(function () {
        changeMarkerPosition();
    }, 2000);
}

//Map to Store
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
        icon: DriverMarkerIcon,
        optimized: false,
    });

    storemarker = new google.maps.Marker({
        position: new google.maps.LatLng(latitude, longitude),
        map,
        title: "Store",
        icon: StoreMarkerIcon,
        optimized: false,
    });

    marker.setAnimation(google.maps.Animation.BOUNCE);
    storemarker.setAnimation(google.maps.Animation.BOUNCE);
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

    directionsService.route(request, function (response, status) {

        if (status == 'OK') {
            directionsRenderer.setDirections(response);
            var directionsData = response.routes[0].legs[0]; // Get data about the mapped route
            if (directionsData) {
                document.getElementById('distance').innerText = " Driving distance is " + directionsData.distance.text + " (" + directionsData.duration.text + ").";
            }
            Timer = setInterval(function () {
                watchid = navigator.geolocation.watchPosition(GetCoordinates);
                changeMarkerPosition();
                StopWatch();
            }, 900);
        }
    });
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
    console.log("Store Watch" + watchid);
    console.log("Store Timer" +Timer);
    clearTimeout(Timer);
}
window.initMap = initMap;