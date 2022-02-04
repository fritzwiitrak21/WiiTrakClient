using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Stores
{
    public partial class StoreCarts : ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] ICartHttpRepository CartRepository { get; set; }
        [Inject] IStoreHttpRepository StoreRepository { get; set; }
        List<CartDto> _carts = new();
        List<StoreDto> _stores = new();

        StoreDto _selectedStore;
        List<CartDto> _filteredCarts = new();
        StoreReportDto _storeReport;

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

        protected override async Task OnInitializedAsync()
        {
            _stores = await StoreRepository.GetAllStoresAsync();
        }

        private async Task HandleStoreSelected(StoreDto store)
        {
            Console.WriteLine("HandleStoreSelected" + store.StoreName);

            System.Console.WriteLine(store.Id);
            await GetCartsByStoreId(store.Id);
            _selectedStore = store;
            //await UpdateReport(_selectedCorporate.Id);
            await UpdateStoreReport(store.Id);
            StateHasChanged();
        }

        private async Task GetCartsByStoreId(Guid id)
        {
            _carts = await CartRepository.GetCartsByStoreIdAsync(id);
            _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();
            await UpdateStoreReport(id);
        }

        private async Task UpdateStoreReport(Guid id)
        {
            _storeReport = await StoreRepository.GetStoreReportAsync(id);
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
    }
}
