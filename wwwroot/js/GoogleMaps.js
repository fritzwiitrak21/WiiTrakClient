export function getGMaps(latitude, longitude, dlat, dlon) {
    var latlng = new google.maps.LatLng(latitude, longitude);

    var options = {
        zoom: 17, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,

    };
    var map = new google.maps.Map(document.getElementById("map"), options);
}
export function initMap(latitude, longitude, dlat, dlon) {

    var latlng = new google.maps.LatLng(latitude, longitude);
    var directionsService = new google.maps.DirectionsService();
    var directionsRenderer = new google.maps.DirectionsRenderer();
    var options = {
        zoom: 16, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,

    };
    var map = new google.maps.Map(document.getElementById("map"), options);
    //var map = new google.maps.Map(document.getElementById("map"), {
    //    center: latlng,
    //    zoom: 14,

    //});
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
        }
    });
}

window.initMap = initMap;