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
    const point = { lat: latitude, lng: longitude };

    // create marker
    //new google.maps.Marker({ position: point, map: map, icon: "/Image/clear.png", title: "hai" });
    marker = new google.maps.Marker({
        position: latlng,
        map,
        title: "Store Location",
        icon: "/Images/NavigationLogo.png"
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

    directionsService.route(request, function (response, status) {

        if (status == 'OK') {
            directionsRenderer.setDirections(response);
           
            Timer = setInterval(function () {
                watchid = navigator.geolocation.watchPosition(GetCoordinates);
            console.log(watchid);

                


              changeMarkerPosition();
            }, 1100);
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
    console.log("Watch end " + watchid);
}
export function StopTimer() {
    console.log(Timer);

    clearTimeout(Timer);
}
window.initMap = initMap;