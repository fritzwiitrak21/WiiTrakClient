using Microsoft.AspNetCore.Components;
using WiiTrakClient.DTOs;

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
