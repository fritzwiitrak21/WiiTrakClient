/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.Collections.Generic;

namespace WiiTrakClient.Shared
{
    public class CartMarkerInfo 
    {
        public Guid CartId {get; set;}
        public double Lat {get; set;}
        public double Long {get; set;}
        public string PopupContent {get; set;} = string.Empty;
        public string Text {get; set;} = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Color {get; set; } = string.Empty;
    }
    public class StoreCartMapMarker
    {
        public Guid StoreId {get; set;}
        public string StoreName {get; set;} = string.Empty;
        public string StoreNumber { get; set; } = string.Empty;
        public string PopupContent {get; set;} = string.Empty;
        public string Text {get; set;} = string.Empty;
        public double StoreLat {get; set;}
        public double StoreLong {get; set;}
        public string Color {get; set; } = string.Empty;
        public List<CartMarkerInfo>? CartMarkers {get; set;}
    }
}