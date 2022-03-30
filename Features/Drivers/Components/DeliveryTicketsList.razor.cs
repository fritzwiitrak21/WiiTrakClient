using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
using WiiTrakClient.Features.Drivers.Models;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Drivers.Components
{
    public partial class DeliveryTicketsList : ComponentBase, IAsyncDisposable
    {
        [Inject] IDriverHttpRepository DriverRepository { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }
        [Inject] public ICartHttpRepository CartHttpRepository { get; set; }

        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
        [Inject] ICartHttpRepository CartRepository { get; set; }
        [Parameter]
        public List<DeliveryTicketDto>? DeliveryTickets { get; set; }
        [Inject]
        IJSRuntime _js { get; set; }
        IJSObjectReference? module;

        [Parameter]
        public EventCallback DeliveryTicketUpdatedEventCallback { get; set; }

        [Inject]
        IDialogService? DialogService { get; set; }

        DriverDto selectedDriver = new();
        StoreDto selectedStoreDto = new();
        private bool _listIsLoading = true;
        List<DriverDto> _drivers = new();
        List<DeliveryTicketDto> _deliveryTickets = new();
        List<CartDto> _carts = new();
        List<StoreDto> _stores = new();
        DeliveryTicketUpdateDto _editDeliveryTicket = new();
        Guid deliveryTicketId = Guid.Empty;
        List<CartDto>? cartsTable { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            //module = await _js.InvokeAsync<IJSObjectReference>("import", "./js/ss.js?$$REVISION$$");
            //module = await _js.InvokeAsync<IJSObjectReference>("import", "./js/ss.ui.js?$$REVISION$$");
            var targetUrl = "js/ss.js?$$REVISION$$";
            await _js.InvokeVoidAsync("loadJs", targetUrl, "ssFile");

            targetUrl = "js/ss.ui.js?$$REVISION$$";
            await _js.InvokeVoidAsync("loadJs", targetUrl, "ssUiFile");

        }
        protected override void OnParametersSet()
        {
            _listIsLoading = false;
        }

        public async Task OpenUpdateDeliveryTicketDialog(DeliveryTicketDto deliveryTicket) 
        {
            Console.WriteLine("OpenUpdateDeliveryTickerDialog()");
            selectedDriver = await DriverRepository.GetDriverByIdAsync(deliveryTicket.DriverId);
            _stores = await StoreHttpRepository.GetStoresByDriverId(deliveryTicket.DriverId);
            _carts = await CartHttpRepository.GetCartsByDriverIdAsync(deliveryTicket.DriverId);
            _editDeliveryTicket.StoreId = deliveryTicket.StoreId;
            _editDeliveryTicket.PicUrl = deliveryTicket.PicUrl;
            _editDeliveryTicket.DriverId = deliveryTicket.DriverId;
            _editDeliveryTicket.NumberOfCarts = deliveryTicket.NumberOfCarts;
            _editDeliveryTicket.ServiceProviderId = deliveryTicket.ServiceProviderId;
            _editDeliveryTicket.DeliveryTicketNumber = deliveryTicket.DeliveryTicketNumber;
            var parameters = new DialogParameters();
            parameters.Add("editDeliveryTicket", _editDeliveryTicket);
            parameters.Add("Driver", selectedDriver);
            parameters.Add("Carts", _carts);
            parameters.Add("Stores", _stores);
            
            deliveryTicketId = deliveryTicket.Id;
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<UpdateDeliveryTicketDialog>("Edit Delivery Ticket", parameters);

            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                // add new delivery ticket to backend
                var deliveryTicketUpdate = new DeliveryTicketUpdateDto
                {
                    NumberOfCarts = _editDeliveryTicket.NumberOfCarts,
                    PicUrl = _editDeliveryTicket.PicUrl,
                    DeliveredAt = _editDeliveryTicket.DeliveredAt,
                    StoreId = _editDeliveryTicket.StoreId,
                    ServiceProviderId = _editDeliveryTicket.ServiceProviderId,
                    DriverId = _editDeliveryTicket.DriverId,
                    DeliveryTicketNumber = _editDeliveryTicket.DeliveryTicketNumber,
                     SignOffRequired = _stores.FirstOrDefault(x => x.Id == _editDeliveryTicket.StoreId).IsSignatureRequired

                };

                await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(deliveryTicketId,deliveryTicketUpdate);

                // update status of carts to delivered and update cart hitory
                var carts = _carts.Where(x => x.StoreId == _editDeliveryTicket.StoreId).ToList();
                foreach (var cart in carts)
                {
                    var cartHistory = new CartHistoryUpdateDto
                    {
                        DeliveryTicketId = deliveryTicketId,
                        PickupLatitude = cart.TrackingDevice != null ? cart.TrackingDevice.Latitude : 0,
                        PickupLongitude = cart.TrackingDevice != null ? cart.TrackingDevice.Longitude : 0,
                        DroppedOffAt = DateTime.Now,
                        ServiceProviderId = cart.Store != null ? cart.Store.ServiceProviderId : null,
                        StoreId = cart.StoreId,
                        DriverId = selectedDriver.Id,
                        Condition = cart.Condition,
                        Status = CartStatus.PickedUp,// CartStatus.InsideGeofence, Need to change in connected asserts
                        IsDelivered = true,
                        CartId = cart.Id
                    };

                    var cartUpdate = new CartUpdateDto
                    {
                        ManufacturerName = cart.ManufacturerName,
                        DateManufactured = cart.DateManufactured,
                        OrderedFrom = cart.OrderedFrom,
                        Condition = cart.Condition,
                        Status = CartStatus.PickedUp,// CartStatus.InsideGeofence, Need to change in connected asserts
                        PicUrl = cart.PicUrl,
                        IsProvisioned = cart.IsProvisioned,
                        BarCode = cart.BarCode,
                        StoreId = cart.StoreId,
                        CartHistory = cartHistory
                    };

                    await CartHttpRepository.UpdateCartAsync(cart.Id, cartUpdate);
                }
            }


            //var cartPreUpdate = cart;

            //Console.WriteLine("cart id: " + cart.Id);

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

        public async Task OpenDeliveryTicketDialog(DeliveryTicketDto deliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(deliveryTicket.StoreId);
            var deliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(deliveryTicket.Id);
            cartsTable = await CartRepository.GetCartsByDeliveryTicketIdAsync(deliveryTicket.Id);
            parameters.Add("deliveryTicketDto", deliveryTicket);
            parameters.Add("StoreName", store.StoreNumber + "-" + store.StoreName);
            parameters.Add("deliveryTicketSummary", deliveryTicketSummary);
            parameters.Add("cartsTable", cartsTable);
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<DeliveryTicketDetailsDialog>("Delivery Ticket Summary", parameters);
        }

        public async Task OpenSignatureDeliveryTicketDialog(DeliveryTicketDto deliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(deliveryTicket.StoreId);
            var deliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(deliveryTicket.Id);
            cartsTable = await CartRepository.GetCartsByDeliveryTicketIdAsync(deliveryTicket.Id);
            _editDeliveryTicket.StoreId = deliveryTicket.StoreId;
            _editDeliveryTicket.PicUrl = deliveryTicket.PicUrl;
            _editDeliveryTicket.DriverId = deliveryTicket.DriverId;
            _editDeliveryTicket.NumberOfCarts = deliveryTicket.NumberOfCarts;
            _editDeliveryTicket.ServiceProviderId = deliveryTicket.ServiceProviderId;
            _editDeliveryTicket.DeliveryTicketNumber = deliveryTicket.DeliveryTicketNumber;
            _editDeliveryTicket.DeliveredAt = deliveryTicket.DeliveredAt;
            parameters.Add("deliveryTicketDto", deliveryTicket);
            parameters.Add("StoreName", store.StoreNumber + "-" + store.StoreName);
            parameters.Add("deliveryTicketSummary", deliveryTicketSummary);
            parameters.Add("editDeliveryTicket", _editDeliveryTicket);
            parameters.Add("cartsTable", cartsTable);
            deliveryTicketId = deliveryTicket.Id;
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<DeliveryTicketSignatureDialog>("Delivery Ticket Signature", parameters);

            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                if (_editDeliveryTicket.ApprovedByStore)
                {
                    var _carts = await CartRepository.GetCartsByDriverIdAsync(_editDeliveryTicket.DriverId);
                    foreach (var cart in _carts)
                    {
                        if (cart.Condition == CartCondition.Damage)
                        {
                            var newWorkOrder = new WorkOrderCreationDto
                            {
                                Issue = cart.DamageIssue,
                                Notes = "",
                                CartId = cart.Id,
                                StoreId = cart.Store != null ? cart.Store.Id : null
                            };

                            //await WorkOrderHttpRepository.CreateWorkOrderAsync(newWorkOrder);
                        }
                    }
                }
                // add new delivery ticket to backend
                var deliveryTicketUpdate = new DeliveryTicketUpdateDto
                {
                    NumberOfCarts = _editDeliveryTicket.NumberOfCarts,
                    PicUrl = _editDeliveryTicket.PicUrl,
                    DeliveredAt = _editDeliveryTicket.DeliveredAt,
                    StoreId = _editDeliveryTicket.StoreId,
                    ServiceProviderId = _editDeliveryTicket.ServiceProviderId,
                    DriverId = _editDeliveryTicket.DriverId,
                    DeliveryTicketNumber = _editDeliveryTicket.DeliveryTicketNumber,
                    ApprovedByStore = _editDeliveryTicket.ApprovedByStore,
                    SignOffRequired = _editDeliveryTicket.SignOffRequired,
                    SignaturePicUrl = _editDeliveryTicket.SignaturePicUrl,
                    Signee=_editDeliveryTicket.Signee 
                };

                await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(deliveryTicketId, deliveryTicketUpdate);

                // update Delivery Ticket List
                DeliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByStoreIdAsync(_editDeliveryTicket.StoreId);
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            var targetUrl = "js/ss.js?$$REVISION$$";
            await _js.InvokeVoidAsync("unloadJs", targetUrl, "ssFile");

            targetUrl = "js/ss.ui.js?$$REVISION$$";
            await _js.InvokeVoidAsync("unloadJs", targetUrl, "ssUiFile");
            if (module is not null)
            {
                await module.DisposeAsync();
            }
            StateHasChanged();
        }
    }    
}
