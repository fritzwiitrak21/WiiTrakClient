using Microsoft.AspNetCore.Components;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
using WiiTrakClient.Features.Drivers.Models;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Drivers.Components
{
    public partial class DeliveryTicketsList : ComponentBase
    {
        [Inject] IDriverHttpRepository DriverRepository { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }
        [Inject] public ICartHttpRepository CartHttpRepository { get; set; }

        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
        [Parameter]
        public List<DeliveryTicketDto>? DeliveryTickets { get; set; }       

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
                    DeliveredAt = DateTime.Now,
                    StoreId = _editDeliveryTicket.StoreId,
                    ServiceProviderId = _editDeliveryTicket.ServiceProviderId,
                    DriverId = _editDeliveryTicket.DriverId,
                    DeliveryTicketNumber = _editDeliveryTicket.DeliveryTicketNumber
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
                        Status = CartStatus.InsideGeofence,
                        IsDelivered = true,
                        CartId = cart.Id
                    };

                    var cartUpdate = new CartUpdateDto
                    {
                        ManufacturerName = cart.ManufacturerName,
                        DateManufactured = cart.DateManufactured,
                        OrderedFrom = cart.OrderedFrom,
                        Condition = cart.Condition,
                        Status = CartStatus.InsideGeofence,
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
    }    
}
