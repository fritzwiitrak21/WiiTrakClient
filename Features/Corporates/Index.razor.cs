/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Corporates
{
    public partial class Index : ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] ICartHttpRepository CartRepository { get; set; }
        [Inject] IStoreHttpRepository StoreRepository { get; set; }
        protected override async Task OnInitializedAsync() 
        {
            // get store/carts by corporate id
            // get corporate report by id

        }
    }
}
