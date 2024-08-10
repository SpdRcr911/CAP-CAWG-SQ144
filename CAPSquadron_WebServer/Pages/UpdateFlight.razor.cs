using CAPSquadron_WebServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CAPSquadron_WebServer.Pages;

public partial class UpdateFlight
{
    [Parameter]
    public int FlightId { get; set; }

    private FlightDto? flight;
    private List<Member> members = [];
    private List<Member> unassignedCadet = [];
    private Member? flightCommander;
    private IEnumerable<Member>? flightSergeants;

    private List<int> selectedCadetIds = [];
    private List<string> addedCadetNames = [];
    private bool showAlert = false;

    protected override async Task OnInitializedAsync()
    {
        flight = await FlightService.GetFlightByIdAsync(FlightId);

        members = (await MemberService.GetAsync(flight.MemberIds)).ToList();
        flightCommander = await MemberService.GetAsync(flight.FlightCommanderId!.Value);
        flightSergeants = await MemberService.GetAsync(flight.FlightSergeantIds);

    }
    protected async Task RemoveCadetFromFlight(int capId)
    {
        members!.Remove(members.FirstOrDefault(m => m.Capid == capId)!);
        flight!.MemberIds.Remove(capId);

        await FlightService.UpdateFlight(flight.Id, flight);
    }
    protected async Task GetUnassignedCadets()
    {
        // Clear any previous selections
        selectedCadetIds.Clear();

        // Retrieve the updated list of unassigned cadets
        unassignedCadet = (await FlightService.GetUnassignedCadetsAsync()).ToList();
    }
    private async Task AddCadets()
    {
        addedCadetNames.Clear();

        foreach (var member in await MemberService.GetAsync(selectedCadetIds))
        {
            if (!members!.Any(m => m.Capid == member.Capid))
            {
                members!.Add(member);
                flight!.MemberIds.Add(member.Capid);
                addedCadetNames.Add(member.FullName);
            }
        }

        await FlightService.UpdateFlight(flight!.Id, flight);

        // Show the alert
        showAlert = true;

        // Close the modal after saving
        await JS.InvokeVoidAsync("closeModal", "addCadet");

        // Clear the selected IDs after updating and closing the modal
        selectedCadetIds.Clear();
        unassignedCadet!.Clear();

    }
    private void UpdateCadetList(int capId, bool isChecked)
    {
        if (isChecked)
        {
            if (!selectedCadetIds.Contains(capId))
            {
                selectedCadetIds.Add(capId);
            }
        }
        else
        {
            if (selectedCadetIds.Contains(capId))
            {
                selectedCadetIds.Remove(capId);
            }
        }
    }
}