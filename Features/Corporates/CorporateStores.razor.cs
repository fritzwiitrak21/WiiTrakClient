using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.HttpRepository.Contracts;

namespace WiiTrakClient.Features.Corporates
{
    public partial class CorporateStores : ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }

        [Inject] ICartHttpRepository CartRepository { get; set; }

        [Inject] IStoreHttpRepository StoreRepository { get; set; }
        
        protected override async Task OnInitializedAsync() 
        {
          

        }
    }
}
