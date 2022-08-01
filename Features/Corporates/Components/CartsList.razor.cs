/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Corporates.Components
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
    }
}
