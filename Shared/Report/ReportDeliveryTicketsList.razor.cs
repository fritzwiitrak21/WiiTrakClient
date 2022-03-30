using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
using WiiTrakClient.Shared.Report;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Shared.Report
{
    public partial class ReportDeliveryTicketsList : ComponentBase
    {
       
        [Parameter]
        public List<DeliveryTicketDto>? _ReportDeliveryTicketsList { get; set; }
       
        private bool listIsLoading = true;
       
        protected override void OnParametersSet()
        {
            listIsLoading = false;
        }
    }    
}
