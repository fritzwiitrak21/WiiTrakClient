/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Cores;
using WiiTrakClient.Enums;
using WiiTrakClient.HttpRepository.Contracts;
namespace WiiTrakClient.Features.Drivers.Components
{
    public partial class DeliveryTicketsList : ComponentBase, IAsyncDisposable
    {
        [Inject] IDriverHttpRepository DriverRepository { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }
        [Inject] public ICartHttpRepository CartHttpRepository { get; set; }
        [Inject] public ICartHistoryHttpRepository CartHistoryHttpRepository { get; set; }
        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
        [Inject] ICartHttpRepository CartRepository { get; set; }
        [Parameter]
        public List<DeliveryTicketDto>? DeliveryTickets { get; set; }
        [Parameter]
        public int RecordCount { get; set; }
        [Inject]
        IJSRuntime js { get; set; }
        IJSObjectReference? module;
        [Parameter]
        public EventCallback DeliveryTicketUpdatedEventCallback { get; set; }
        [Inject]
        IDialogService? DialogService { get; set; }
        DriverDto SelectedDriver = new();
        private bool ListIsLoading = true;
        List<CartDto> Carts = new();
        List<StoreDto> Stores = new();
        List<StoreDto> TempStoreList = new();
        DeliveryTicketUpdateDto EditDeliveryTicket = new();
        Guid DeliveryTicketId = Guid.Empty;
        List<CartDto>? CartsTable { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            var TargetUrl = "js/ss.js?$$REVISION$$";
            await js.InvokeVoidAsync("loadJs", TargetUrl, "ssFile");
            TargetUrl = "js/ss.ui.js?$$REVISION$$";
            await js.InvokeVoidAsync("loadJs", TargetUrl, "ssUiFile");
            SelectedDriver = await DriverRepository.GetDriverByIdAsync(CurrentUser.UserId);
            TempStoreList = Stores = await StoreHttpRepository.GetStoresByDriverId(CurrentUser.UserId);
            Carts = await CartHttpRepository.GetCartsByDriverIdAsync(CurrentUser.UserId);
        }
        protected override void OnParametersSet()
        {
            ListIsLoading = false;
        }
        public async Task OpenUpdateDeliveryTicketDialog(DeliveryTicketDto deliveryTicket)
        {
            Stores = Stores.Where(x => x.DriverStoresIsActive && x.IsActive).ToList();
            EditDeliveryTicket.StoreId = deliveryTicket.StoreId;
            EditDeliveryTicket.PicUrl = deliveryTicket.PicUrl;
            EditDeliveryTicket.DriverId = deliveryTicket.DriverId;
            EditDeliveryTicket.NumberOfCarts = deliveryTicket.NumberOfCarts;
            EditDeliveryTicket.ServiceProviderId = deliveryTicket.ServiceProviderId;
            EditDeliveryTicket.DeliveryTicketNumber = deliveryTicket.DeliveryTicketNumber;
            DeliveryTicketId = deliveryTicket.Id;
            var parameters = new DialogParameters();
            parameters.Add("editDeliveryTicket", EditDeliveryTicket);
            parameters.Add("Driver", SelectedDriver);
            parameters.Add("Carts", Carts);
            parameters.Add("Stores", Stores);
            parameters.Add("DeliveryTicketId", DeliveryTicketId);
            bool SignOffRequired = false;
            SignOffRequired = deliveryTicket.SignOffRequired;
            var dialog = DialogService.Show<UpdateDeliveryTicketDialog>("Edit Delivery Ticket", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                // add new delivery ticket to backend
                var deliveryTicketUpdate = new DeliveryTicketUpdateDto
                {
                    NumberOfCarts = EditDeliveryTicket.NumberOfCarts,
                    PicUrl = EditDeliveryTicket.PicUrl,
                    DeliveredAt = DateTime.UtcNow,
                    StoreId = EditDeliveryTicket.StoreId,
                    ServiceProviderId = EditDeliveryTicket.ServiceProviderId,
                    DriverId = EditDeliveryTicket.DriverId,
                    DeliveryTicketNumber = EditDeliveryTicket.DeliveryTicketNumber,
                    IsActive = true,
                    SignOffRequired = SignOffRequired
                };
                try
                {
                    await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(DeliveryTicketId, deliveryTicketUpdate);
                }
                catch
                {
                    //Exception
                }
                await Refreshdeliveryticket();
                StateHasChanged();
                // update status of carts to delivered and update cart hitory
                var PickedUpCarts = await CartHttpRepository.GetCartsByStoreIdAsync(EditDeliveryTicket.StoreId);
                var carts = PickedUpCarts.Where(x => (x.Status == CartStatus.InsideGeofence || x.Status == CartStatus.PickedUp)).ToList();
                foreach (var cart in carts)
                {
                    var CartHistory = new CartHistoryUpdateDto
                    {
                        DeliveryTicketId = DeliveryTicketId,
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
                        CartNumber = cart.CartNumber,
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
                        IssueType = cart.IssueType,
                        IssueDescription = cart.IssueDescription,
                        CartHistory = CartHistory

                    };
                    if (EditDeliveryTicket.PickedUpCarts.Where(x => x.Id == cart.Id).ToList().Any())
                    {
                        CartUpdate.CartHistory.Status = CartStatus.InsideGeofence;
                        CartUpdate.Status = CartStatus.InsideGeofence;
                    }
                    else
                    {
                        CartUpdate.CartHistory.Status = CartStatus.PickedUp;
                        CartUpdate.Status = CartStatus.PickedUp;
                    }
                    try
                    {
                        await CartHttpRepository.UpdateCartAsync(cart.Id, CartUpdate);
                    }
                    catch
                    {
                        //Exception
                    }
                }
            }
            //var cartPreUpdate = cart;
            //var parameters = new DialogParameters();
            //parameters.Add("Cart", cart);
            //parameters.Add("RepairIssues", RepairIssues);
            //DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };
            //if (DialogService is null) return;
            //var dialog = DialogService.Show<UpdateCartDialog>("Update Cart", parameters);
            //var result = await dialog.Result;
            //if (!result.Cancelled)
            //{
            //    // save updated cart to backend
            //    var cartUpdate = new CartUpdateDto
            //    {
            //        ManufacturerName = cart.ManufacturerName,
            //        DateManufactured = cart.DateManufactured,
            //        OrderedFrom = cart.OrderedFrom,
            //        Condition = cart.Condition,
            //        Status = cart.Status,
            //        PicUrl = cart.PicUrl,
            //        IsProvisioned = cart.IsProvisioned,
            //        BarCode = cart.BarCode,
            //        StoreId = cart.StoreId
            //    };
            //    if (CartHttpRepository is null) return;
            //    await CartHttpRepository.UpdateCartAsync(cart.Id, cartUpdate);
            //    // pass update changes back to parent for driver summary
            //    var cartChange = new CartChange
            //    {
            //        Id = cart.Id,
            //        Status = cart.Status,
            //        Condition = cart.Condition,
            //        CreatedAt = DateTime.Now
            //    };
            //    await CartUpdatedEventCallback.InvokeAsync(cartChange);
            //}
        }
        public async Task OpenDeliveryTicketDialog(DeliveryTicketDto DeliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(DeliveryTicket.StoreId);
            var DeliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(DeliveryTicket.Id);
            CartsTable = await CartRepository.GetCartsByDeliveryTicketIdAsync(DeliveryTicket.Id);
            var SelectedCartList = await CartHistoryHttpRepository.GetCartHistoryByDeliveryTicketIdAsync(DeliveryTicket.Id);
            parameters.Add("deliveryTicketDto", DeliveryTicket);
            parameters.Add("StoreName", store.StoreNumber + "-" + store.StoreName);
            parameters.Add("deliveryTicketSummary", DeliveryTicketSummary);
            parameters.Add("cartsTable", CartsTable);
            parameters.Add("SelectedCartList", SelectedCartList);
            DialogService.Show<DeliveryTicketDetailsDialog>("Delivery Ticket Summary", parameters);
        }
        public async Task OpenSignatureDeliveryTicketDialog(DeliveryTicketDto DeliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(DeliveryTicket.StoreId);
            var DeliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(DeliveryTicket.Id);
            CartsTable = await CartRepository.GetCartsByDeliveryTicketIdAsync(DeliveryTicket.Id);
            EditDeliveryTicket.StoreId = DeliveryTicket.StoreId;
            EditDeliveryTicket.PicUrl = DeliveryTicket.PicUrl;
            EditDeliveryTicket.DriverId = DeliveryTicket.DriverId;
            EditDeliveryTicket.NumberOfCarts = DeliveryTicket.NumberOfCarts;
            EditDeliveryTicket.ServiceProviderId = DeliveryTicket.ServiceProviderId;
            EditDeliveryTicket.DeliveryTicketNumber = DeliveryTicket.DeliveryTicketNumber;
            EditDeliveryTicket.DeliveredAt = DeliveryTicket.DeliveredAt;
            EditDeliveryTicket.Signee = "";
            parameters.Add("deliveryTicketDto", DeliveryTicket);
            parameters.Add("StoreName", store.StoreNumber + "-" + store.StoreName);
            parameters.Add("deliveryTicketSummary", DeliveryTicketSummary);
            parameters.Add("editDeliveryTicket", EditDeliveryTicket);
            parameters.Add("cartsTable", CartsTable);
            DeliveryTicketId = DeliveryTicket.Id;
            var dialog = DialogService.Show<DeliveryTicketSignatureDialog>("Delivery Ticket Signature", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                //dont remove the code 
                #region 
                //if (EditDeliveryTicket.ApprovedByStore)
                //{
                //    var _carts = await CartRepository.GetCartsByDriverIdAsync(EditDeliveryTicket.DriverId);
                //    foreach (var cart in _carts)
                //    {
                //        if (cart.Condition == CartCondition.Damage)
                //        {
                //            var newWorkOrder = new WorkOrderCreationDto
                //            {
                //                Issue = cart.DamageIssue,
                //                Notes = "",
                //                CartId = cart.Id,
                //                StoreId = cart.Store != null ? cart.Store.Id : null
                //            };

                //            //await WorkOrderHttpRepository.CreateWorkOrderAsync(newWorkOrder);
                //        }
                //    }
                //}
                #endregion
                // add new delivery ticket to backend
                var DeliveryTicketUpdate = new DeliveryTicketUpdateDto
                {
                    NumberOfCarts = EditDeliveryTicket.NumberOfCarts,
                    PicUrl = EditDeliveryTicket.PicUrl,
                    DeliveredAt = EditDeliveryTicket.DeliveredAt,
                    StoreId = EditDeliveryTicket.StoreId,
                    ServiceProviderId = EditDeliveryTicket.ServiceProviderId,
                    DriverId = EditDeliveryTicket.DriverId,
                    DeliveryTicketNumber = EditDeliveryTicket.DeliveryTicketNumber,
                    ApprovedByStore = EditDeliveryTicket.ApprovedByStore,
                    SignOffRequired = EditDeliveryTicket.SignOffRequired,
                    SignaturePicUrl = EditDeliveryTicket.SignaturePicUrl,
                    Signee = EditDeliveryTicket.Signee,
                    IsActive = true
                };
                await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(DeliveryTicketId, DeliveryTicketUpdate);
            }
            await Refreshdeliveryticket();
        }
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            var targetUrl = "js/ss.js?$$REVISION$$";
            await js.InvokeVoidAsync("unloadJs", targetUrl, "ssFile");
            targetUrl = "js/ss.ui.js?$$REVISION$$";
            await js.InvokeVoidAsync("unloadJs", targetUrl, "ssUiFile");
            if (module is not null)
            {
                await module.DisposeAsync();
            }
            StateHasChanged();
        }
        #region Refreshdeliveryticket
        async Task Refreshdeliveryticket()
        {
            var Deliverytickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsById(CurrentUser.UserId, (Role)CurrentUser.UserRoleId, RecordCount);
            if (Deliverytickets is not null)
            {
                foreach (var item in Deliverytickets)
                {
                    try
                    {
                        item.DriverStoresIsActive = TempStoreList.FirstOrDefault(x => x.Id == item.StoreId).DriverStoresIsActive;
                        item.StoresIsActive = TempStoreList.FirstOrDefault(x => x.Id == item.StoreId).IsActive;
                    }
                    catch 
                    {
                        //Exception
                    }
                }
                DeliveryTickets = Deliverytickets;
            }
            StateHasChanged();
        }
        #endregion
    }
}
