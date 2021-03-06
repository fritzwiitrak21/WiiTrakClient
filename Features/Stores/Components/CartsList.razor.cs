/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WiiTrakClient.Cores;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
namespace WiiTrakClient.Features.Stores.Components
{
    public partial class CartsList: ComponentBase
    {
        [Parameter]
        public List<CartDto>? Carts { get; set; } = null;
        [Inject]
        IDialogService? DialogService { get; set; }
        [Inject]
        ICartHttpRepository? CartHttpRepository { get; set; }
        private bool ListIsLoading = true;
        protected override void OnParametersSet()
        {
            ListIsLoading = false;
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
        public async Task OpenDialog(CartDto cart)
        {
            var parameters = new DialogParameters();
            parameters.Add("Cart", cart);
            var dialog = DialogService.Show<UpdateCartsDialog>("Update Cart", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                // save updated cart to backend
                var CartUpdate = new CartUpdateDto
                {
                    CartNumber = cart.CartNumber,
                    ManufacturerName = cart.ManufacturerName,
                    DateManufactured=cart.DateManufactured,
                    OrderedFrom = cart.OrderedFrom,
                    Condition = cart.Condition,
                    Status = cart.Status,
                    StoreId = CurrentUser.UserId,
                    CartHistory = new(),
                    IsActive=true,
                };
                try
                {
                    await CartHttpRepository.UpdateCartAsync(cart.Id, CartUpdate);
                }
                catch
                {
                    //Exception
                }
            }
            Carts = await CartHttpRepository.GetCartsByStoreIdAsync(CurrentUser.UserId);
            StateHasChanged();
        }
    }
}
