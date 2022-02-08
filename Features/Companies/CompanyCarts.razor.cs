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
            _stores = await StoreRepository.GetStoresByCompanyId(_selectedCompany.Id);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // await ReadSummary();
        }

        private async Task HandleCompanySelected(CompanyDto company)
        {
            System.Console.WriteLine("company id: " + company.Id);
             _selectedCompany = company;
            _stores = await StoreRepository.GetStoresByCompanyId(_selectedCompany.Id);
            await GetCartsByCompanyId(company.Id);
            await UpdateReport(company.Id);
            StateHasChanged();
        }

        private async Task HandleStoreSelected(StoreDto store)
        {
            Console.WriteLine("HandleStoreSelected" + store.StoreName);

            System.Console.WriteLine(store.Id);
            await GetCartsByStoreId(store.Id);
            _selectedStore = store;
            await UpdateReport(_selectedCompany.Id);
            StateHasChanged();
        }

        private async Task GetCartsByCompanyId(Guid id)
        {
            _carts = await CartRepository.GetCartsByCompanyIdAsync(id);
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
            _report = await CompanyRepository.GetCompanyReportAsync(id);
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
