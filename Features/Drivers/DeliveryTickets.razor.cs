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

        DriverDto _selectedDriver = new();
        //List<DriverDto> _drivers = new();
        List<DeliveryTicketDto> _deliveryTickets = new();
        List<DeliveryTicketDto> deliveryTickets = new();
        List<CartDto> _carts = new();
        List<StoreDto> _stores = new();
        DeliveryTicketCreationDto _newDeliveryTicket = new();

        protected override async Task OnInitializedAsync()
        {
           
            try
            {
                await GetDeliveryTicketsByDriverId(CurrentUser.UserId);
                _selectedDriver = await DriverRepository.GetDriverByIdAsync(CurrentUser.UserId);
            }
            catch (Exception ex)
            {

                
            }
           await HandleDriverSelected();
        }

        private async Task HandleDriverSelected()
        {
            _stores = await StoreHttpRepository.GetStoresByDriverId(CurrentUser.UserId);
            _carts = await CartHttpRepository.GetCartsByDriverIdAsync(CurrentUser.UserId);
        }

        private async Task GetDeliveryTicketsByDriverId(Guid id)
        {
            deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByDriverIdAsync(id);
            if (deliveryTickets is not null)
            {
                _deliveryTickets = deliveryTickets;
            }
           
        }
        private async Task OpenDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("NewDeliveryTicket", _newDeliveryTicket);
            parameters.Add("Driver", _selectedDriver);
            parameters.Add("Carts", _carts);
            parameters.Add("Stores", _stores);

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
                    SignOffRequired=_stores.FirstOrDefault(x=>x.Id== _newDeliveryTicket.StoreId).IsSignatureRequired
                };


                var deliveryTicketResponse = await DeliveryTicketHttpRepository.CreateDeliveryTicketAsync(deliveryTicketCreation);
                await GetDeliveryTicketsByDriverId(CurrentUser.UserId);//Refreshing the data in the grid once new ticket added
                // update status of carts to delivered and update cart hitory
                var carts = _carts.Where(x => x.StoreId == _newDeliveryTicket.StoreId).ToList();
                foreach(var cart in carts) 
                {
                    if(!deliveryTicketResponse.SignOffRequired)
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
                    var cartHistory = new CartHistoryUpdateDto {
                        DeliveryTicketId = deliveryTicketResponse.Id,
                        PickupLatitude = cart.TrackingDevice != null ? cart.TrackingDevice.Latitude : 0,
                        PickupLongitude = cart.TrackingDevice != null ? cart.TrackingDevice.Longitude: 0,
                        DroppedOffAt = DateTime.Now,
                        ServiceProviderId = cart.Store != null ? cart.Store.ServiceProviderId : null,
                        StoreId = cart.StoreId,
                        DriverId = CurrentUser.UserId,
                        Condition = cart.Condition,
                        Status = CartStatus.PickedUp,// CartStatus.InsideGeofence,
                        IsDelivered = true,
                        CartId = cart.Id
                    };

                    var cartUpdate = new CartUpdateDto {
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

      
    }
}