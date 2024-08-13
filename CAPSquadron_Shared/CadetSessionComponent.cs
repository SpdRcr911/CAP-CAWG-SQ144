using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CAPSquadron_Shared;

public abstract class CadetSessionComponentBase : ComponentBase
{
    [Inject] protected IJSRuntime? JSRuntime { get; set; }
    [Inject] protected NavigationManager? NavigationManager { get; set; }

    protected string? capid;
    private bool _firstRender = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && _firstRender)
        {
            _firstRender = false;

            // Retrieve CAPID from session storage
            capid = await JSRuntime!.InvokeAsync<string>("getSessionStorage", "CAPID");

            if (string.IsNullOrEmpty(capid))
            {
                NavigationManager!.NavigateTo("/login");
            }
            else
            {
                await OnCapidRetrievedAsync();
                StateHasChanged(); // Refresh UI with loaded data
            }
        }
    }

    // Abstract method to be implemented by derived components
    protected abstract Task OnCapidRetrievedAsync();
}
