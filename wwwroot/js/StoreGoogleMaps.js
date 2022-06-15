﻿
var map;
var MovingDlat;
var MovingDlon;
var marker;
var storemarker;
var Timer;
var watchid;
const StoreMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/StoreMarker.png";
const DriverMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/NavigationLogo.png";
const infoWindow = new google.maps.InfoWindow();
var DestName;
var travelindex = 0;
var templist;



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
export function initMap(storedetails, driverdetails) {

    travelindex = 0;
    DestName = storedetails.title;
    var latitude = storedetails.latitude;
    var longitude = storedetails.longitude;

    var latlng = new google.maps.LatLng(latitude, longitude);
    var directionsService = new google.maps.DirectionsService();
    var directionsRenderer = new google.maps.DirectionsRenderer();
    var dlat = driverdetails.latitude;
    var dlon = driverdetails.longitude;

    MovingDlat = dlat;
    MovingDlon = dlon;

    var options = {
        zoom: 16, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,

    };
    map = new google.maps.Map(document.getElementById("map"), options);
    const point = { lat: MovingDlat, lng: MovingDlon };

    marker = new google.maps.Marker({
        position: point,
        map,
        title: driverdetails.title,
        icon: DriverMarkerIcon,
        optimized: false,
    });

    storemarker = new google.maps.Marker({
        position: new google.maps.LatLng(latitude, longitude),
        map,
        title: DestName,
        icon: StoreMarkerIcon,
        optimized: false,
    });

    marker.setAnimation(google.maps.Animation.BOUNCE);
    marker.addListener("click", () => {

        infoWindow.close();
        infoWindow.setContent(marker.getTitle());
        infoWindow.open(stepmarker.getMap(), marker);

    });
    storemarker.setAnimation(google.maps.Animation.BOUNCE);
    storemarker.addListener("click", () => {

        infoWindow.close();
        infoWindow.setContent(storemarker.getTitle());
        infoWindow.open(stepmarker.getMap(), storemarker);

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
            var directionsData = response.routes[0].legs[0]; // Get data about the mapped route

            if (directionsData) {
                document.getElementById('distance').innerText = " Driving distance is " + directionsData.distance.text + " (" + directionsData.duration.text + ").";

                TextToSpeech("total Driving distance to reach " + DestName + " is " + directionsData.distance.text + " and it will takes around " + directionsData.duration.text);

            }
            var steps = directionsData.steps;
            let customList = [];
            steps.forEach((step, i) => {
                var text = RemoveHtml(step.instructions);
                text = text.replaceAll("Rd", "Road <br\>")
                var position = new google.maps.LatLng(steps[i].end_location);

                var stepmarker = new google.maps.Marker({
                    position: position,
                    map,
                    title: step.instructions,

                    optimized: false,
                });
                stepmarker.addListener("click", () => {

                    infoWindow.close();
                    infoWindow.setContent(stepmarker.getTitle());
                    infoWindow.open(stepmarker.getMap(), stepmarker);
                    var texts = RemoveHtml(stepmarker.getTitle());
                    texts = texts.replaceAll("Rd", "Road <br\>")
                    TextToSpeech(RemoveHtml(texts));

                });
                var distance = Finddistance(MovingDlat, MovingDlon, position.lat(), position.lng())
                var obj = {};
                obj['End_Lat'] = position.lat();
                obj['End_Lng'] = position.lng();
                obj['IsReached'] = 0;
                obj['Speech_Text'] = text;
                obj['IsNavigated'] = 0;
                obj['Distance'] = distance;
                customList.push(obj);
            });
            console.table(customList);

            templist = customList;
            console.table(templist);
            Timer = setInterval(function () {
                watchid = navigator.geolocation.watchPosition(GetCoordinates);
            }, 900);
        }
    });
}
function RemoveHtml(html) {
    let tmp = document.createElement("DIV");
    tmp.innerHTML = html;
    return tmp.textContent || tmp.innerText || "";
}
function GetCoordinates(position) {
    MovingDlat = position.coords.latitude;
    MovingDlon = position.coords.longitude;
    changeMarkerPosition();
    StopWatch();

    var dist = Finddistance(MovingDlat, MovingDlon, templist[travelindex].End_Lat, templist[travelindex].End_Lng)

    if (dist <= 10 && templist[travelindex].IsReached == 0) {
        templist[travelindex].IsReached = 1;

        TextToSpeech(templist[travelindex].Speech_Text);
        templist[travelindex].IsNavigated = 1;
        travelindex++;
        console.table(templist);
    }
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
    console.log("Store Timer" + Timer);
    clearTimeout(Timer);
    window.speechSynthesis.cancel();
}
function TextToSpeech(Text) {
    console.log(travelindex + " " + Text);
    var msg = new SpeechSynthesisUtterance();
    var voices = window.speechSynthesis.getVoices();
    msg.text = Text;
    msg.voice = voices[2];
    msg.volume = 1;
    msg.rate = 1;
    msg.pitch = 1;
    msg.lang = 'en';
    window.speechSynthesis.speak(msg);
}
function Finddistance(lat1, lon1, lat2, lon2) {
    var radlat1 = Math.PI * lat1 / 180
    var radlat2 = Math.PI * lat2 / 180
    var theta = lon1 - lon2
    var radtheta = Math.PI * theta / 180
    var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
    dist = Math.acos(dist)
    dist = dist * 180 / Math.PI
    dist = dist * 60 * 1.1515
    dist = dist * 1609.34

    return Math.round(dist)
}



window.initMap = initMap;