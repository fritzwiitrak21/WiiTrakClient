/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WiiTrakClient.Cores;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Shared.Components;
namespace WiiTrakClient.Features.Technicians.Components
{
    public partial class CartsList : ComponentBase
    {
        [Parameter]
        public List<CartDto>? Carts { get; set; } = null;
        [Inject]
        IDialogService? DialogService { get; set; }
        [Inject]
        ICartHttpRepository? CartHttpRepository { get; set; }
        [Inject]
        IStoreHttpRepository storeHttpRepository { get; set; }
        [Inject]
        IDevicesHttpRepository DevicesHttpRepository { get; set; }
        private bool ListIsLoading = true;
        public List<StoreDto> Store { get; set; } = null;
        public List<DevicesDto> DeviceList { get; set; } = null;
        private string SuccessMessage { get; set; } = "";
        protected override void OnParametersSet()
        {
            ListIsLoading = false;
        }
        public async Task OpenMapDevice(CartDto cart)
        {
            try
            {
                Store = await storeHttpRepository.GetStoresByTechnicianId(CurrentUser.UserId);
                DeviceList = await DevicesHttpRepository.GetDeviceByTechnicianIdAsync(CurrentUser.UserId);
            }
            catch (Exception ex)
            {
                //ex
            }
            var parameters = new DialogParameters();
            parameters.Add("Stores", Store);
            parameters.Add("DeviceList", DeviceList);
            parameters.Add("Cart", cart);
            var dialog = DialogService.Show<MapDeviceDialog>("Map Device", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {

                var CartUpdate = new CartUpdateDto
                {
                    CartNumber = cart.CartNumber,
                    ManufacturerName = cart.ManufacturerName,
                    DateManufactured = cart.DateManufactured,
                    OrderedFrom = cart.OrderedFrom,
                    Condition = cart.Condition,
                    Status = cart.Status,
                    StoreId = cart.StoreId,
                    IsActive = true,
                    DeviceId = cart.DeviceId,
                    CreatedBy = CurrentUser.UserId,
                    CartHistory=new()
                };
                await CartHttpRepository.UpdateCartAsync(cart.Id, CartUpdate);
            }
            Carts = await CartHttpRepository.GetCartsByTechnicianIdAsync(CurrentUser.UserId);
            StateHasChanged();
        }


        public async Task GetConfirmation(CartDto cart)
        {
            var parameters = new DialogParameters();
            SuccessMessage = "Are you sure do you want to remove " + cart.Device.DeviceModel+" - " + cart.Device.DeviceName + " Device from " + cart.ManufacturerName + "  #" + cart.CartNumber + "?";
            parameters.Add("DisplayMessage", SuccessMessage);
            parameters.Add("FromWindow", "carts");
            parameters.Add("IsSuccessNotification", true);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<ShowMessageDialog>("Confirmation Message", parameters);
            var result = await dialog.Result;
            try {
                if (!result.Cancelled)
                {

                    var CartUpdate = new CartUpdateDto
                    {
                        CartNumber = cart.CartNumber,
                        ManufacturerName = cart.ManufacturerName,
                        DateManufactured = cart.DateManufactured,
                        OrderedFrom = cart.OrderedFrom,
                        Condition = cart.Condition,
                        Status = cart.Status,
                        StoreId = cart.StoreId,
                        IsActive = true,
                        DeviceId = Guid.Empty,
                        CreatedBy = CurrentUser.UserId,
                        CartHistory = new()
                    };
                    await CartHttpRepository.UpdateCartAsync(cart.Id, CartUpdate);
                }
                Carts = await CartHttpRepository.GetCartsByTechnicianIdAsync(CurrentUser.UserId);
                StateHasChanged();
            }
            catch(Exception ex)
            {

            }
            }
    }
}