using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.Features.Drivers.Models;
using WiiTrakClient.Features.Drivers.Components;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.DTOs;
using WiiTrakClient.Cores;
using MudBlazor;
using WiiTrakClient.Enums;
using WiiTrakClient.Helpers;

namespace WiiTrakClient.Features.Drivers
{
    public partial class DeliveryTickets : ComponentBase
    {
        [Inject] NavigationManager NavManager { get; set; }
        [Inject] IJSRuntime JsRuntime { get; set; }

        [Inject] IDriverHttpRepository DriverRepository { get; set; }

        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }

        [Inject] public ICartHttpRepository CartHttpRepository { get; set; }

        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }

        [Inject] IDialogService DialogService { get; set; }

        [Inject] IWorkOrderHttpRepository WorkOrderHttpRepository { get; set; }

        DriverDto SelectedDriver = new();

        List<DeliveryTicketDto> _deliveryTickets = new();
        List<DeliveryTicketDto> deliveryTickets = new();
        List<CartDto> _carts = new();
        List<StoreDto> _stores = new();
        List<StoreDto> TempStores = new();
        DeliveryTicketCreationDto _newDeliveryTicket = new();
        private IJSObjectReference JsModule;
        private double Latitude;
        private double Longitude;

        protected override async Task OnInitializedAsync()
        {

            try
            {
                JsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/localstorage.js");
                await JsModule.InvokeVoidAsync("getCoord", false);
                if (CurrentUser.UserId == Guid.Empty)
                {
                    var Id = await JsModule.InvokeAsync<string>("getUserId");
                    CurrentUser.UserId = new Guid(Id);
                }
                try
                {
                    var coords = await JsModule.InvokeAsync<string>("getCoordinates");
                    var lat = coords.Split("##")[0];
                    var Lon = coords.Split("##")[1];
                    Latitude = Core.ToDouble(lat);
                    Longitude = Core.ToDouble(Lon);
                    await JsModule.InvokeVoidAsync("ClearCoord");
                }
                catch(Exception ex)
                {
                    await JsModule.InvokeVoidAsync("ClearCoord");
                }
                TempStores = _stores = await StoreHttpRepository.GetStoresByDriverId(CurrentUser.UserId);
                await GetDeliveryTicketsByDriverId(CurrentUser.UserId);
            
                SelectedDriver = await DriverRepository.GetDriverByIdAsync(CurrentUser.UserId);
            }
            catch (Exception ex)
            {
            }
            await HandleDriverSelected();
           
        }

        private async Task HandleDriverSelected()
        {
            _carts = await CartHttpRepository.GetCartsByDriverIdAsync(CurrentUser.UserId);
            //var lat = CurrentUser.Coord.Split("##")[0];
            //var Lon = CurrentUser.Coord.Split("##")[1];
            //Latitude = Core.ToDouble(lat);
            //Longitude = Core.ToDouble(Lon);

            FindDistance();
        }

        #region GetDeliveryTicketsByDriverId
        private async Task GetDeliveryTicketsByDriverId(Guid id)
        {
            deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByDriverIdAsync(id);

            if (deliveryTickets is not null)
            {
                foreach (var item in deliveryTickets)
                {
                    try
                    {
                        item.DriverStoresIsActive = TempStores.FirstOrDefault(x => x.Id == item.StoreId).DriverStoresIsActive;
                        item.StoresIsActive = TempStores.FirstOrDefault(x => x.Id == item.StoreId).IsActive;
                    }
                    catch (Exception ex)
                    {
                        //Exception
                    }
                }
                _deliveryTickets = deliveryTickets;
            }
            StateHasChanged();
        }
        #endregion

        #region AddNewDeliveryDialog

        private async Task AddNewDeliveryDialog()
        {
            try
            {
                JsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/localstorage.js");
                await JsModule.InvokeVoidAsync("getCoord", false);
                var Coords = await JsModule.InvokeAsync<string>("getCoordinates");
                var lat = Coords.Split("##")[0];
                var Lon = Coords.Split("##")[1];
                Latitude = Core.ToDouble(lat);
                Longitude = Core.ToDouble(Lon);
                await JsModule.InvokeVoidAsync("ClearCoord");
            }
            catch (Exception ex)
            {
                await JsModule.InvokeVoidAsync("ClearCoord");
            }

            FindDistance();
            var parameters = new DialogParameters();
            _newDeliveryTicket = new DeliveryTicketCreationDto();

            parameters.Add("NewDeliveryTicket", _newDeliveryTicket);
            parameters.Add("Driver", SelectedDriver);
            parameters.Add("Carts", _carts);
            parameters.Add("Stores", _stores);
            parameters.Add("Latitude", Latitude);
            parameters.Add("Longitude", Longitude);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<AddDeliveryTicketDialog>("New Delivery Ticket", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                // add new delivery ticket to backend
                var deliveryTicketCreation = new DeliveryTicketCreationDto
                {
                    NumberOfCarts = _newDeliveryTicket.NumberOfCarts,
                    PicUrl = _newDeliveryTicket.PicUrl,
                    DeliveredAt = DateTime.Now,
                    StoreId = _newDeliveryTicket.StoreId,
                    ServiceProviderId = _newDeliveryTicket.ServiceProviderId,
                    DriverId = _newDeliveryTicket.DriverId,
                    SignOffRequired = _stores.FirstOrDefault(x => x.Id == _newDeliveryTicket.StoreId).IsSignatureRequired
                };

                var deliveryTicketResponse = await DeliveryTicketHttpRepository.CreateDeliveryTicketAsync(deliveryTicketCreation);
                await GetDeliveryTicketsByDriverId(CurrentUser.UserId);//Refreshing the data in the grid once new ticket added

                // update status of carts to delivered and update cart hitory
                var carts = _carts.Where(x => x.StoreId == _newDeliveryTicket.StoreId).ToList();
                foreach (var cart in carts)
                {
                    if (!deliveryTicketResponse.SignOffRequired)
                    {
                        if (cart.Condition == CartCondition.Damage)
                        {
                            var newWorkOrder = new WorkOrderCreationDto
                            {
                                Issue = cart.DamageIssue,
                                Notes = "",
                                CartId = cart.Id,
                                StoreId = cart.Store != null ? cart.Store.Id : null,
                                DriverId = CurrentUser.UserId

                            };

                            await WorkOrderHttpRepository.CreateWorkOrderAsync(newWorkOrder);
                        }
                    }
                    var cartHistory = new CartHistoryUpdateDto
                    {
                        DeliveryTicketId = deliveryTicketResponse.Id,
                        PickupLatitude = cart.TrackingDevice != null ? cart.TrackingDevice.Latitude : 0,
                        PickupLongitude = cart.TrackingDevice != null ? cart.TrackingDevice.Longitude : 0,
                        DroppedOffAt = DateTime.Now,
                        ServiceProviderId = cart.Store != null ? cart.Store.ServiceProviderId : null,
                        StoreId = cart.StoreId,
                        DriverId = CurrentUser.UserId,
                        Condition = cart.Condition,
                        Status = CartStatus.PickedUp,// CartStatus.InsideGeofence,
                        IsDelivered = true,
                        CartId = cart.Id
                    };

                    var cartUpdate = new CartUpdateDto
                    {
                        ManufacturerName = cart.ManufacturerName,
                        DateManufactured = cart.DateManufactured,
                        OrderedFrom = cart.OrderedFrom,
                        Condition = cart.Condition,
                        Status = CartStatus.PickedUp,// CartStatus.InsideGeofence,
                        PicUrl = cart.PicUrl,
                        IsProvisioned = cart.IsProvisioned,
                        BarCode = cart.BarCode,
                        StoreId = cart.StoreId,
                        CartHistory = cartHistory
                    };

                    await CartHttpRepository.UpdateCartAsync(cart.Id, cartUpdate);
                    await GetDeliveryTicketsByDriverId(CurrentUser.UserId);
                }
            }
        }
        #endregion

        #region Get Distance


        private double Getdistance(double lat2, double lon2, char unit = 'K')
        {
            double lat1 = Latitude;
            double lon1 = Longitude;

            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        #endregion
        private void FindDistance()
        {
            _stores = _stores.Where(x => x.DriverStoresIsActive == true && x.IsActive == true).ToList();
            foreach (var item in _stores)
            {
                var distance = Getdistance(item.Latitude, item.Longitude);
                item.Distance = Convert.ToInt32(Math.Ceiling(distance));
            }
        }

    }
}