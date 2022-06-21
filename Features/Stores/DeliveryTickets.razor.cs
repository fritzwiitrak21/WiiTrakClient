/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Cores;
using WiiTrakClient.Enums;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Stores
{
    public partial class DeliveryTickets : ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] IDriverHttpRepository DriverRepository { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }
        [Inject] public ICartHttpRepository CartHttpRepository { get; set; }
        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        StoreDto SelectedStore = new();
        List<DeliveryTicketDto> _deliveryTickets = new();
        List<DeliveryTicketDto> deliveryTickets = new();
        DeliveryTicketCreationDto _newDeliveryTicket = new();
        private IJSObjectReference JsModule;
        public int SelectedOption = 30;
        public int TempSelectedOption = 0;
        protected override async Task OnInitializedAsync()
        {
            //_drivers = await DriverRepository.GetAllDriversAsync();
            if (CurrentUser.UserId == Guid.Empty)
            {
                JsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/localstorage.js");
                var Id = await JsModule.InvokeAsync<string>("getUserId");
                CurrentUser.UserId = new Guid(Id);
            }
            SelectedStore =  await StoreHttpRepository.GetStoreByIdAsync(CurrentUser.UserId);
            await HandleStoreSelected(SelectedStore);
        }
        private async Task HandleStoreSelected(StoreDto store)
        {
            await GetDeliveryTicketsByStoreId();
            ///_carts = await CartHttpRepository.GetCartsByStoreIdAsync(CurrentUser.UserId);
        }
        private async Task GetDeliveryTicketsByStoreId()
        {
            deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsById(CurrentUser.UserId,(Role)CurrentUser.UserRoleId, SelectedOption);
            if (deliveryTickets is not null)
            {
                _deliveryTickets = deliveryTickets;
            }
            else
            {
                _deliveryTickets = new();
            }
        }
        public async Task GetDeliveryTicketDetails()
        {
            if (TempSelectedOption != SelectedOption)
            {
                var value = SelectedOption;
                TempSelectedOption = SelectedOption;
                deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsById(CurrentUser.UserId, (Role)CurrentUser.UserRoleId, value);
                if (deliveryTickets is not null)
                {
                    _deliveryTickets = deliveryTickets;
                }
                StateHasChanged();

            }
        }
    }
}
