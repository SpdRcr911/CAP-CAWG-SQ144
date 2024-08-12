using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CAPSquadron_Shared;

public class CadetSessionComponent : ComponentBase
{
    [Inject] protected IJSRuntime? JSRuntime { get; set; }
    [Inject] protected NavigationManager? NavigationManager { get; set; }

    protected string? capid;
    private bool _firstRender = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && _firstRender)
        {
            _firstRender = false;

            // Perform JavaScript interop after the component has been rendered
            capid = await JSRuntime!.InvokeAsync<string>("getSessionStorage", "CAPID");

            if (string.IsNullOrEmpty(capid))
            {
                NavigationManager!.NavigateTo("/login");
            }
            else
            {
                StateHasChanged(); // Ensure the UI is updated if capid is found
            }
        }
    }
}
