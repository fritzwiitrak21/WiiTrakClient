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

        private async Task HandleDriverSelected(DriverDto driver)
        {
            System.Console.WriteLine(driver.Id);
            _selectedDriver = driver;
            await GetDeliveryTicketsByDriverId(_selectedDriver.Id);
            _stores = await StoreHttpRepository.GetStoresByDriverId(_selectedDriver.Id);
            _carts = await CartHttpRepository.GetCartsByDriverIdAsync(_selectedDriver.Id);
        }
        private async Task HandleStoreSelected(StoreDto store)
        {
            System.Console.WriteLine(store.Id);
            _selectedStore = store;
            await GetDeliveryTicketsByStoreId(_selectedStore.Id);
            _carts = await CartHttpRepository.GetCartsByStoreIdAsync(_selectedStore.Id);
        }

        private async Task GetDeliveryTicketsByDriverId(Guid id)
        {
            var deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByDriverIdAsync(id);
            if (deliveryTickets is not null)
            {
                _deliveryTickets = deliveryTickets;
            }
        }

        private async Task GetDeliveryTicketsByStoreId(Guid id)
        {
            var deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsByStoreIdAsync(id);
            if (deliveryTickets is not null)
            {
                _deliveryTickets = deliveryTickets;
            }
        }
        private async Task OpenDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("NewDeliveryTicket", _newDeliveryTicket);
            parameters.Add("Driver", _selectedDriver);
            parameters.Add("Carts", _carts);
            parameters.Add("Stores", _stores);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<AddDeliveryTicketDialog>("New Delivery Ticket", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                // add new delivery ticket to backend
                var deliveryTicketCreation = new DeliveryTicketCreationDto
                {
                    NumberOfCarts = _newDeliveryTicket.NumberOfCarts,
                    PicUrl = _newDeliveryTicket.PicUrl,
                    DeliveredAt = DateTime.Now,
                    StoreId = _newDeliveryTicket.StoreId,
                    ServiceProviderId = _newDeliveryTicket.ServiceProviderId,
                    DriverId = _newDeliveryTicket.DriverId
                };

                var deliveryTicketResponse = await DeliveryTicketHttpRepository.CreateDeliveryTicketAsync(deliveryTicketCreation);

                // update status of carts to delivered and update cart hitory
                var carts = _carts.Where(x => x.StoreId == _newDeliveryTicket.StoreId).ToList();
                foreach (var cart in carts)
                {
                    var cartHistory = new CartHistoryUpdateDto
                    {
                        DeliveryTicketId = deliveryTicketResponse.Id,
                        PickupLatitude = cart.TrackingDevice != null ? cart.TrackingDevice.Latitude : 0,
                        PickupLongitude = cart.TrackingDevice != null ? cart.TrackingDevice.Longitude : 0,
                        DroppedOffAt = DateTime.Now,
                        ServiceProviderId = cart.Store != null ? cart.Store.ServiceProviderId : null,
                        StoreId = cart.StoreId,
                        DriverId = _selectedDriver.Id,
                        Condition = cart.Condition,
                        Status = CartStatus.InsideGeofence,
                        IsDelivered = true,
                        CartId = cart.Id
                    };

                    var cartUpdate = new CartUpdateDto
                    {
                        ManufacturerName = cart.ManufacturerName,
                        DateManufactured = cart.DateManufactured,
                        OrderedFrom = cart.OrderedFrom,
                        Condition = cart.Condition,
                        Status = CartStatus.InsideGeofence,
                        PicUrl = cart.PicUrl,
                        IsProvisioned = cart.IsProvisioned,
                        BarCode = cart.BarCode,
                        StoreId = cart.StoreId,
                        CartHistory = cartHistory
                    };

                    await CartHttpRepository.UpdateCartAsync(cart.Id, cartUpdate);
                }
            }
        }
    }
}
