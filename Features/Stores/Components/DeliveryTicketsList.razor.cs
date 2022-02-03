﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Stores.Components
{
    public partial class DeliveryTicketsList: ComponentBase
    {
        [Parameter]
        public List<DeliveryTicketDto>? DeliveryTickets { get; set; }

        [Parameter]
        public EventCallback DeliveryTicketUpdatedEventCallback { get; set; }
        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }

        [Inject]
        IDialogService? DialogService { get; set; }

        private bool _listIsLoading = true;
        DeliveryTicketUpdateDto _editDeliveryTicket = new();
        Guid deliveryTicketId = Guid.Empty;
        protected override void OnParametersSet()
        {
            _listIsLoading = false;
        }

        public async Task OpenDeliveryTicketDialog(DeliveryTicketDto deliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(deliveryTicket.StoreId);
            var deliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(deliveryTicket.Id);
            parameters.Add("deliveryTicketDto", deliveryTicket);
            parameters.Add("StoreName", store.StoreNumber + "-" + store.StoreName);
            parameters.Add("deliveryTicketSummary", deliveryTicketSummary);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<DeliveryTicketDetailsDialog>("Delivery Ticket Summary", parameters);
        }

        public async Task OpenSignatureDeliveryTicketDialog(DeliveryTicketDto deliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(deliveryTicket.StoreId);
            var deliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(deliveryTicket.Id);

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
            deliveryTicketId = deliveryTicket.Id;
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<DeliveryTicketSignatureDialog>("Delivery Ticket Signature", parameters);

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
                    ApprovedByStore = _editDeliveryTicket.ApprovedByStore,
                    SignOffRequired = _editDeliveryTicket.SignOffRequired,  
                    SignaturePicUrl = _editDeliveryTicket.SignaturePicUrl
                };

                await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(deliveryTicketId, deliveryTicketUpdate);

                // update Delivery Ticket List
                DeliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByStoreIdAsync(_editDeliveryTicket.StoreId);
            }
        }

        public async Task OpenUpdateDeliveryTicketDialog(DeliveryTicketDto deliveryTicket)

        {
            Console.WriteLine("OpenUpdateDeliveryTickerDialog()");

            // var cartPreUpdate = cart;

            // Console.WriteLine("cart id: " + cart.Id);

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
    }
}
