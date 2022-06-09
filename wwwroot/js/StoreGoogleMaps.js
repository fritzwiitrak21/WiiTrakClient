
var map;
var MovingDlat;
var MovingDlon;
var marker;
var storemarker;
var Timer;
var watchid;
const StoreMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/StoreMarker.png";
const DriverMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/NavigationLogo.png";
var i = 1;
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
export function initMap(latitude, longitude, dlat, dlon, StoreName) {
    DestName = StoreName;
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
    directionsRenderer.setPanel(document.getElementById("directionsPanel"));

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

            //console.table(directionsData.steps);
            //console.log(directionsData.steps);

            if (directionsData) {
                document.getElementById('distance').innerText = " Driving distance is " + directionsData.distance.text + " (" + directionsData.duration.text + ").";

                TextToSpeech("total Driving distance to reach " + DestName+" is " + directionsData.distance.text + " and it will takes around " + directionsData.duration.text);

            }
            var steps = directionsData.steps;
            let customList = [];
            steps.forEach((step, i) => {
                var text = RemoveHtml(step.instructions);
                text = text.replaceAll("Rd", "Road <br\>")
                //console.log(i + 1 + " " + text);

                //TextToSpeech(text);
               
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

                });
                //console.log(stepmarker.position.lat(), stepmarker.position.lng());
                var distance = Finddistance(MovingDlat, MovingDlon, position.lat(), position.lng())
                //console.log("Distance: " + distance);

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
            TextToSpeech(customList[travelindex].Speech_Text);//index 0
            customList[travelindex].IsNavigated = 1;
            templist = customList;
            console.table(templist);
            Timer = setInterval(function () {
             
                watchid = navigator.geolocation.watchPosition(GetCoordinates);
                //changeMarkerPosition();
               // StopWatch();
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
    //MovingDlat = MovingDlat - 0.1;
    //MovingDlon = MovingDlon - 0.1;
    changeMarkerPosition();
    StopWatch();
    if (i % 3 == 0) {
        var dist = Finddistance(MovingDlat, MovingDlon, templist[travelindex].End_Lat, templist[travelindex].End_Lng)
       
        if (dist <= 5) {
            templist[travelindex].IsReached = 1;
            travelindex++;
            TextToSpeech(templist[travelindex].Speech_Text);
            templist[travelindex].IsNavigated = 1;
        }
       
    }
    i++;
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