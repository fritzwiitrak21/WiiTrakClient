using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.Features.SystemOwner;
using WiiTrakClient.Features.SystemOwner.Components;
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

namespace WiiTrakClient.Features.SystemOwner
{
    public partial class DeliveryTickets : ComponentBase
    {
        
        [Inject] IJSRuntime JsRuntime { get; set; }

        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }

        List<DeliveryTicketDto> deliveryTickets = new();
        
        List<DeliveryTicketDto> _deliveryTickets = new();
        
        private IJSObjectReference JsModule;

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

                deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByPrimaryIdAsync(CurrentUser.UserId,(Role)CurrentUser.UserRoleId);
                if (deliveryTickets is not null)
                {
                    _deliveryTickets = deliveryTickets;
                }
            }
            catch (Exception ex)
            {
            }
        }

        

      
    }
}