var dmarker;
var drivername;
var MovingDlat;
var MovingDlon;
var Timer;
var watchid;
const StoreMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/StoreMarker.png";
const DriverMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/NavigationLogo.png";

export function initMap(CartDetailsList, StoretDetailsList, DriverDetails) {

    drivername = DriverDetails.title;
    MovingDlat = DriverDetails.latitude;
    MovingDlon = DriverDetails.longitude;
    const point = { lat: MovingDlat, lng: MovingDlon };
    navigator.geolocation.getCurrentPosition(GetCoordinates)

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
        //label: `driver`,
        icon: DriverMarkerIcon

    });
    dmarker.setAnimation(google.maps.Animation.BOUNCE);
    dmarker.addListener("click", () => {

        infoWindow.close();
        infoWindow.setContent(dmarker.getTitle());
        infoWindow.open(dmarker.getMap(), dmarker);

    });
    // Set LatLng and title text for the markers. The first marker (Boynton Pass)
    // receives the initial focus when tab is pressed. Use arrow keys to
    // move between markers; press tab again to cycle through the map controls.
    //const tourStops = [
    //    [{ lat: 34.8791806, lng: -111.8265049 }, "Boynton Pass"],
    //    [{ lat: 34.8559195, lng: -111.7988186 }, "Airport Mesa"],
    //    [{ lat: 34.832149, lng: -111.7695277 }, "Chapel of the Holy Cross"],
    //    [{ lat: 34.823736, lng: -111.8001857 }, "Red Rock Crossing"],
    //    [{ lat: 34.800326, lng: -111.7665047 }, "Bell Rock"],
    //];
    // Create an info window to share between markers.




    console.table(CartDetailsList);
    console.table(StoretDetailsList);




    // Create the markers.
    //tourStops.forEach(([position, title], i) => {
    //    console.log('Index: ' + i + ' Value: ' + position.lat);
    //    const marker = new google.maps.Marker({
    //        position,
    //        map,
    //        title: `${i + 1}. ${title}`,
    //        label: `${i + 1}`,
    //        //icon: "/Images/CartMarker.png",
    //        optimized: false,
    //    });



    //    // Add a click listener for each marker, and set up the info window.
    //    marker.addListener("click", () => {

    //        infoWindow.close();
    //        infoWindow.setContent(marker.getTitle());
    //        infoWindow.open(marker.getMap(), marker);
    //        directionsRenderer.setMap(map);
    //        consoleLog(marker.position.lat());
    //        calcRoute(directionsService, directionsRenderer, marker.position.lat(), marker.position.lng(), dlat, dlon);
    //    });
    //});


    CartDetailsList.forEach((cartInfo, i) => {



        const cartmarker = new google.maps.Marker({
            position: { lat: cartInfo.latitude, lng: cartInfo.longitude },
            map,
            title: `${i + 1}. ${cartInfo.title}`,
            label: `${i + 1}`,

            optimized: false,

        });
        // Add a click listener for each marker, and set up the info window.
        cartmarker.addListener("click", () => {

            infoWindow.close();
            infoWindow.setContent(cartmarker.getTitle());
            infoWindow.open(cartmarker.getMap(), cartmarker);
            directionsRenderer.setMap(map);

            calcRoute(directionsService, directionsRenderer, cartmarker.position.lat(), cartmarker.position.lng(), MovingDlat, MovingDlon);
        });
    });

    StoretDetailsList.forEach((storeInfo, i) => {



        const storemarker = new google.maps.Marker({
            position: { lat: storeInfo.latitude, lng: storeInfo.longitude },
            map,
            title: `${i + 1}. ${storeInfo.title}`,
            //label: `${i + 1}`,
            icon: StoreMarkerIcon,
            optimized: false,

        });

        // Add a click listener for each marker, and set up the info window.
        storemarker.addListener("click", () => {

            infoWindow.close();


            infoWindow.setContent(storemarker.getTitle());
            infoWindow.open(storemarker.getMap(), storemarker);

            directionsRenderer.setMap(map);

            calcRoute(directionsService, directionsRenderer, storemarker.position.lat(), storemarker.position.lng(), MovingDlat, MovingDlon);
        });
    });

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
            StopWatch();
            StopTimer();
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
}
window.initMap = initMap;