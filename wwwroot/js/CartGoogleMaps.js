var dmarker;
var drivername;
var MovingDlat;
var MovingDlon;
var Timer;
var watchid;
const StoreMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/StoreMarker.png";
const DriverMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/NavigationLogo.png";
var travelindex = 0;
var templist;

export function initMap(CartDetailsList, StoretDetailsList, DriverDetails) {
    travelindex = 0;
    drivername = DriverDetails.title;
    MovingDlat = DriverDetails.latitude;
    MovingDlon = DriverDetails.longitude;
    const point = { lat: MovingDlat, lng: MovingDlon };

    var directionsService = new google.maps.DirectionsService();
    var directionsRenderer = new google.maps.DirectionsRenderer();
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 13,
        center: point,
    });

    const infoWindow = new google.maps.InfoWindow();
    dmarker = new google.maps.Marker({
        position: point,
        map,
        title: drivername,
        icon: DriverMarkerIcon
    });
    dmarker.setAnimation(google.maps.Animation.BOUNCE);
    dmarker.addListener("click", () => {

        infoWindow.close();
        infoWindow.setContent(dmarker.getTitle());
        infoWindow.open(dmarker.getMap(), dmarker);

    });
    
    CartDetailsList.forEach((cartInfo, i) => {
        const cartmarker = new google.maps.Marker({
            position: { lat: cartInfo.latitude, lng: cartInfo.longitude },
            map,
            title: `${i + 1}. ${cartInfo.title}`,
            label: `${i + 1}`,
            arguments: cartInfo.title,
            optimized: false,
        });
       
        // Add a click listener for each marker, and set up the info window.
        cartmarker.addListener("click", () => {
            infoWindow.close();
            infoWindow.setContent(cartmarker.getTitle());
            infoWindow.open(cartmarker.getMap(), cartmarker);
            directionsRenderer.setMap(map);
            calcRoute(directionsService, directionsRenderer, cartmarker.position.lat(), cartmarker.position.lng(), MovingDlat, MovingDlon, cartmarker.arguments);
        });
    });

    StoretDetailsList.forEach((storeInfo, i) => {
        const storemarker = new google.maps.Marker({
            position: { lat: storeInfo.latitude, lng: storeInfo.longitude },
            map,
            title: `${i + 1}. ${storeInfo.title}`,
            arguments: storeInfo.title,
            icon: StoreMarkerIcon,
            optimized: false,

        });

        // Add a click listener for each marker, and set up the info window.
        storemarker.addListener("click", () => {
            infoWindow.close();
            infoWindow.setContent(storemarker.getTitle());
            infoWindow.open(storemarker.getMap(), storemarker);
            directionsRenderer.setMap(map);
            calcRoute(directionsService, directionsRenderer, storemarker.position.lat(), storemarker.position.lng(), MovingDlat, MovingDlon, storemarker.arguments);
        });
    });
}

function calcRoute(directionsService, directionsRenderer, latitude, longitude, dlat, dlon, DestName) {
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
            let customList = [];
            var steps = directionsData.steps;
            steps.forEach((step, i) => {
                var text = RemoveHtml(step.instructions);
                text = text.replaceAll("Rd", "Road <br\>")
                var position = new google.maps.LatLng(steps[i].end_location);

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
            StopWatch();
            StopTimer();
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

    if (dist <= 7 && templist[travelindex].IsReached == 0) {
        templist[travelindex].IsReached = 1;

        TextToSpeech(templist[travelindex].Speech_Text);
        templist[travelindex].IsNavigated = 1;
        travelindex++;
        console.table(templist);
    }
}

function changeMarkerPosition() {
    var latlng = new google.maps.LatLng(MovingDlat, MovingDlon);
    dmarker.setPosition(latlng);
}

export function StopWatch() {
    navigator.geolocation.clearWatch(watchid);
    watchid = null;
}

export function StopTimer() {
    console.log("Cart Watch" + watchid);
    console.log("Cart Timer" + Timer);
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
