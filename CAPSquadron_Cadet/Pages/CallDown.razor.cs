using CAPSquadron_Shared;
using CAPSquadron_Shared.Services;
using CAPSquadron_Shared.Models;

namespace CAPSquadron_Cadet.Pages
{
    public partial class CallDown : CadetSessionComponentBase
    {
        private Member? cadet;
        private PersonalCadetTrackerDto? personalCadetTracker;
        private MeetingInfoDto? meetingInfo;
        private CallDownModel formModel = new();
        private List<RequestItem>? availableRequests;

        protected override async Task OnInitializedAsync()
        {
            // Initialize any necessary data here if needed, otherwise remove Task.Delay(1000)
            await base.OnInitializedAsync();
        }

        protected override async Task OnCapidRetrievedAsync()
        {
            if (!string.IsNullOrEmpty(capid))
            {
                var capId = Convert.ToInt32(capid);
                cadet = await MemberService.GetAsync(capId);
                personalCadetTracker = await CadetTrackerService.GetCadetTrackerByCapidAsync(capId);
                meetingInfo = await meetingService.GetNextMeeting();

                // Initialize available requests
                availableRequests = meetingService
                    .GetAvailableRequestsForCadet(personalCadetTracker)
                    .Select(r => new RequestItem { Name = r, IsSelected = false })
                    .ToList(); // Ensure list is stored as a List<T> to keep it stable
            }
        }

        private async Task HandleValidSubmit()
        {
            if (availableRequests is not null && availableRequests.Count != 0)
            {
                formModel.Requests = availableRequests
                    .Where(r => r.IsSelected)
                    .Select(r => r.Name!)
                    .ToList() ?? [];
            }

            var cdr = new CallDownResponse
            {
                Attending = formModel.Attending,
                CapId = Convert.ToInt32(capid),
                Comments = formModel.Comments,
                MeetingDate = meetingInfo!.Date!.Value,
                Reason = formModel.Reason,
                Requests = formModel.Requests
            };

            await meetingService.RecordCallDownAsync(cdr);
        }
    }
}
