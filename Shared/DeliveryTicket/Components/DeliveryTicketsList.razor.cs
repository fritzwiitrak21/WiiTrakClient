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
using WiiTrakClient.Shared.DeliveryTicket.Components;

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
        DriverDto selectedDriver = new();
        private bool _listIsLoading = true;
        List<DeliveryTicketDto> _deliveryTickets = new();
        List<CartDto> _carts = new();
        List<StoreDto> _stores = new();
        DeliveryTicketUpdateDto _editDeliveryTicket = new();
        Guid deliveryTicketId = Guid.Empty;
        List<CartDto>? cartsTable { get; set; } = new();
        private string ErrorMessage { get; set; } = "";
        private string SuccessMessage { get; set; } = "";
        protected override void OnParametersSet()
        {
            _listIsLoading = false;
        }
        public async Task OpenUpdateDeliveryTicketDialog(DeliveryTicketDto deliveryTicket) 
        {
            selectedDriver = await DriverRepository.GetDriverByIdAsync(deliveryTicket.DriverId);
            _stores = await StoreHttpRepository.GetStoresByDriverId(deliveryTicket.DriverId);
            _carts = await CartHttpRepository.GetCartsByDriverIdAsync(deliveryTicket.DriverId);
            _editDeliveryTicket.StoreId = deliveryTicket.StoreId;
            _editDeliveryTicket.PicUrl = deliveryTicket.PicUrl;
            _editDeliveryTicket.DriverId = deliveryTicket.DriverId;
            _editDeliveryTicket.NumberOfCarts = deliveryTicket.NumberOfCarts;
            _editDeliveryTicket.ServiceProviderId = deliveryTicket.ServiceProviderId;
            _editDeliveryTicket.DeliveryTicketNumber = deliveryTicket.DeliveryTicketNumber;
            var parameters = new DialogParameters();
            parameters.Add("editDeliveryTicket", _editDeliveryTicket);
            parameters.Add("Driver", selectedDriver);
            parameters.Add("Carts", _carts);
            parameters.Add("Stores", _stores);
            deliveryTicketId = deliveryTicket.Id;
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };
            var dialog = DialogService.Show<UpdateDeliveryTicketDialog>("Edit Delivery Ticket", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                // add new delivery ticket to backend
                var deliveryTicketUpdate = new DeliveryTicketUpdateDto
                {
                    NumberOfCarts = _editDeliveryTicket.NumberOfCarts,
                    PicUrl = _editDeliveryTicket.PicUrl,
                    DeliveredAt = _editDeliveryTicket.DeliveredAt,
                    StoreId = _editDeliveryTicket.StoreId,
                    ServiceProviderId = _editDeliveryTicket.ServiceProviderId,
                    DriverId = _editDeliveryTicket.DriverId,
                    DeliveryTicketNumber = _editDeliveryTicket.DeliveryTicketNumber,
                    IsActive = true,
                     SignOffRequired = _stores.FirstOrDefault(x => x.Id == _editDeliveryTicket.StoreId).IsSignatureRequired
                };
                await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(deliveryTicketId,deliveryTicketUpdate);
                
            }
            await RefreshDeliveryTicket();

        }
        public async Task OpenDeliveryTicketDialog(DeliveryTicketDto deliveryTicket)
        {
            var parameters = new DialogParameters();
            var store = await StoreHttpRepository.GetStoreByIdAsync(deliveryTicket.StoreId);
            var deliveryTicketSummary = await DeliveryTicketHttpRepository.GetDeliveryTicketSummaryAsync(deliveryTicket.Id);
            cartsTable = await CartRepository.GetCartsByDeliveryTicketIdAsync(deliveryTicket.Id);
            var SelectedCartList = await CartHistoryHttpRepository.GetCartHistoryByDeliveryTicketIdAsync(deliveryTicket.Id);
            parameters.Add("SelectedCartList", SelectedCartList);
            parameters.Add("deliveryTicketDto", deliveryTicket);
            parameters.Add("StoreName", store.StoreNumber + "-" + store.StoreName);
            parameters.Add("deliveryTicketSummary", deliveryTicketSummary);
            parameters.Add("cartsTable", cartsTable);
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };
            var dialog = DialogService.Show<DeliveryTicketDetailsDialog>("Delivery Ticket Summary", parameters);
        }
        public async Task GetConfirmation(DeliveryTicketDto deliveryTicket)
        {
            #region Show Message Dialog
            var parameters = new DialogParameters();
            ErrorMessage = "";
            SuccessMessage = "Are you sure do you want to delete this ticket " + deliveryTicket.DeliveryTicketNumber + "?";
            parameters.Add("DisplayMessage", SuccessMessage);
            parameters.Add("FromWindow", "deliveryicketlist");
            parameters.Add("IsSuccessNotification",true);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<ShowMessageDialog>("Confirmation Message", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                try
                {
                    _editDeliveryTicket.StoreId = deliveryTicket.StoreId;
                    _editDeliveryTicket.PicUrl = deliveryTicket.PicUrl;
                    _editDeliveryTicket.DriverId = deliveryTicket.DriverId;
                    _editDeliveryTicket.NumberOfCarts = deliveryTicket.NumberOfCarts;
                    _editDeliveryTicket.ServiceProviderId = deliveryTicket.ServiceProviderId;
                    _editDeliveryTicket.DeliveryTicketNumber = deliveryTicket.DeliveryTicketNumber;
                    deliveryTicketId = deliveryTicket.Id;
                    var deliveryTicketUpdate = new DeliveryTicketUpdateDto
                    {
                        NumberOfCarts = _editDeliveryTicket.NumberOfCarts,
                        PicUrl = _editDeliveryTicket.PicUrl,
                        DeliveredAt = _editDeliveryTicket.DeliveredAt,
                        StoreId = _editDeliveryTicket.StoreId,
                        ServiceProviderId = _editDeliveryTicket.ServiceProviderId,
                        DriverId = _editDeliveryTicket.DriverId,
                        DeliveryTicketNumber = _editDeliveryTicket.DeliveryTicketNumber,
                        SignOffRequired =_editDeliveryTicket.SignOffRequired,// _stores.FirstOrDefault(x => x.Id == _editDeliveryTicket.StoreId).IsSignatureRequired,
                        IsActive = false,
                        UpdatedBy = CurrentUser.UserId
                    };

                    await DeliveryTicketHttpRepository.UpdateDeliveryTicketAsync(deliveryTicketId, deliveryTicketUpdate);
                    DeliveryTickets.RemoveAll(x => x.Id == deliveryTicketId);
                    DeliveryTickets = DeliveryTickets.OrderByDescending(y => y.DeliveryTicketNumber).ToList();
                    StateHasChanged();
                }
                catch (Exception ex)
                {
                    //exception
                }

            }
            await RefreshDeliveryTicket();
            #endregion
        }
        async Task RefreshDeliveryTicket()
        {
            _deliveryTickets = await DeliveryTicketHttpRepository.GetDeliveryTicketsById(CurrentUser.UserId, (Role)CurrentUser.UserRoleId, RecordCount);
            if (_deliveryTickets is not null)
            {
                DeliveryTickets = _deliveryTickets;
            }
            StateHasChanged();
        }
    }    
}
