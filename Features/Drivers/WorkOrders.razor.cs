/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.DTOs;
using MudBlazor;

namespace WiiTrakClient.Features.Drivers
{
    public partial class WorkOrders : ComponentBase
    {
         [Inject] IJSRuntime JsRuntime { get; set; }
         [Inject] IWorkOrderHttpRepository WorkOrderHttpRepository {get; set;}
         [Inject] IDriverHttpRepository DriverRepository { get; set; }
         [Inject] public ICartHttpRepository CartHttpRepository { get; set; }
         [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
         [Inject] IDialogService DialogService { get; set; }
         List<WorkOrderDto> _workOrders = new();
         DriverDto SelectedDriver = new();
         List<DriverDto> _drivers = new();
         List<CartDto> _carts = new();
         List<StoreDto> _stores = new();
        protected override async Task OnInitializedAsync()
        {
            _drivers = await DriverRepository.GetAllDriversAsync();
            SelectedDriver = _drivers[0];
            await HandleDriverSelected(SelectedDriver);
        }
        private async Task HandleDriverSelected(DriverDto driver)
        {
            SelectedDriver = driver;
             await GetWorkOrdersByDriverId(SelectedDriver.Id);
            //_stores = await StoreHttpRepository.GetStoresByDriverId(SelectedDriver.Id);
            //_carts = await CartHttpRepository.GetCartsByDriverIdAsync(SelectedDriver.Id);
        }
        private async Task GetWorkOrdersByDriverId(Guid id)
        {
            // TODO for now we are just getting all work orders
            // but this should be by driver
            var workOrders = await WorkOrderHttpRepository.GetAllWorkOrdersAsync();
            if (workOrders is not null)
            {
                _workOrders = workOrders;
            }
        }
        // private async Task OpenDialog()
        // {
        //     var parameters = new DialogParameters();
        //     parameters.Add("NewDeliveryTicket", _newDeliveryTicket);
        //     parameters.Add("Driver", SelectedDriver);
        //     parameters.Add("Carts", _carts);
        //     parameters.Add("Stores", _stores);
        //     DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };
        //     var dialog = DialogService.Show<AddDeliveryTicketDialog>("New Delivery Ticket", parameters);
        //     var result = await dialog.Result;
        //     if (!result.Cancelled)
        //     {
        //         // add new delivery ticket to backend
        //         var deliveryTicketCreation = new DeliveryTicketCreationDto
        //         {
        //             NumberOfCarts = _newDeliveryTicket.NumberOfCarts,
        //             PicUrl = _newDeliveryTicket.PicUrl,
        //             DeliveredAt = DateTime.Now,
        //             StoreId = _newDeliveryTicket.StoreId,
        //             ServiceProviderId = _newDeliveryTicket.ServiceProviderId,
        //             DriverId = _newDeliveryTicket.DriverId
        //         };
        //         var deliveryTicketResponse = await DeliveryTicketHttpRepository.CreateDeliveryTicketAsync(deliveryTicketCreation);
        //         // update status of carts to delivered and update cart hitory
        //         var carts = _carts.Where(x => x.StoreId == _newDeliveryTicket.StoreId).ToList();
        //         foreach(var cart in carts) 
        //         {
        //             var cartHistory = new CartHistoryUpdateDto {
        //                 DeliveryTicketId = deliveryTicketResponse.Id,
        //                 PickupLatitude = cart.TrackingDevice != null ? cart.TrackingDevice.Latitude : 0,
        //                 PickupLongitude = cart.TrackingDevice != null ? cart.TrackingDevice.Longitude: 0,
        //                 DroppedOffAt = DateTime.Now,
        //                 ServiceProviderId = cart.Store != null ? cart.Store.ServiceProviderId : null,
        //                 StoreId = cart.StoreId,
        //                 DriverId = SelectedDriver.Id,
        //                 Condition = cart.Condition,
        //                 Status = CartStatus.InsideGeofence,
        //                 IsDelivered = true,
        //                 CartId = cart.Id
        //             };
        //             var cartUpdate = new CartUpdateDto {
        //                 ManufacturerName = cart.ManufacturerName,
        //                 DateManufactured = cart.DateManufactured,
        //                 OrderedFrom = cart.OrderedFrom,
        //                 Condition = cart.Condition,
        //                 Status = CartStatus.InsideGeofence,
        //                 PicUrl = cart.PicUrl,
        //                 IsProvisioned = cart.IsProvisioned,
        //                 BarCode = cart.BarCode,
        //                 StoreId = cart.StoreId,
        //                 CartHistory = cartHistory                  
        //             };
        //             await CartHttpRepository.UpdateCartAsync(cart.Id, cartUpdate);
        //         }
        //     }
        // }
    }
}
