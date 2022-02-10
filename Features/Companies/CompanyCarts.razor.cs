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

namespace WiiTrakClient.Features.Companies
{
    public partial class CompanyCarts : ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] ICartHttpRepository CartRepository { get; set; }
        [Inject] ICompanyHttpRepository CompanyRepository { get; set; }
        [Inject] IStoreHttpRepository StoreRepository { get; set; }
        List<CompanyDto> _companies = new();
        CompanyDto _selectedCompany;
        List<CartDto> _carts = new();
        List<StoreDto> _stores = new();
        List<StoreDto> _mapStores = new();
        StoreDto _selectedStore;
        List<CartDto> _filteredCarts = new();
        List<RepairIssueDto> _repairIssues = new();
        CompanyReportDto _report;
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
            _companies = await CompanyRepository.GetAllCompaniesAsync();
            _selectedCompany = _companies[0];
            await GetCartsByCompanyId(_selectedCompany.Id);
            await UpdateReport(_selectedCompany.Id);
            await GetStoreListByCompanyId();
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // await ReadSummary();
        }

        private async Task HandleCompanySelected(CompanyDto company)
        {
            System.Console.WriteLine("company id: " + company.Id);
             _selectedCompany = company;
            await GetCartsByCompanyId(company.Id);
            await UpdateReport(company.Id);
            await GetStoreListByCompanyId();
            StateHasChanged();
        }

        private async Task HandleStoreSelected(StoreDto store)
        {
            Console.WriteLine("HandleStoreSelected" + store.StoreName);

            System.Console.WriteLine(store.Id);
            if (store.Id == Guid.Empty)
            {
                await GetAllStoreCartsByCompanyId(_selectedCompany.Id);
            }
            else
            {
                await GetCartsByStoreId(store);
            }
            _selectedStore = store;
            await UpdateStoreReport(store.Id);
            StateHasChanged();
        }

        private async Task GetStoreListByCompanyId()
        {
            _stores = new List<StoreDto>();
            var storelist = await StoreRepository.GetStoresByCompanyId(_selectedCompany.Id);
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
        private async Task GetAllStoreCartsByCompanyId(Guid id)
        {
            _carts = await CartRepository.GetCartsByCompanyIdAsync(id);
            _mapStores = new List<StoreDto>();
            _mapStores = await StoreRepository.GetStoresByCompanyId(id);
            _filteredCarts = _carts.Where(x => x.Status == CartStatus.OutsideGeofence).ToList();

            //await UpdateStoreReport(id);
        }
        private async Task GetCartsByCompanyId(Guid id)
        {
            _carts = await CartRepository.GetCartsByCompanyIdAsync(id);
            _mapStores = new List<StoreDto>();
            _mapStores = await StoreRepository.GetStoresByCompanyId(id);
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
            _report = new CompanyReportDto();
            _report = await CompanyRepository.GetCompanyReportAsync(id);
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
                _storeReport = await StoreRepository.GetAllStoreReportByCompanyAsync(_selectedCompany.Id);
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
