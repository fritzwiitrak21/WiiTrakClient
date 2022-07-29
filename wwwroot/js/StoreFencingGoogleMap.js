var map;
var drawingManager;
const StoreMarkerIcon = "https://wiitrakstorageaccount.blob.core.windows.net/wiitrakblobcontainer/StoreMarker.png";
var polycoords;
var polygonobj;
let dotNetObjectReference;
export function initMap(dotNetObjectRef, IsFenced,StoreDetails,Coords) {
     dotNetObjectReference = dotNetObjectRef;

    var latlng = new google.maps.LatLng(StoreDetails.latitude, StoreDetails.longitude);
    
    var options = {
        zoom: 17, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,

    };
    map = new google.maps.Map(document.getElementById("map"), options);

    new google.maps.Marker({
        position: latlng,
        map,
        title: StoreDetails.title,
        icon: StoreMarkerIcon,

    });
    if (!IsFenced) {
        drawingManager = new google.maps.drawing.DrawingManager({
            drawingMode: google.maps.drawing.OverlayType.POLYGON,
            drawingControl: true,
            drawingControlOptions: {
                position: google.maps.ControlPosition.TOP_CENTER,
                drawingModes: [
                    google.maps.drawing.OverlayType.POLYGON,
                ],
            },
            polygonOptions: {
                editable: true
            }

        });

        drawingManager.setMap(map);

        google.maps.event.addListener(drawingManager, "overlaycomplete", function (event) {
            var newShape = event.overlay;
            newShape.type = event.type;
        });

        google.maps.event.addListener(drawingManager, "overlaycomplete", function (event) {
            overlayClickListener(event.overlay);
            polycoords = event.overlay.getPath().getArray();

            dotNetObjectReference.invokeMethodAsync("SavePolyCoords", JSON.stringify(polycoords));
        });
    }
    else {

         polycoords = JSON.parse(Coords);
         polygonobj = new google.maps.Polygon({
             paths: polycoords,
             strokeColor: "#FF0000",
            strokeOpacity: 0.8,
            strokeWeight: 2,
             fillColor: "#FF0000",
            fillOpacity: 0.35,
        });
        polygonobj.setMap(map);

        isWithinPoly(latlng);
    }

}

function overlayClickListener(overlay) {
    google.maps.event.addListener(overlay, "mouseup", function (event) {
        polycoords = overlay.getPath().getArray();
        dotNetObjectReference.invokeMethodAsync("SavePolyCoords", JSON.stringify(polycoords));
    });

}
function isWithinPoly(latlng) {

    var isWithinPolygon = google.maps.geometry.poly.containsLocation(latlng, polygonobj);
    console.log(isWithinPolygon);
}

window.initMap = initMap;

