using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.DTOs;
using MudBlazor;
using WiiTrakClient.Enums;
using WiiTrakClient.Helpers;

namespace WiiTrakClient.Features.Corporates
{
    public partial class CorporateCarts : ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] ICartHttpRepository CartRepository { get; set; }
        [Inject] ICorporateHttpRepository CorporateRepository { get; set; }
        [Inject] IStoreHttpRepository StoreRepository { get; set; }
        List<CorporateDto> _corporates = new();
        CorporateDto _selectedCorporate;
        List<CartDto> _carts = new();
        List<StoreDto> _stores = new();
        List<StoreDto> _mapStores = new();
        StoreDto _selectedStore;
        List<CartDto> _filteredCarts = new();
        List<RepairIssueDto> _repairIssues = new();
        CorporateReportDto _report;
        StoreReportDto _storeReport;
        bool mapCoporateBind = true;
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
            _corporates = await CorporateRepository.GetAllCorporatesAsync();
            _selectedCorporate = _corporates[0];
            await GetCartsByCorporateId(_selectedCorporate.Id);
            await UpdateReport(_selectedCorporate.Id);
            await GetStoreListByCoroporateId();
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // await ReadSummary();
        }

        private async Task HandleCorporateSelected(CorporateDto corporate)
        {
            System.Console.WriteLine(corporate.Id);
            mapCoporateBind = true;
            await GetCartsByCorporateId(corporate.Id);
            _selectedCorporate = corporate;
            await UpdateReport(corporate.Id);
            await GetStoreListByCoroporateId();
            StateHasChanged();
        }

        private async Task HandleStoreSelected(StoreDto store)
        {
            Console.WriteLine("HandleStoreSelected" + store.StoreName);
            mapCoporateBind = false;
            System.Console.WriteLine(store.Id);
            if (store.Id == Guid.Empty)
            {
                await GetAllStoreCartsByCorporateId(_selectedCorporate.Id);
            }
            else
            {
                await GetCartsByStoreId(store);
            }
            _selectedStore = store;
            //await UpdateReport(_selectedCorporate.Id);
            await UpdateStoreReport(store.Id);
            StateHasChanged();
        }

        private async Task GetStoreListByCoroporateId()
        {
            _stores = new List<StoreDto>();
            var storelist = await StoreRepository.GetStoresByCorporateId(_selectedCorporate.Id);
            var AllStore = new StoreDto()
            {
                Id = Guid.Empty,
                StoreName = "All",
            };
            _stores.Add(AllStore);
            foreach (var store in storelist)
            {
                _stores.Add(store);
            }
            if (_stores != null)
            {
                _selectedStore = _stores.Where(x => x.Id == Guid.Empty).FirstOrDefault();
            }
            await UpdateStoreReport(_selectedStore.Id);
        }

        private async Task GetCartsByCorporateId(Guid id)
        {
            _carts = await CartRepository.GetCartsByCorporateIdAsync(id);
            _mapStores = new List<StoreDto>();
            _mapStores = await StoreRepository.GetStoresByCorporateId(id);
            _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();            
        }

        private async Task GetAllStoreCartsByCorporateId(Guid id)
        {
            _carts = await CartRepository.GetCartsByCorporateIdAsync(id);
            _mapStores = new List<StoreDto>();
            _mapStores = await StoreRepository.GetStoresByCorporateId(id);
            _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();
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
        }

        private async Task UpdateReport(Guid id)
        {
            _report = new CorporateReportDto();
            _report = await CorporateRepository.GetCorporateReportAsync(id);
        }

        private async Task UpdateStoreReport(Guid id)
        {
            if (id != Guid.Empty)
            {
                _storeReport = new StoreReportDto();
                _storeReport = await StoreRepository.GetStoreReportAsync(id);
            }
            else
            {
                _storeReport = new StoreReportDto();
                _storeReport = await StoreRepository.GetAllStoreReportByCoprporateAsync(_selectedCorporate.Id);
            }
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
