using CAPSquadron_Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CAPSquadron_WebServer.Pages;

public partial class CallDown
{
    private IEnumerable<DateOnly> callDownDates = [];
    private DateOnly? SelectedCallDownDate { get; set; } = null;
    private IEnumerable<FlightDto> flights = [];
    private List<FlightMembership> flightMemberships = [];
    private IEnumerable<bool?> attendanceCategory = [true, false, null];

    private Member? selectedCadet { get; set; }
    private CallDownResponse? cadetsCallDownResponse { get; set; }

    protected override async Task OnInitializedAsync()
    {
        callDownDates = await meetingService.GetCallDownDatesAsync();
    }

    private async Task OnDateChanged(ChangeEventArgs e)
    {
        if (DateOnly.TryParse(e.Value.ToString(), out var selectedDate))
        {
            SelectedCallDownDate = selectedDate;
            flights = (await FlightService.GetFlightsAsync())
                .Where(flt => flt.Name.EndsWith("Flight"))
                .OrderBy(flt => flt.Name);

            var callDownResponse = await meetingService.GetCallDownsAsync(selectedDate);

            flightMemberships.Clear();
            foreach (var flight in flights)
            {
                var members = await GetFlightMembershipAsync(flight);

                flightMemberships.Add(new FlightMembership
                {
                    FlightId = flight.Id,
                    FlightName = flight.Name,
                    Members = members,
                    CallDown = (from m in members
                                let myAttendance = callDownResponse.FirstOrDefault(cd => cd.CapId == m.Capid)
                                select new CallDownResponse
                                {
                                    CapId = m.Capid,
                                    Attending = myAttendance?.Attending ?? null,
                                    Reason = myAttendance?.Reason,
                                    Comments = myAttendance?.Comments,
                                    Requests = myAttendance?.Requests?.ToList() ?? new List<string>(),
                                    MeetingDate = selectedDate
                                }).ToList()
                });
            }
            StateHasChanged();
        }
    }

    private void ShowModal(Member member, CallDownResponse? callDownResponse)
    {
        selectedCadet = member;
        cadetsCallDownResponse = callDownResponse;
    }

    protected async Task<IEnumerable<Member>> GetFlightMembershipAsync(FlightDto flight)
    {
        var memberCapIds = flight.FlightSergeantIds.Concat(flight.MemberIds).ToList();
        if (flight.FlightCommanderId.HasValue)
        {
            memberCapIds.Add(flight.FlightCommanderId.Value);
        }

        return await MemberService.GetAsync(memberCapIds);
    }

    public class FlightMembership
    {
        public int FlightId { get; set; }
        public string FlightName { get; set; } = string.Empty;
        public IEnumerable<Member> Members { get; set; } = Enumerable.Empty<Member>();
        public List<CallDownResponse> CallDown { get; set; } = new List<CallDownResponse>();
    }
    private async Task HandleCallDownUpdated(CallDownResponse updatedResponse)
    {
        // Update the flight membership call downs with the updated response
        var flightMembership = flightMemberships.FirstOrDefault(fm => fm.Members.Any(m => m.Capid == updatedResponse.CapId));
        if (flightMembership != null)
        {
            var callDown = flightMembership.CallDown.FirstOrDefault(cd => cd.CapId == updatedResponse.CapId);
            if (callDown != null)
            {
                flightMembership.CallDown.Remove(callDown);
                flightMembership.CallDown.Add(updatedResponse);
            }
        }
        await JS.InvokeVoidAsync("closeModal", "updateCallDown");

        selectedCadet = null;
        cadetsCallDownResponse = null;

        StateHasChanged();
    }
}
