/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.DTOs;
using WiiTrakClient.Cores;
using WiiTrakClient.Enums;
using MudBlazor;
using WiiTrakClient.Features.Drivers.Components;
namespace WiiTrakClient.Features.Drivers
{
    public partial class DeliveryTickets : ComponentBase
    {

        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] IDriverHttpRepository DriverRepository { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }
        [Inject] public ICartHttpRepository CartHttpRepository { get; set; }
        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] IWorkOrderHttpRepository WorkOrderHttpRepository { get; set; }
        DriverDto SelectedDriver = new();
        List<DeliveryTicketDto> deliverytickets = new();
        List<DeliveryTicketDto> Deliverytickets = new();
        List<CartDto> Carts = new();
        List<StoreDto> Stores = new();
        List<StoreDto> TempStores = new();
        DeliveryTicketCreationDto NewDeliveryTicket = new();
        private IJSObjectReference JsModule;
        private double Latitude;
        private double Longitude;
        int SelectedOption = 30;
        int TempSelectedOption;
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
                catch
                {
                    await JsModule.InvokeVoidAsync("ClearCoord");
                }
                TempStores = Stores = await StoreHttpRepository.GetStoresByDriverId(CurrentUser.UserId);
                await GetDeliveryTicketsByDriverId(CurrentUser.UserId);
                SelectedDriver = await DriverRepository.GetDriverByIdAsync(CurrentUser.UserId);
            }
            catch
            {
                //Exception
            }
            await HandleDriverSelected();
        }
        private async Task HandleDriverSelected()
        {
            Carts = await CartHttpRepository.GetCartsByDriverIdAsync(CurrentUser.UserId);
            FindDistance();
        }
        #region GetDeliveryTicketsByDriverId
        private async Task GetDeliveryTicketsByDriverId(Guid id)
        {
            Deliverytickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsById(id, (Role)CurrentUser.UserRoleId, SelectedOption);

            if (Deliverytickets is not null)
            {
                foreach (var item in Deliverytickets)
                {
                    try
                    {
                        item.DriverStoresIsActive = TempStores.FirstOrDefault(x => x.Id == item.StoreId).DriverStoresIsActive;
                        item.StoresIsActive = TempStores.FirstOrDefault(x => x.Id == item.StoreId).IsActive;
                    }
                    catch
                    {
                        //Exception
                    }
                }
                deliverytickets = Deliverytickets;
            }
            StateHasChanged();
        }
        #endregion
        public async Task GetDeliveryTicketDetails()
        {
            if (TempSelectedOption != SelectedOption)
            {
                var value = SelectedOption;
                TempSelectedOption = SelectedOption;
                Deliverytickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsById(CurrentUser.UserId, (Role)CurrentUser.UserRoleId, value);
                if (Deliverytickets is not null)
                {
                    deliverytickets = Deliverytickets;
                }
                StateHasChanged();
            }
        }
        #region AddNewDeliveryDialog
        private async Task AddNewDeliveryDialog()
        {
            try
            {
                if (JsModule is null)
                {
                    JsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/localstorage.js");
                }
                await JsModule.InvokeVoidAsync("getCoord", false);
                var Coords = await JsModule.InvokeAsync<string>("getCoordinates");
                var lat = Coords.Split("##")[0];
                var Lon = Coords.Split("##")[1];
                Latitude = Core.ToDouble(lat);
                Longitude = Core.ToDouble(Lon);
                await JsModule.InvokeVoidAsync("ClearCoord");
            }
            catch
            {
                await JsModule.InvokeVoidAsync("ClearCoord");
            }
            FindDistance();
            var parameters = new DialogParameters();
            NewDeliveryTicket = new DeliveryTicketCreationDto();
            parameters.Add("NewDeliveryTicket", NewDeliveryTicket);
            parameters.Add("Driver", SelectedDriver);
            parameters.Add("Carts", Carts);
            parameters.Add("Stores", Stores);
            parameters.Add("Latitude", Latitude);
            parameters.Add("Longitude", Longitude);
            var dialog = DialogService.Show<AddDeliveryTicketDialog>("New Delivery Ticket", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                // add new delivery ticket to backend
                var DeliveryTicketCreation = new DeliveryTicketCreationDto
                {
                    NumberOfCarts = NewDeliveryTicket.NumberOfCarts,
                    PicUrl = NewDeliveryTicket.PicUrl,
                    DeliveredAt = DateTime.UtcNow,
                    StoreId = NewDeliveryTicket.StoreId,
                    ServiceProviderId = NewDeliveryTicket.ServiceProviderId,
                    DriverId = CurrentUser.UserId,
                    SignOffRequired = Stores.FirstOrDefault(x => x.Id == NewDeliveryTicket.StoreId).IsSignatureRequired
                };
                var DeliveryTicketResponse = await DeliveryTicketHttpRepository.CreateDeliveryTicketAsync(DeliveryTicketCreation);
                await GetDeliveryTicketsByDriverId(CurrentUser.UserId);//Refreshing the data in the grid once new ticket added
                // update status of carts to delivered and update cart hitory
                //var carts = _carts.Where(x => x.StoreId == _newDeliveryTicket.StoreId && x.Status==(CartStatus)2).ToList();
                foreach (var cart in NewDeliveryTicket.PickedUpCarts)
                {
                    if (!DeliveryTicketResponse.SignOffRequired)
                    {
                        if (cart.Condition == CartCondition.Damage)
                        {
                            var NewWorkOrder = new WorkOrderCreationDto
                            {
                                Issue = cart.IssueType,
                                Notes = cart.IssueDescription,
                                CartId = cart.Id,
                                StoreId = cart.Store != null ? cart.Store.Id : null,
                                DriverId = CurrentUser.UserId

                            };
                            await WorkOrderHttpRepository.CreateWorkOrderAsync(NewWorkOrder);
                        }
                    }
                    var CartHistory = new CartHistoryUpdateDto
                    {
                        DeliveryTicketId = DeliveryTicketResponse.Id,
                        PickupLatitude = cart.TrackingDevice != null ? cart.TrackingDevice.Latitude : 0,
                        PickupLongitude = cart.TrackingDevice != null ? cart.TrackingDevice.Longitude : 0,
                        DroppedOffAt = DateTime.UtcNow,
                        ServiceProviderId = cart.Store != null ? cart.Store.ServiceProviderId : null,
                        StoreId = cart.StoreId,
                        DriverId = CurrentUser.UserId,
                        Condition = cart.Condition,
                        Status = CartStatus.InsideGeofence,
                        IsDelivered = true,
                        CartId = cart.Id,
                        IssueType = cart.IssueType,
                        IssueDescription = cart.IssueDescription,
                        DeviceId = cart.DeviceId
                    };
                    var CartUpdate = new CartUpdateDto
                    {
                        ManufacturerName = cart.ManufacturerName,
                        DateManufactured = cart.DateManufactured,
                        OrderedFrom = cart.OrderedFrom,
                        Condition = cart.Condition,
                        Status = CartStatus.InsideGeofence,
                        PicUrl = cart.PicUrl,
                        IsProvisioned = cart.IsProvisioned,
                        DeviceId = cart.DeviceId,
                        BarCode = cart.BarCode,
                        StoreId = cart.StoreId,
                        CartNumber = cart.CartNumber,
                        IssueType = cart.IssueType,
                        IssueDescription = cart.IssueDescription,
                        CartHistory = CartHistory
                    };
                    try
                    {
                        await CartHttpRepository.UpdateCartAsync(cart.Id, CartUpdate);
                    }
                    catch
                    {
                        //Exception
                    }
                    await GetDeliveryTicketsByDriverId(CurrentUser.UserId);
                }
            }
        }
        #endregion

        #region Get Distance
        private double Getdistance(double lat2, double lon2)
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
                double dist = Math.Sin(degtorad(lat1)) * Math.Sin(degtorad(lat2)) + Math.Cos(degtorad(lat1)) * Math.Cos(degtorad(lat2)) * Math.Cos(degtorad(theta));
                dist = Math.Acos(dist);
                dist = radtodeg(dist);
                dist = dist * Numbers.Sixty * Numbers.OneOneFive;
                dist = dist * Numbers.OneSixZero;

                return (dist);
            }
        }
        private double degtorad(double deg)
        {
            return (deg * Math.PI / Numbers.OneEighty);
        }
        private double radtodeg(double rad)
        {
            return (rad / Math.PI * Numbers.OneEighty);
        }
        #endregion

        #region Find Region
        private void FindDistance()
        {
            Stores = Stores.Where(x => x.DriverStoresIsActive && x.IsActive).ToList();
            foreach (var item in Stores)
            {
                var distance = Getdistance(item.Latitude, item.Longitude);
                item.Distance = Convert.ToInt32(Math.Ceiling(distance));
            }
        }
        #endregion
    }
}
