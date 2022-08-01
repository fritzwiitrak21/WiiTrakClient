/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Features.Drivers.Models;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Drivers.Components
{
    public partial class CartsList: ComponentBase
    {
        [Parameter]
        public List<CartDto>? Carts { get; set; } = null;
        [Parameter]
        public List<RepairIssueDto>? RepairIssues { get; set; } = null;
        [Parameter]
        public EventCallback<CartChange> CartUpdatedEventCallback { get; set; }
        [Inject]
        IDialogService? DialogService { get; set; }
        [Inject]
        ICartHttpRepository? CartHttpRepository { get; set; }       
        private bool ListIsLoading = true;
        protected override void OnParametersSet()
        {
            ListIsLoading = false;
        }
        public async Task OpenUpdateCartDialog(CartDto cart)        
        {
            var parameters = new DialogParameters();
            parameters.Add("Cart", cart);
            parameters.Add("RepairIssues", RepairIssues);
            if (DialogService is null)
            {
                return;
            }
            var dialog = DialogService.Show<UpdateCartDialog>("Update Cart", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                // save updated cart to backend
                var cartUpdate = new CartUpdateDto
                {
                    ManufacturerName = cart.ManufacturerName,
                    DateManufactured = cart.DateManufactured,
                    OrderedFrom = cart.OrderedFrom,
                    Condition = cart.Condition,
                    Status = cart.Status,
                    PicUrl = cart.PicUrl,
                    IsProvisioned = cart.IsProvisioned,
                    BarCode = cart.BarCode,
                    StoreId = cart.StoreId
                };
                if (CartHttpRepository is null)
                {
                    return;
                }
                await CartHttpRepository.UpdateCartAsync(cart.Id, cartUpdate);
                // pass update changes back to parent for driver summary
                var cartChange = new CartChange
                {
                    Id = cart.Id,
                    Status = cart.Status,
                    Condition = cart.Condition,
                    CreatedAt = DateTime.Now,
                    DamageIssue = cart.DamageIssue
                };
                await CartUpdatedEventCallback.InvokeAsync(cartChange);
            }
        }
        public async Task OpenCartDetailsDialog(CartDto cart) 
        {
            var parameters = new DialogParameters();
            parameters.Add("Cart", cart);
            if (DialogService is null)
            {
                return;
            }
            var dialog = DialogService.Show<CartDetailsDialog>("Cart Details", parameters);
            await dialog.Result;
        }
    }
}
