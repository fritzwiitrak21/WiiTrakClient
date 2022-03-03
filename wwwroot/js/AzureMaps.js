export function getMap(storeCartMarkers, dotNetObjectRef) { 
    
    let dotNetObjectReference = dotNetObjectRef;

    const stringifiedObj = JSON.stringify(storeCartMarkers);
    const storeCartMarkersObj = JSON.parse(stringifiedObj);

    // Get coordinates of first element for center
    const centerLong = storeCartMarkersObj[0].storeLong;
    const centerLat = storeCartMarkersObj[0].storeLat;

    //Initialize a map instance.
    let map = new atlas.Map('myMap', {
        view: 'Auto',
        zoom: 10,
        center: [centerLong, centerLat],
        //Add authentication details for connecting to Azure Maps.
        authOptions: {
            //Use Azure Active Directory authentication.
            // authType: 'anonymous',
            // clientId: '1806cbde-7e0a-4a1f-899b-3034ddfc1dd2', //Your Azure Active Directory client id for accessing your Azure Maps account.
            // getToken: function (resolve, reject, map) {
            //     //URL to your authentication service that retrieves an Azure Active Directory Token.
            //     var tokenServiceUrl = "https://azuremapscodesamples.azurewebsites.net/Common/TokenService.ashx";

            //     fetch(tokenServiceUrl).then(r => r.text()).then(token => resolve(token));
            // }

            //Alternatively, use an Azure Maps key. Get an Azure Maps key at https://azure.com/maps. NOTE: The primary key should be used as the key.
            authType: 'subscriptionKey',
            subscriptionKey: 'KeCEGMJad72lJuE71dvTmMrEvwQswAbVAvZqHG8buYY'
        }
    });

    //Wait until the map resources are ready.
    map.events.add('ready', function () {  
        for (let i = 0; i < storeCartMarkersObj.length; i++) {
            const storeMarkerInfo = storeCartMarkersObj[i];  

              //Create a HTML marker and add it to the map
            const storeContent = storeMarkerInfo.popupContent;
            const newStoreMarker = new atlas.HtmlMarker({
                htmlContent: '<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="30" height="37" viewBox="0 0 30 37" xml:space="preserve"><rect x="0" y="0" rx="8" ry="8" width="30" height="30" fill="{color}"/><polygon fill="{color}" points="10,29 20,29 15,37 10,29"/><text x="15" y="20" style="font-size:16px;font-family:arial;fill:#ffffff;" text-anchor="middle">{text}</text></svg>',
                color: storeMarkerInfo.color,
                text: storeMarkerInfo.text,
                position: [storeMarkerInfo.storeLong, storeMarkerInfo.storeLat],
                popup: new atlas.Popup({
                    content: '<div style="padding:30px">'+ storeContent + '</div>',
                    pixelOffset: [0, -30]
                })
            });

            //Add a click event to toggle the popup.
            map.events.add('click',newStoreMarker, () => {
                newStoreMarker.togglePopup();                
            });

            map.markers.add(newStoreMarker);

            for (let j = 0; j < storeMarkerInfo.cartMarkers.length; j++) {
                const cartMarkerInfo = storeMarkerInfo.cartMarkers[j];

                //Create a HTML marker and add it to the map
                const cartContent = cartMarkerInfo.popupContent;
                const newCartMarker = new atlas.HtmlMarker({
                    htmlContent: '<svg height="32" viewBox="0 0 511.728 511.728" width="32" xmlns="http://www.w3.org/2000/svg"><path fill="{color}" d="M147.925 379.116c-22.357-1.142-21.936-32.588-.001-33.68 62.135.216 226.021.058 290.132.103 17.535 0 32.537-11.933 36.481-29.017l36.404-157.641c2.085-9.026-.019-18.368-5.771-25.629s-14.363-11.484-23.626-11.484c-25.791 0-244.716-.991-356.849-1.438L106.92 54.377c-4.267-15.761-18.65-26.768-34.978-26.768H15c-8.284 0-15 6.716-15 15s6.716 15 15 15h56.942a6.246 6.246 0 0 1 6.017 4.592l68.265 253.276c-12.003.436-23.183 5.318-31.661 13.92-8.908 9.04-13.692 21.006-13.471 33.695.442 25.377 21.451 46.023 46.833 46.023h21.872a52.18 52.18 0 0 0-5.076 22.501c0 28.95 23.552 52.502 52.502 52.502s52.502-23.552 52.502-52.502a52.177 52.177 0 0 0-5.077-22.501h94.716a52.185 52.185 0 0 0-5.073 22.493c0 28.95 23.553 52.502 52.502 52.502 28.95 0 52.503-23.553 52.503-52.502a52.174 52.174 0 0 0-5.464-23.285c5.936-1.999 10.216-7.598 10.216-14.207 0-8.284-6.716-15-15-15zm91.799 52.501c0 12.408-10.094 22.502-22.502 22.502s-22.502-10.094-22.502-22.502c0-12.401 10.084-22.491 22.483-22.501h.038c12.399.01 22.483 10.1 22.483 22.501zm167.07 22.494c-12.407 0-22.502-10.095-22.502-22.502 0-12.285 9.898-22.296 22.137-22.493h.731c12.24.197 22.138 10.208 22.138 22.493-.001 12.407-10.096 22.502-22.504 22.502zm74.86-302.233c.089.112.076.165.057.251l-15.339 66.425H414.43l8.845-67.023 58.149.234c.089.002.142.002.23.113zm-154.645 163.66v-66.984h53.202l-8.84 66.984zm-74.382 0-8.912-66.984h53.294v66.984zm-69.053 0h-.047c-3.656-.001-6.877-2.467-7.828-5.98l-16.442-61.004h54.193l8.912 66.984zm56.149-96.983-9.021-67.799 66.306.267v67.532zm87.286 0v-67.411l66.022.266-8.861 67.145zm-126.588-67.922 9.037 67.921h-58.287l-18.38-68.194zm237.635 164.905H401.63l8.84-66.984h48.973l-14.137 61.217a7.406 7.406 0 0 1-7.25 5.767z"/></svg>',
                    color: cartMarkerInfo.color, 
                    text: cartMarkerInfo.text,
                    position: [cartMarkerInfo.long, cartMarkerInfo.lat],
                    popup: new atlas.Popup({
                        content: '<div style="padding:30px">'+ cartContent + '</div>',
                        pixelOffset: [0, -30]
                    })                               
                });

                 //Add a click event to toggle the popup.
                map.events.add('click',newCartMarker, () => {
                    //newCartMarker.togglePopup();
                    dotNetObjectReference.invokeMethodAsync("ShowUpdateCartDialog", storeMarkerInfo.cartMarkers[j].cartId);
                });

                map.markers.add(newCartMarker);
            }
        }
    });   
}


export function callShowUpdateCartDialog(dotNetObjRef) {
    dotNetObjRef.invokeMethodAsync("ShowUpdateCartDialog", "called from js");
}