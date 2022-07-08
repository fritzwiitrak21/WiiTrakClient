/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;

namespace WiiTrakClient.Features.Companies
{
    public partial class CompanyCarts : ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] ICartHttpRepository CartRepository { get; set; }
        [Inject] ICompanyHttpRepository CompanyRepository { get; set; }
        [Inject] IStoreHttpRepository StoreRepository { get; set; }
        List<CompanyDto> _companies = new();
        CompanyDto SelectedCompany;
        List<CartDto> _carts = new();
        List<StoreDto> _stores = new();
        List<StoreDto> _mapStores = new();
        StoreDto SelectedStore;
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

        protected override async Task OnInitializedAsync()
        {
            _companies = await CompanyRepository.GetAllCompaniesAsync();
            SelectedCompany = _companies[0];
            await GetCartsByCompanyId(SelectedCompany.Id);
            await UpdateReport(SelectedCompany.Id);
            await GetStoreListByCompanyId();
            StateHasChanged();
        }
        private async Task HandleCompanySelected(CompanyDto company)
        {
            SelectedCompany = company;
            await GetCartsByCompanyId(company.Id);
            await UpdateReport(company.Id);
            await GetStoreListByCompanyId();
            StateHasChanged();
        }
        private async Task HandleStoreSelected(StoreDto store)
        {
            if (store.Id == Guid.Empty)
            {
                await GetAllStoreCartsByCompanyId(SelectedCompany.Id);
            }
            else
            {
                await GetCartsByStoreId(store);
            }
            SelectedStore = store;
            await UpdateStoreReport(store.Id);
            StateHasChanged();
        }
        private async Task GetStoreListByCompanyId()
        {
            _stores = new List<StoreDto>();
            var storelist = await StoreRepository.GetStoresByCompanyId(SelectedCompany.Id);
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
                SelectedStore = _stores.Where(x => x.Id == Guid.Empty).FirstOrDefault();
            }
            await UpdateStoreReport(SelectedStore.Id);
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
                _storeReport = await StoreRepository.GetAllStoreReportByCompanyAsync(SelectedCompany.Id);
            }
        }
        private void ShowMapView()
        {
            _view = ViewOption.Map;
        }

        private void ShowListView()
        {
            _view = ViewOption.List;
        }
    }
}
