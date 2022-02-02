using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
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

        DriverDto _selectedDriver = new();
        StoreDto _selectedStore = new();
        List<DriverDto> _drivers = new();
        List<DeliveryTicketDto> _deliveryTickets = new();
        List<CartDto> _carts = new();
        List<StoreDto> _stores = new();
        DeliveryTicketCreationDto _newDeliveryTicket = new();

        protected override async Task OnInitializedAsync()
        {
            //_drivers = await DriverRepository.GetAllDriversAsync();
            _stores = await StoreHttpRepository.GetAllStoresAsync();
            _selectedStore = _stores[0];

            await HandleStoreSelected(_selectedStore);
        }

        private async Task HandleStoreSelected(StoreDto store)
        {
            System.Console.WriteLine(store.Id);
            _selectedStore = store;
            await GetDeliveryTicketsByStoreId(_selectedStore.Id);
            _carts = await CartHttpRepository.GetCartsByStoreIdAsync(_selectedStore.Id);
        }

        private async Task GetDeliveryTicketsByStoreId(Guid id)
        {
            var deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByStoreIdAsync(id);
            if (deliveryTickets is not null)
            {
                _deliveryTickets = deliveryTickets;
            }
        }
    }
}
