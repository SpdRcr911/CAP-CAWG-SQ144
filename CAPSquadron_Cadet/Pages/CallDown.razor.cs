using CAPSquadron_Shared.Services;

namespace CAPSquadron_Cadet.Pages
{
    public partial class CallDown
    {
        private Member? cadet;
        private PersonalCadetTrackerDto? personalCadetTracker;
        private MeetingInfoDto? meetingInfo;
        private CallDownModel formModel = new();
        private Member? Cadet { get; set; }
        private IEnumerable<RequestItem>? availableRequests;

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(1000);
        }

        private async Task HandleValidSubmit()
        {
            await Task.Delay(1000);
        }

        protected override async Task OnCapidRetrievedAsync()
        {
            if (!string.IsNullOrEmpty(capid))
            {
                var capId = Convert.ToInt32(capid);
                cadet = await MemberService.GetAsync(capId);
                personalCadetTracker = await CadetTrackerService.GetCadetTrackerByCapidAsync(capId);
                meetingInfo = await meetingService.GetNextMeeting();
                availableRequests = meetingService.GetAvailableRequestsForCadet(personalCadetTracker).Select(r => new RequestItem { Name = r });

            }
        }
        public class CallDownModel
        {
            public bool Attending { get; set; } = false;
            public string? Reason { get; set; }
            public List<string> Requests { get; set; } = [];
            public string? Comments { get; set; }
        }

        public class RequestItem
        {
            public string? Name { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}
