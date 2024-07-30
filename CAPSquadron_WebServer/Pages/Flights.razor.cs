using CAPSquadron_WebServer.Services;

namespace CAPSquadron_WebServer.Pages
{
    public partial class Flights
    {
        private IEnumerable<FlightDto> flights = [];
        private List<FlightMembership> flightMemberships = [];
        private IEnumerable<MemberAttributesDto> memberAttributes = [];

        protected override async Task OnInitializedAsync()
        {
            flights = await FlightService.GetFlightsAsync();
            flightMemberships = new List<FlightMembership>();
            memberAttributes = await MemberAttributeService.GetMemberAttributesAsync();

            foreach (var flight in flights)
            {
                var (flightCommander, flightSergeants, members) = await GetFlightMembershipAsync(flight);
                flightMemberships.Add(new FlightMembership
                {
                    FlightId = flight.Id,
                    FlightCommander = flightCommander,
                    FlightSergeants = flightSergeants.ToList(),
                    Members = members.ToList()
                });
            }
        }

        protected async Task<(Member? flightCommander, IEnumerable<Member> flightSergeants, IEnumerable<Member> members)> GetFlightMembershipAsync(FlightDto flight)
        {
            var memberCapIds = flight.FlightSergeantIds.Concat(flight.MemberIds).ToList();
            if (flight.FlightCommanderId.HasValue)
            {
                memberCapIds.Add(flight.FlightCommanderId.Value);
            }

            var members = await MemberService.GetAsync(memberCapIds);
            var flightCommander = members.FirstOrDefault(m => m.Capid == flight.FlightCommanderId);
            var flightSergeants = members.Where(m => flight.FlightSergeantIds.Contains(m.Capid));
            return (flightCommander, flightSergeants, members.Where(m => flight.MemberIds.Contains(m.Capid)));
        }

        private void NavigateToFlight(int flightId)
        {
            Navigation.NavigateTo($"/flights/{flightId}");
        }
        private void NavigateToCadet(int capid)
        {
            Navigation.NavigateTo($"/cadet-report/{capid}");
        }

        private class FlightMembership
        {
            public int FlightId { get; set; }
            public Member? FlightCommander { get; set; }
            public List<Member> FlightSergeants { get; set; } = new();
            public List<Member> Members { get; set; } = new();
        }
    }
}