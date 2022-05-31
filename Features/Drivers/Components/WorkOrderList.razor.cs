using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.Features.Drivers.Components
{
    public partial class WorkOrderList : ComponentBase
    {
         [Parameter]
        public List<WorkOrderDto>? WorkOrders { get; set; }       

        [Parameter]
        public EventCallback WorkOrderUpdatedEventCallback { get; set; }

        [Inject]
        IDialogService? DialogService { get; set; }          

        private bool _listIsLoading = true;

        protected override void OnParametersSet()
        {
            _listIsLoading = false;
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
