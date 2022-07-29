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
namespace WiiTrakClient.Features.Stores.Components
{
    public partial class DeliveryTicketsList : ComponentBase, IAsyncDisposable
    {
        [Parameter]
        public List<DeliveryTicketDto>? DeliveryTickets { get; set; }
        [Parameter]
        public int RecordCount { get; set; }
        [Parameter]
        public EventCallback DeliveryTicketUpdatedEventCallback { get; set; }
        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }
        [Inject] public ICartHttpRepository CartRepository { get; set; }
        [Inject] public ICartHistoryHttpRepository CartHistoryHttpRepository { get; set; }
        [Inject] IDriverHttpRepository DriverRepository { get; set; }
        [Inject] public ICartHttpRepository CartHttpRepository { get; set; }
        [Inject] IWorkOrderHttpRepository WorkOrderHttpRepository { get; set; }
        [Inject]
        IJSRuntime js { get; set; }
        IJSObjectReference? module;
        [Inject]
        IDialogService? DialogService { get; set; }
        DriverDto SelectedDriver = new();
        List<CartDto> Carts = new();
        List<StoreDto> Stores = new();
        private bool ListIsLoading = true;
        List<DeliveryTicketDto> DeliveryTicket = new();
        DeliveryTicketUpdateDto EditDeliveryTicket = new();
        Guid DeliveryTicketId = Guid.Empty;
        List<CartDto>? CartsTable { get; set; } = new List<CartDto>();
        protected override async Task OnInitializedAsync()
        {
            //module = await _js.InvokeAsync<IJSObjectReference>("import", "./js/ss.js?$$REVISION$$");
            //module = await _js.InvokeAsync<IJSObjectReference>("import", "./js/ss.ui.js?$$REVISION$$");
            var TargetUrl = "js/ss.js?$$REVISION$$";
            await js.InvokeVoidAsync("loadJs", TargetUrl, "ssFile");
            TargetUrl = "js/ss.ui.js?$$REVISION$$";
            await js.InvokeVoidAsync("loadJs", TargetUrl, "ssUiFile");
        }
        protected override void OnParametersSet()
        {
            ListIsLoading = false;
        }
        public async Task OpenDeliveryTicketDialog(DeliveryTicketDto DeliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(DeliveryTicket.StoreId);
            var DeliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(DeliveryTicket.Id);
            CartsTable = await CartRepository.GetCartsByDeliveryTicketIdAsync(DeliveryTicket.Id);
            var SelectedCartList = await CartHistoryHttpRepository.GetCartHistoryByDeliveryTicketIdAsync(DeliveryTicket.Id);
            parameters.Add("SelectedCartList", SelectedCartList);
            parameters.Add("deliveryTicketDto", DeliveryTicket);
            parameters.Add("StoreName", store.StoreNumber + "-" + store.StoreName);
            parameters.Add("deliveryTicketSummary", DeliveryTicketSummary);
            parameters.Add("cartsTable", CartsTable);
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
                //if(_editDeliveryTicket.ApprovedByStore)
                //{
                //    var _carts = await CartRepository.GetCartsByDriverIdAsync(_editDeliveryTicket.DriverId);
                //    //foreach (var cart in _carts)
                //{
                //    if (cart.Condition == CartCondition.Damage)
                //    {
                //        var newWorkOrder = new WorkOrderCreationDto
                //        {
                //            Issue = cart.DamageIssue,
                //            Notes = "",
                //            CartId = cart.Id,
                //            StoreId = cart.Store != null ? cart.Store.Id : null
                //        };

                //        await WorkOrderHttpRepository.CreateWorkOrderAsync(newWorkOrder);
                //    }
                //}
                // }
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
                    IsActive=true
                };
                await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(DeliveryTicketId, DeliveryTicketUpdate);
            }
            await RefreshDeliveryTicket();
        }
        public async Task OpenUpdateDeliveryTicketDialog(DeliveryTicketDto DeliveryTicket)
        {
            try
            {
                SelectedDriver = await DriverRepository.GetDriverByIdAsync(DeliveryTicket.DriverId);
                Stores = await StoreHttpRepository.GetStoresByDriverId(DeliveryTicket.DriverId);
                Carts = await CartHttpRepository.GetCartsByDriverIdAsync(DeliveryTicket.DriverId);
                EditDeliveryTicket.StoreId = DeliveryTicket.StoreId;
                EditDeliveryTicket.PicUrl = DeliveryTicket.PicUrl;
                EditDeliveryTicket.DriverId = DeliveryTicket.DriverId;
                EditDeliveryTicket.NumberOfCarts = DeliveryTicket.NumberOfCarts;
                EditDeliveryTicket.ServiceProviderId = DeliveryTicket.ServiceProviderId;
                EditDeliveryTicket.DeliveryTicketNumber = DeliveryTicket.DeliveryTicketNumber;
                var parameters = new DialogParameters();
                parameters.Add("editDeliveryTicket", EditDeliveryTicket);
                parameters.Add("Driver", SelectedDriver);
                parameters.Add("Carts", Carts);
                parameters.Add("Stores", Stores);
                DeliveryTicketId = DeliveryTicket.Id;
                var dialog = DialogService.Show<UpdateDeliveryTicketDialog>("Edit Delivery Ticket", parameters);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    // add new delivery ticket to backend
                    var DeliveryTicketUpdate = new DeliveryTicketUpdateDto
                    {
                        NumberOfCarts = EditDeliveryTicket.NumberOfCarts,
                        PicUrl = EditDeliveryTicket.PicUrl,
                        DeliveredAt = EditDeliveryTicket.DeliveredAt,
                        StoreId = CurrentUser.UserId,
                        ServiceProviderId = EditDeliveryTicket.ServiceProviderId,
                        DriverId = EditDeliveryTicket.DriverId,
                        DeliveryTicketNumber = EditDeliveryTicket.DeliveryTicketNumber,
                        SignOffRequired = Stores.FirstOrDefault(x => x.Id == EditDeliveryTicket.StoreId).IsSignatureRequired
                    };
                    await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(DeliveryTicketId, DeliveryTicketUpdate);
                }
                await RefreshDeliveryTicket();
            }
            catch
            {
                //Exception
            }
                // var cartPreUpdate = cart;
                // var parameters = new DialogParameters();
                // parameters.Add("Cart", cart);
                // parameters.Add("RepairIssues", RepairIssues);
                // DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };
                // if (DialogService is null) return;
                // var dialog = DialogService.Show<UpdateCartDialog>("Update Cart", parameters);
                // var result = await dialog.Result;
                // if (!result.Cancelled)
                // {
                //     // save updated cart to backend
                //     var cartUpdate = new CartUpdateDto
                //     {
                //         ManufacturerName = cart.ManufacturerName,
                //         DateManufactured = cart.DateManufactured,
                //         OrderedFrom = cart.OrderedFrom,
                //         Condition = cart.Condition,
                //         Status = cart.Status,
                //         PicUrl = cart.PicUrl,
                //         IsProvisioned = cart.IsProvisioned,
                //         BarCode = cart.BarCode,
                //         StoreId = cart.StoreId
                //     };
                //     if (CartHttpRepository is null) return;
                //     await CartHttpRepository.UpdateCartAsync(cart.Id, cartUpdate);
                //     // pass update changes back to parent for driver summary
                //     var cartChange = new CartChange
                //     {
                //         Id = cart.Id,
                //         Status = cart.Status,
                //         Condition = cart.Condition,
                //         CreatedAt = DateTime.Now
                //     };
                //     await CartUpdatedEventCallback.InvokeAsync(cartChange);
                //}
        }
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            var TargetUrl = "js/ss.js?$$REVISION$$";
            await js.InvokeVoidAsync("unloadJs", TargetUrl, "ssFile");
            TargetUrl = "js/ss.ui.js?$$REVISION$$";
            await js.InvokeVoidAsync("unloadJs", TargetUrl, "ssUiFile");
            if (module is not null)
            {
                await module.DisposeAsync();
            }
            StateHasChanged();
        }
        async Task RefreshDeliveryTicket()
        {
            DeliveryTicket = await DeliveryTicketHttpRepository.GetDeliveryTicketsById(CurrentUser.UserId, (Role)CurrentUser.UserRoleId, RecordCount);
            if (DeliveryTicket is not null)
            {
                DeliveryTickets = DeliveryTicket;
            }
            StateHasChanged();
        }
    }
}
