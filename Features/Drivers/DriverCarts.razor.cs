using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.Features.Drivers.Models;
using WiiTrakClient.Features.Drivers.Components;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.DTOs;
using MudBlazor;
using WiiTrakClient.Enums;
using WiiTrakClient.Helpers;

namespace WiiTrakClient.Features.Drivers
{
    public partial class DriverCarts: ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }

        [Inject] ICartHttpRepository CartHttpRepository { get; set; }

        [Inject] IDriverHttpRepository DriverRepository { get; set; }

        [Inject] public IRepairIssueHttpRepository RepairIssueHttpRepository { get; set; }

        [Inject] IWorkOrderHttpRepository WorkOrderHttpRepository {get; set;}

        DriverDto _selectedDriver = new();
        List<DriverDto> _drivers = new();
        List<CartDto> _carts = new();
        List<CartDto> _filteredCarts = new();
        List<RepairIssueDto> _repairIssues = new();

        // Driver summary
        #region
        DriverSummary? _driverSummary;
        List<CartChange>? _cartChanges;
        const string _driverSummaryKey = "DriverSummary";
        const string _cartChangesKey = "CartChanges";
        const int _idleHours = 8;
        bool _isSummaryExpanded = true;
        #endregion

        private enum ViewOption
        {
            Map,
            List,
            Grid
        }

        ViewOption _view = ViewOption.Map;

        // Filter chip set
        // ref: https://mudblazor.com/components/chipset#api
        MudChip? listFilterChip;
        string listFilterOption1 = "Carts Out";
        string listFilterOption2 = "Carts on Truck";

        protected override async Task OnInitializedAsync()
        {
            _drivers = await DriverRepository.GetAllDriversAsync();
            await GetCartsByDriverId(_drivers[0].Id);
            _selectedDriver = _drivers[0];

            _repairIssues = await RepairIssueHttpRepository.GetAllRepairIssuesAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await ReadSummary();
        }

        private async Task HandleDriverSelected(DriverDto driver)
        {
            System.Console.WriteLine(driver.Id);
            await GetCartsByDriverId(driver.Id);
            _selectedDriver = driver;
        }

        private async Task GetCartsByDriverId(Guid id)
        {
            _carts = await CartHttpRepository.GetCartsByDriverIdAsync(id);
            _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();
            Console.WriteLine(_carts.Count());
            await UpdateDriverSummary();
        }

        private void ShowMapView()
        {
            Console.WriteLine("map view");
            _view = ViewOption.Map;
        }

        private void ShowListView()
        {
            Console.WriteLine("list view");
            _view = ViewOption.List;
        }

        public async Task CartUpdatedHandler(CartChange cartChange)
        {
            Console.WriteLine("CartUpdatedHandler");
            Console.WriteLine(cartChange.Id);

            if (listFilterChip is not null)
            {
                if (listFilterChip.Text.Equals(listFilterOption1))
                {
                    _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();
                }
                else if (listFilterChip.Text.Equals(listFilterOption2))
                {
                    _filteredCarts = _carts.Where(x => x.Status == CartStatus.PickedUp).ToList();
                }
            }
            else
            {
                _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();
            }

            await UpdateDriverSummary(cartChange);



            //
            // TODO driver summary should come from backend  using cart history
            //

            var cart = _carts.First(x => x.Id == cartChange.Id);

             var cartHistory = new CartHistoryUpdateDto {
                        ServiceProviderId = cart.Store != null ? cart.Store.ServiceProviderId : null,
                        StoreId = cart.StoreId,
                        DriverId = _selectedDriver.Id,
                        Condition = cart.Condition,
                        Status = CartStatus.PickedUp,
                        CartId = cart.Id
                    };

            var cartUpdate = new CartUpdateDto {
                ManufacturerName = cart.ManufacturerName,
                DateManufactured = cart.DateManufactured,
                OrderedFrom = cart.OrderedFrom,
                Condition = cart.Condition,
                Status = CartStatus.PickedUp,
                PicUrl = cart.PicUrl,
                IsProvisioned = cart.IsProvisioned,
                BarCode = cart.BarCode,
                StoreId = cart.StoreId,
                CartHistory = cartHistory                  
            };

            await CartHttpRepository.UpdateCartAsync(cart.Id, cartUpdate);


            if (cartUpdate.Condition == CartCondition.Damage) 
            {
                var newWorkOrder = new WorkOrderCreationDto {
                    Issue = cartChange.DamageIssue,
                    Notes = "",
                    CartId = cart.Id,
                    StoreId = cart.Store != null ? cart.Store.Id : null
                };

                await WorkOrderHttpRepository.CreateWorkOrderAsync(newWorkOrder);
            }


            StateHasChanged();
        }

        private async Task UpdateDriverSummary(CartChange? cartChange = null)
        {
            string jsonString = await JsRuntime.GetFromLocalStorage(_driverSummaryKey);
            if (string.IsNullOrEmpty(jsonString))
                _driverSummary = new DriverSummary { CreatedAt = DateTime.Now };
            else
                _driverSummary = JsonSerializer.Deserialize<DriverSummary>(jsonString);

            if (_driverSummary is null) return;

            jsonString = await JsRuntime.GetFromLocalStorage(_cartChangesKey);
            if (string.IsNullOrEmpty(jsonString))
                _cartChanges = new List<CartChange>();
            else
                _cartChanges = JsonSerializer.Deserialize<List<CartChange>>(jsonString);

            if (_cartChanges is null) return;

             _driverSummary.CartsOut = _carts.Count(x => x.Status == CartStatus.OutsideGeofence);
            _driverSummary.CartsOnTruck = _carts.Count(x => x.Status == CartStatus.PickedUp);

            if (cartChange is null)
            {
                _driverSummary.UpdatedAt = DateTime.Now;
                await SaveSummary();
                return;
            }

            // get last cart change object with same id, if any
            CartChange? lastCartChange = null;
            if (cartChange is not null && _cartChanges.Any(x => x.Id == cartChange.Id))
            {
                DateTime maxDateTime = _cartChanges.Max(x => x.CreatedAt);
                Console.WriteLine($"max date time: {maxDateTime}");
                lastCartChange = _cartChanges.FirstOrDefault(x => x.CreatedAt == maxDateTime);
            }           

            _cartChanges.Add(cartChange);

            // if only one cart then update using switch
            switch (cartChange.Status)
            {
                case CartStatus.InsideGeofence:
                    _driverSummary.CartsDelivered++;
                    break;
                case CartStatus.OutsideGeofence:
                    //_driverSummary.CartsOut++;
                    break;
                case CartStatus.PickedUp:
                    //_driverSummary.CartsOnTruck++;
                    break;
                case CartStatus.Lost:
                    _driverSummary.CartsLost++;
                    break;
            }

            switch (lastCartChange?.Status)
            {
                case CartStatus.InsideGeofence:
                    _driverSummary.CartsDelivered--;
                    break;
                case CartStatus.OutsideGeofence:
                    //_driverSummary.CartsOut--;
                    break;
                case CartStatus.PickedUp:
                    //_driverSummary.CartsOnTruck--;
                    break;
                case CartStatus.Lost:
                    _driverSummary.CartsLost--;
                    break;
            }

            switch (cartChange.Condition)
            {
                case CartCondition.Good:
                    _driverSummary.CartsGood++;
                    break;
                case CartCondition.Damage:
                    _driverSummary.CartsNeedRepair++;
                    break;
                case CartCondition.DamageBeyondRepair:
                    _driverSummary.CartsTrash++;
                    break;
            }

            switch (lastCartChange?.Condition)
            {
                case CartCondition.Good:
                    _driverSummary.CartsGood--;
                    break;
                case CartCondition.Damage:
                    _driverSummary.CartsNeedRepair--;
                    break;
                case CartCondition.DamageBeyondRepair:
                    _driverSummary.CartsTrash--;
                    break;
            }

            _driverSummary.UpdatedAt = DateTime.Now;
            await SaveSummary();
        }

        private async Task SaveSummary()
        {
            if (_driverSummary is null) return;
            string jsonString = JsonSerializer.Serialize(_driverSummary);
            await JsRuntime.SetInLocalStorage(_driverSummaryKey, jsonString);

            if (_cartChanges is null) return;
            jsonString = JsonSerializer.Serialize(_cartChanges);
            await JsRuntime.SetInLocalStorage(_cartChangesKey, jsonString);
        }

        private async Task ReadSummary()
        {
            string jsonString = await JsRuntime.GetFromLocalStorage(_driverSummaryKey);
            if (string.IsNullOrEmpty(jsonString)) return;
            _driverSummary = JsonSerializer.Deserialize<DriverSummary>(jsonString);


            //
            // TODO 
            // change back to hours
            // replace hardcode int with _idleHours
            //

            // reset driver summary if idle for n hours
            if (_driverSummary is not null)
            {
                var timeSpan = _driverSummary.UpdatedAt - _driverSummary.CreatedAt;

                Console.WriteLine($"minutes: {timeSpan.Minutes}");

                if (timeSpan.Minutes >= 5)
                {
                    _driverSummary = null;
                    _cartChanges = null;
                    await JsRuntime.SetInLocalStorage(_driverSummaryKey, string.Empty);
                    await JsRuntime.SetInLocalStorage(_cartChangesKey, string.Empty);
                }
            }
        }

        private void SummaryIsExpandedChangedHandler(bool isExpanded)
        {
            Console.WriteLine($"is expanded: {isExpanded}");
            _isSummaryExpanded = !isExpanded;
        }
    }
}