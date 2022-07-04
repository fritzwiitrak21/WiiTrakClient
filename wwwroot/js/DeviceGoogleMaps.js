export function initMap(devicedetails) {
  
    var latlng = new google.maps.LatLng(devicedetails.latitude, devicedetails.longitude);
    var options = {
        zoom: 15, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,

    };


    var map = new google.maps.Map(document.getElementById("map"), options);

    var DeviceMarker = new google.maps.Marker({
        position: latlng,
        map,
        title: devicedetails.title,

    });
    DeviceMarker.setAnimation(google.maps.Animation.BOUNCE);
    const info = new google.maps.InfoWindow();
    DeviceMarker.addListener("click", () => {

        info.close();
        info.setContent(devicedetails.title.replaceAll("\n", "<br\>"));
        info.open(DeviceMarker.getMap(), DeviceMarker);

    });
}

window.initMap = initMap;