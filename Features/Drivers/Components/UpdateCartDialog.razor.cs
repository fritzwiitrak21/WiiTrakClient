using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
using WiiTrakClient.Features.Drivers.Models;
using WiiTrakClient.Shared;

namespace WiiTrakClient.Features.Drivers.Components
{
    public partial class UpdateCartDialog: ComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Parameter]
        public CartDto? Cart { get; set; }

        [Parameter]
        public List<RepairIssueDto>? RepairIssues { get; set; }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        private IJSObjectReference? _jsModule = null;
        bool _cartHasGeolocation = true;
        bool showRoute = false;
        int _selectedStatusInt = 0;
        int _seletedConditionInt = 0;
        string _seletedIssue = "";
        string _otherText = "";

        protected override async Task OnInitializedAsync()
        {
            if (_jsModule is null)
            {
                _jsModule = await JSRuntime
                    .InvokeAsync<IJSObjectReference>("import", "./js/CartUpdateMap.js");
            }
            var dotNetObjectRef = DotNetObjectReference.Create(this);
            await _jsModule.InvokeVoidAsync("GetRoutePermission", dotNetObjectRef);
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            System.Console.WriteLine($"long: {Cart.TrackingDevice.Longitude}");
            
        }
        protected override void OnParametersSet()
        {
            if (Cart is not null)
            {
                _selectedStatusInt = (int)Cart.Status;
                _seletedConditionInt = (int)Cart.Condition;
            }

            if (RepairIssues is not null)
            {
                _seletedIssue = RepairIssues[0].Issue;

                if (!RepairIssues.Any(x => x.Issue.Equals("Other")))
                {
                    RepairIssues.Add(new RepairIssueDto
                    {
                        Issue = "Other"
                    });
                }
            }
        }
        [JSInvokable]
        public async Task ShowCartRoute(bool routePermission)
        {
            System.Console.WriteLine($"show cart Route Permission: {routePermission}");

            showRoute = routePermission;
            StateHasChanged();
        }
        private async Task Showroute()
        {
            if (Cart.TrackingDevice is not null && Cart.TrackingDevice.Latitude > 0)
            {
                _cartHasGeolocation = true;

                var cartMarker = new CartMarkerInfo
                {
                    CartId = Cart.Id,
                    Lat = Cart.TrackingDevice.Latitude,
                    Long = Cart.TrackingDevice.Longitude,
                    PopupContent = "Cart #" + Cart.CartNumber,
                    Color = "MidnightBlue",
                    Number = Cart.CartNumber
                };
                await _jsModule.InvokeVoidAsync("GetCartRoute", cartMarker);
            }
            else
            {
                _cartHasGeolocation = false;
            }
        }

        void Submit()
        {
            if (_selectedStatusInt > -1)
            {
                Cart.Status = (CartStatus)_selectedStatusInt;
            }
            if (_seletedConditionInt > -1)
            {
                Cart.Condition = (CartCondition)_seletedConditionInt;
            }

            Cart.DamageIssue = _seletedIssue;
            MudDialog.Close(DialogResult.Ok(true));
        }
        void Cancel() => MudDialog.Cancel();

        void StatusChangedHandler(int statusInt)
        {
            _selectedStatusInt = statusInt;
        }

        void ConditionChangedHandler(int conditionInt)
        {
            _seletedConditionInt = conditionInt;
        }

        public async ValueTask DisposeAsync()
        {
            if (_jsModule is not null)
            {
                await _jsModule.DisposeAsync();
            }
        }
    }
}