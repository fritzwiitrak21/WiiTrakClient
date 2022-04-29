using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WiiTrakClient;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.HttpRepository;
using WiiTrakClient.Services;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Syncfusion -- register community service license
Syncfusion.Licensing.SyncfusionLicenseProvider
    .RegisterLicense(builder.Configuration["SyncfusionLicense"]);


// Syncfusion
builder.Services.AddSyncfusionBlazor();
builder.Services.AddProtectedBrowserStorage();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("WebAPI",
         client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")));

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("WebAPI"));

builder.Services.AddScoped<IHttpService, HttpService>();

builder.Services.AddScoped<ICartHttpRepository, CartHttpRepository>();
builder.Services.AddScoped<ICompanyHttpRepository, CompanyHttpRepository>();
builder.Services.AddScoped<IDriverHttpRepository, DriverHttpRepository>();
builder.Services.AddScoped<IServiceProviderHttpRepository, ServiceProviderHttpRepository>();
builder.Services.AddScoped<ITechnicianHttpRepository, TechnicianHttpRepository>();
builder.Services.AddScoped<ISystemOwnerHttpRepository, SystemOwnerHttpRepository>();
builder.Services.AddScoped<ITrackingDeviceHttpRepository, TrackingDeviceHttpRepository>();
builder.Services.AddScoped<IStoreHttpRepository, StoreHttpRepository>();
builder.Services.AddScoped<IRepairIssueHttpRepository, RepairIssueHttpRepository>();
builder.Services.AddScoped<ICorporateHttpRepository, CorporateHttpRepository>();
builder.Services.AddScoped<IPicUploadHttpRepository, PicUploadHttpRepository>();
builder.Services.AddScoped<IDeliveryTicketHttpRepository, DeliveryTicketHttpRepository>();
builder.Services.AddScoped<IDeliveryTicketHttpRepository, DeliveryTicketHttpRepository>();
builder.Services.AddScoped<ICartHistoryHttpRepository, CartHistoryHttpRepository>();
builder.Services.AddScoped<IWorkOrderHttpRepository, WorkOrderHttpRepository>();
builder.Services.AddScoped<IlocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ILoginHttpRepository, LoginHttpRepository>();
builder.Services.AddScoped<IEmailHttpRepository, EmailHttpRepository>();
builder.Services.AddSingleton<ExcelService>();
builder.Services.AddScoped<IDriverStoresHttpRepository, DriverStoresHttpRepository>();
builder.Services.AddScoped<INotificationHttpRepository, NotificationHttpRepository>();




builder.Services.AddMudServices();
builder.Services.AddSweetAlert2(options =>
{
    options.Theme = SweetAlertTheme.Default;
});

await builder.Build().RunAsync();
