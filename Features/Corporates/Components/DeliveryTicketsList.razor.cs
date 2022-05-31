using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
using WiiTrakClient.Features.Corporates;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Corporates.Components
{
    public partial class DeliveryTicketsList : ComponentBase
    {
        [Inject] IDriverHttpRepository DriverRepository { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }
        [Inject] public ICartHttpRepository CartHttpRepository { get; set; }

        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
        [Inject] ICartHttpRepository CartRepository { get; set; }
        [Parameter]
        public List<DeliveryTicketDto>? DeliveryTickets { get; set; }
        

        [Parameter]
        public EventCallback DeliveryTicketUpdatedEventCallback { get; set; }

        [Inject]
        IDialogService? DialogService { get; set; }

        List<CartDto>? cartsTable { get; set; } = new();
        private bool _listIsLoading = true;
      
      
      
        DeliveryTicketUpdateDto _editDeliveryTicket = new();
        Guid deliveryTicketId = Guid.Empty;
      
       
        protected override void OnParametersSet()
        {
            _listIsLoading = false;
        }

      

        public async Task OpenDeliveryTicketDialog(DeliveryTicketDto deliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(deliveryTicket.StoreId);
            var deliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(deliveryTicket.Id);
            cartsTable = await CartRepository.GetCartsByDeliveryTicketIdAsync(deliveryTicket.Id);
            parameters.Add("deliveryTicketDto", deliveryTicket);
            parameters.Add("StoreName", store.StoreNumber + "-" + store.StoreName);
            parameters.Add("deliveryTicketSummary", deliveryTicketSummary);
            parameters.Add("cartsTable", cartsTable);
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<DeliveryTicketDetailsDialog>("Delivery Ticket Summary", parameters);
        }

       

       
    }    
}
