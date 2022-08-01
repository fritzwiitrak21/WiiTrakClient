/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Cores;
using WiiTrakClient.Shared.Components;
namespace WiiTrakClient.Shared.DeliveryTicket.Components
{
    public partial class DeliveryTicketsList : ComponentBase
    {
        [Inject] IDriverHttpRepository DriverRepository { get; set; }
        [Inject] public IDeliveryTicketHttpRepository DeliveryTicketHttpRepository { get; set; }
        [Inject] public ICartHttpRepository CartHttpRepository { get; set; }
        [Inject] public ICartHistoryHttpRepository CartHistoryHttpRepository { get; set; }
        [Inject] public IStoreHttpRepository StoreHttpRepository { get; set; }
        [Inject] ICartHttpRepository CartRepository { get; set; }
        [Parameter]
        public List<DeliveryTicketDto>? DeliveryTickets { get; set; }
        [Parameter]
        public int RecordCount { get; set; }
        [Parameter]
        public EventCallback DeliveryTicketUpdatedEventCallback { get; set; }
        [Inject]
        IDialogService? DialogService { get; set; }
        DriverDto SelectedDriver = new();
        private bool ListIsLoading = true;
        List<DeliveryTicketDto> Deliverytickets = new();
        List<CartDto> Carts = new();
        List<StoreDto> Stores = new();
        DeliveryTicketUpdateDto EditDeliveryTicket = new();
        Guid DeliveryTicketId = Guid.Empty;
        List<CartDto>? CartsTable { get; set; } = new();
        private string ErrorMessage { get; set; } = "";
        private string SuccessMessage { get; set; } = "";
        protected override void OnParametersSet()
        {
            ListIsLoading = false;
        }
        public async Task OpenUpdateDeliveryTicketDialog(DeliveryTicketDto DeliveryTicket) 
        {
            SelectedDriver = await DriverRepository.GetDriverByIdAsync(DeliveryTicket.DriverId);
            Stores = await StoreHttpRepository.GetStoresByDriverId(DeliveryTicket.DriverId);
            Carts = await CartHttpRepository.GetCartsByDriverIdAsync(DeliveryTicket.DriverId);
            EditDeliveryTicket.StoreId = DeliveryTicket.StoreId;
            EditDeliveryTicket.PicUrl = DeliveryTicket.PicUrl;
            EditDeliveryTicket.DriverId = DeliveryTicket.DriverId;
            EditDeliveryTicket.NumberOfCarts = DeliveryTicket.NumberOfCarts;
            EditDeliveryTicket.ServiceProviderId = DeliveryTicket.ServiceProviderId;
            EditDeliveryTicket.DeliveryTicketNumber = DeliveryTicket.DeliveryTicketNumber;
            var parameters = new DialogParameters();
            parameters.Add("editDeliveryTicket", EditDeliveryTicket);
            parameters.Add("Driver", SelectedDriver);
            parameters.Add("Carts", Carts);
            parameters.Add("Stores", Stores);
            DeliveryTicketId = DeliveryTicket.Id;
            var dialog = DialogService.Show<UpdateDeliveryTicketDialog>("Edit Delivery Ticket", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                // add new delivery ticket to backend
                var DeliveryTicketUpdate = new DeliveryTicketUpdateDto
                {
                    NumberOfCarts = EditDeliveryTicket.NumberOfCarts,
                    PicUrl = EditDeliveryTicket.PicUrl,
                    DeliveredAt = EditDeliveryTicket.DeliveredAt,
                    StoreId = EditDeliveryTicket.StoreId,
                    ServiceProviderId = EditDeliveryTicket.ServiceProviderId,
                    DriverId = EditDeliveryTicket.DriverId,
                    DeliveryTicketNumber = EditDeliveryTicket.DeliveryTicketNumber,
                    IsActive = true,
                    SignOffRequired = Stores.FirstOrDefault(x => x.Id == EditDeliveryTicket.StoreId).IsSignatureRequired
                };
                try
                {
                    await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(DeliveryTicketId, DeliveryTicketUpdate);
                }
                catch
                {
                    //Exception
                }
            }
            await RefreshDeliveryTicket();
        }
        public async Task OpenDeliveryTicketDialog(DeliveryTicketDto DeliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(DeliveryTicket.StoreId);
            var DeliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(DeliveryTicket.Id);
            CartsTable = await CartRepository.GetCartsByDeliveryTicketIdAsync(DeliveryTicket.Id);
            var SelectedCartList = await CartHistoryHttpRepository.GetCartHistoryByDeliveryTicketIdAsync(DeliveryTicket.Id);
            parameters.Add("SelectedCartList", SelectedCartList);
            parameters.Add("deliveryTicketDto", DeliveryTicket);
            parameters.Add("StoreName", store.StoreNumber + "-" + store.StoreName);
            parameters.Add("deliveryTicketSummary", DeliveryTicketSummary);
            parameters.Add("cartsTable", CartsTable);
            DialogService.Show<DeliveryTicketDetailsDialog>("Delivery Ticket Summary", parameters);
        }
        public async Task GetConfirmation(DeliveryTicketDto DeliveryTicket)
        {
            #region Show Message Dialog
            var parameters = new DialogParameters();
            ErrorMessage = "";
            SuccessMessage = "Are you sure do you want to delete this ticket " + DeliveryTicket.DeliveryTicketNumber + "?";
            parameters.Add("DisplayMessage", SuccessMessage);
            parameters.Add("FromWindow", "deliveryicketlist");
            parameters.Add("IsSuccessNotification",true);
            var dialog = DialogService.Show<ShowMessageDialog>("Confirmation Message", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    EditDeliveryTicket.StoreId = DeliveryTicket.StoreId;
                    EditDeliveryTicket.PicUrl = DeliveryTicket.PicUrl;
                    EditDeliveryTicket.DriverId = DeliveryTicket.DriverId;
                    EditDeliveryTicket.NumberOfCarts = DeliveryTicket.NumberOfCarts;
                    EditDeliveryTicket.ServiceProviderId = DeliveryTicket.ServiceProviderId;
                    EditDeliveryTicket.DeliveryTicketNumber = DeliveryTicket.DeliveryTicketNumber;
                    DeliveryTicketId = DeliveryTicket.Id;
                    var DeliveryTicketUpdate = new DeliveryTicketUpdateDto
                    {
                        NumberOfCarts = EditDeliveryTicket.NumberOfCarts,
                        PicUrl = EditDeliveryTicket.PicUrl,
                        DeliveredAt = EditDeliveryTicket.DeliveredAt,
                        StoreId = EditDeliveryTicket.StoreId,
                        ServiceProviderId = EditDeliveryTicket.ServiceProviderId,
                        DriverId = EditDeliveryTicket.DriverId,
                        DeliveryTicketNumber = EditDeliveryTicket.DeliveryTicketNumber,
                        SignOffRequired = EditDeliveryTicket.SignOffRequired,// _stores.FirstOrDefault(x => x.Id == _editDeliveryTicket.StoreId).IsSignatureRequired,
                        IsActive = false,
                        UpdatedBy = CurrentUser.UserId
                    };
                    await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(DeliveryTicketId, DeliveryTicketUpdate);
                    DeliveryTickets.RemoveAll(x => x.Id == DeliveryTicketId);
                    DeliveryTickets = DeliveryTickets.OrderByDescending(y => y.DeliveryTicketNumber).ToList();
                    StateHasChanged();
                }
                catch
                {
                    //Exception
                }
            }
            await RefreshDeliveryTicket();
            #endregion
        }
        async Task RefreshDeliveryTicket()
        {
            Deliverytickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsById(CurrentUser.UserId, (Role)CurrentUser.UserRoleId, RecordCount);
            if (Deliverytickets is not null)
            {
                DeliveryTickets = Deliverytickets;
            }
            StateHasChanged();
        }
    }    
}
