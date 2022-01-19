using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
using WiiTrakClient.Shared;

namespace WiiTrakClient.Features.Companies.Components
{
    public partial class CartDetailsDialog : ComponentBase, IAsyncDisposable
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public CartDto? Cart { get; set; }

        bool _cartHasGeolocation = true;

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private IJSObjectReference? _jsModule = null;

        protected override async Task OnParametersSetAsync()
        {
            
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            System.Console.WriteLine($"long: {Cart.TrackingDevice.Longitude}");

            if (_jsModule is null)
            {
                _jsModule = await JSRuntime
                    .InvokeAsync<IJSObjectReference>("import", "./js/CartMap.js");
            }

            if (Cart.TrackingDevice is not null && Cart.TrackingDevice.Latitude > 0)
            {    
                _cartHasGeolocation = true;

                var cartMarker = new CartMarkerInfo
                {
                    CartId = Cart.Id,
                    Lat = Cart.TrackingDevice.Latitude,
                    Long = Cart.TrackingDevice.Longitude,
                    PopupContent = "Cart",
                    Color = "MidnightBlue"
                };
                await _jsModule.InvokeVoidAsync("GetMap", cartMarker);
            }
            else
            {   
                _cartHasGeolocation = false;
            }
        }

        void Cancel() => MudDialog.Cancel();

        public async ValueTask DisposeAsync()
        {
            if (_jsModule is not null)
            {
                await _jsModule.DisposeAsync();
            }
        }
    }
}
