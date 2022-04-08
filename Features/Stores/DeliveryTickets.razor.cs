using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Cores;
using WiiTrakClient.Enums;
using WiiTrakClient.Features.Drivers.Components;
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

        ///DriverDto _selectedDriver = new();
        ///List<CartDto> _carts = new();
        ///List<DriverDto> _drivers = new();
        StoreDto _selectedStore = new();
        List<DeliveryTicketDto> _deliveryTickets = new();
        List<DeliveryTicketDto> deliveryTickets = new();


        DeliveryTicketCreationDto _newDeliveryTicket = new();

        protected override async Task OnInitializedAsync()
        {
            //_drivers = await DriverRepository.GetAllDriversAsync();
            _selectedStore =  await StoreHttpRepository.GetStoreByIdAsync(CurrentUser.UserId);
            await HandleStoreSelected(_selectedStore);
        }

        private async Task HandleStoreSelected(StoreDto store)
        {
            await GetDeliveryTicketsByStoreId();
            ///_carts = await CartHttpRepository.GetCartsByStoreIdAsync(CurrentUser.UserId);
        }

        private async Task GetDeliveryTicketsByStoreId()
        {
             deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByStoreIdAsync(CurrentUser.UserId);
            if (deliveryTickets is not null)
            {
                _deliveryTickets = deliveryTickets;
            }
            else
            {
                _deliveryTickets = new();
            }
        }
    }
}
