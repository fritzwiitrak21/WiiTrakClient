using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.Features.Companies;
using WiiTrakClient.Features.Companies.Components;
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

namespace WiiTrakClient.Features.Companies
{
    public partial class DeliveryTickets : ComponentBase
    {

        [Inject] IJSRuntime JsRuntime { get; set; }

        //[Inject] IDriverHttpRepository DriverRepository { get; set; }

        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }

        //[Inject] public ICartHttpRepository CartHttpRepository { get; set; }

        //[Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }

        [Inject] IDialogService DialogService { get; set; }

        //[Inject] IWorkOrderHttpRepository WorkOrderHttpRepository { get; set; }

        //DriverDto _selectedDriver = new();
        //List<DriverDto> _drivers = new();
        List<DeliveryTicketDto> _deliveryTickets = new();
        //List<CartDto> _carts = new();
        //List<StoreDto> _stores = new();
        //DeliveryTicketCreationDto _newDeliveryTicket = new();

        protected override async Task OnInitializedAsync()
        {

            try
            {
                var deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByPrimaryIdAsync(CurrentUser.UserId, (Role)CurrentUser.UserRoleId);
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