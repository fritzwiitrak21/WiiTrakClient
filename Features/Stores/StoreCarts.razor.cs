﻿using Microsoft.AspNetCore.Components;
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
        List<StoreDto> _mapStores = new();
        StoreDto SelectedStore;
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
            SelectedStore = _stores[0];
            await GetCartsByStoreId(SelectedStore);
            await UpdateStoreReport(SelectedStore.Id);
            StateHasChanged();
        }

        private async Task HandleStoreSelected(StoreDto store)
        {
            Console.WriteLine("HandleStoreSelected" + store.StoreName);

            System.Console.WriteLine(store.Id);
            await GetCartsByStoreId(store);
            SelectedStore = store;
            //await UpdateReport(SelectedCorporate.Id);
            await UpdateStoreReport(store.Id);
            StateHasChanged();
        }

        private async Task GetCartsByStoreId(StoreDto store)
        {
            _carts = await CartRepository.GetCartsByStoreIdAsync(store.Id);
            _mapStores = new List<StoreDto>();
            _mapStores.Add(store);
            if (_carts != null)
            {
                _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();
            }
            else
            {
                _filteredCarts = new List<CartDto>();
            }
            //await UpdateStoreReport(id);
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
