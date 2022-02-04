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

        StoreDto _selectedStore;
        List<CartDto> _filteredCarts = new();
        List<RepairIssueDto> _repairIssues = new();
        CorporateReportDto _report;
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
            _corporates = await CorporateRepository.GetAllCorporatesAsync();
            _selectedCorporate = _corporates[0];
            await GetCartsByCorporateId(_selectedCorporate.Id);
            _stores = await StoreRepository.GetStoresByCorporateId(_selectedCorporate.Id);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // await ReadSummary();
        }

        private async Task HandleCorporateSelected(CorporateDto corporate)
        {
            System.Console.WriteLine(corporate.Id);
            await GetCartsByCorporateId(corporate.Id);
            _selectedCorporate = corporate;
            await UpdateReport(_selectedCorporate.Id);
            _stores = await StoreRepository.GetStoresByCorporateId(_selectedCorporate.Id);
            StateHasChanged();
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

        private async Task GetCartsByCorporateId(Guid id)
        {
            _carts = await CartRepository.GetCartsByCorporateIdAsync(id);
            _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();
            await UpdateReport(id);
        }

        private async Task GetCartsByStoreId(Guid id)
        {
            _carts = await CartRepository.GetCartsByStoreIdAsync(id);  
            _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();   
            await UpdateStoreReport(id);
        }

        private async Task UpdateReport(Guid id)
        {
            _report = await CorporateRepository.GetCorporateReportAsync(id);
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
