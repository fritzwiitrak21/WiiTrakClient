using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WiiTrakClient;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.HttpRepository;
using WiiTrakClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

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


builder.Services.AddMudServices();
builder.Services.AddSweetAlert2(options =>
{
    options.Theme = SweetAlertTheme.Default;
});

await builder.Build().RunAsync();
