using CAPSquadron_Shared.Services;
using Microsoft.AspNetCore.Components;

namespace CAPSquadron_RazorSharedLibrary
{
    public partial class CalldownSummaryComponent
    {
        [Parameter]
        public DateOnly? MeetingDate { get; set; }
        private IEnumerable<Member>? MembersWithRequests { get; set; }
        private IEnumerable<CallDownResponse>? CallDownResponses { get; set; }
        private IEnumerable<FlightDto> flights = [];

        private (int yesCount, int noCount, int nrCount) counts;

        protected override async Task OnParametersSetAsync()
        {
            if (MeetingDate is not null)
            {
                CallDownResponses = await meetingService.GetCallDownsAsync(MeetingDate.Value);

                flights = (await FlightService.GetFlightsAsync())
                .Where(flt => flt.Name.EndsWith("Flight"))
                .OrderBy(flt => flt.Name);

                var yesCount = CallDownResponses.Count(cd => cd.Attending == true);
                var noCount = CallDownResponses.Count(cd => cd.Attending == false);
                var nrCount = @flights.SelectMany(f => f.MemberIds).Count() - yesCount - noCount;
                counts = (yesCount, noCount, nrCount);

                var capidsWithRequests = CallDownResponses.Where(cr => cr.Attending == true && cr.Requests.Any()).Select(r => r.CapId);

                MembersWithRequests = await MemberService.GetAsync(capidsWithRequests);
            }
        }
    }
}
