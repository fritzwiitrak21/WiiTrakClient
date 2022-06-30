/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.DTOs;
using WiiTrakClient.Cores;
using WiiTrakClient.Enums;

namespace WiiTrakClient.Features.SystemOwner
{
    public partial class DeliveryTickets : ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }
        List<DeliveryTicketDto> deliveryTickets = new();
        List<DeliveryTicketDto> _deliveryTickets = new();
        private IJSObjectReference JsModule;
        int SelectedOption = 30;
        int TempSelectedOption;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (CurrentUser.UserId == Guid.Empty)
                {
                    JsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/localstorage.js");
                    var Id = await JsModule.InvokeAsync<string>("getUserId");
                    CurrentUser.UserId = new Guid(Id);
                    var roleid = await JsModule.InvokeAsync<string>("getUserRoleId");
                    CurrentUser.UserRoleId = Convert.ToInt32(roleid);
                }
                deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsById(CurrentUser.UserId,(Role)CurrentUser.UserRoleId, SelectedOption);
                if (deliveryTickets is not null)
                {
                    _deliveryTickets = deliveryTickets;
                }
            }
            catch (Exception)
            {
                //exception
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
