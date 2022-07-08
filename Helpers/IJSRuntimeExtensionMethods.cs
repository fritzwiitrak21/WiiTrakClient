/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WiiTrakClient.Shared;

namespace WiiTrakClient.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
            => js.InvokeAsync<object>(
                "localStorage.setItem",
                key, content
            );
        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>(
                "localStorage.getItem",
                key
            );
        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
            => js.InvokeAsync<object>(
                "localStorage.removeItem",
                key);
        public static async Task ConsoleLog(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("consoleLog", message);
        }
        public static async ValueTask FocusInput(this IJSRuntime js, string id)
        {
            await js.InvokeVoidAsync("focusInput", id);
        }
        public static async ValueTask ShowAlert(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("showAlert", message);
        }
        public static ValueTask FocusAsync(this IJSRuntime jsRuntime, ElementReference elementReference)
        {
            return jsRuntime.InvokeVoidAsync("BlazorFocusElement", elementReference);
        }
          public static ValueTask GetMap(this IJSRuntime jsRuntime, List<StoreCartMapMarker> markers)
        {
            return jsRuntime.InvokeVoidAsync("GetMap", markers);
        }
    }
}
