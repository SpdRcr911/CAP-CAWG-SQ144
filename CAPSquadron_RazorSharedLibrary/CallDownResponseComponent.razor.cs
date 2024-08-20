using CAPSquadron_Shared.Models;
using CAPSquadron_Shared.Services;
using Microsoft.AspNetCore.Components;

namespace CAPSquadron_RazorSharedLibrary
{
    public partial class CallDownResponseComponent
    {
        [Parameter]
        public Member? cadet { get; set; }
        [Parameter]
        public CallDownResponse? callDownResponse { get; set; }
        [Parameter]
        public EventCallback<CallDownResponse> OnCallDownUpdated { get; set; }

        private CallDownModel formModel = new();
        private List<RequestItem>? availableRequests;
        private MeetingInfoDto? meetingInfo;
        private PersonalCadetTrackerDto? personalCadetTracker;

        protected override async Task OnParametersSetAsync()
        {
            if (cadet is not null && callDownResponse is not null)
            {
                personalCadetTracker = await CadetTrackerService.GetCadetTrackerByCapidAsync(cadet.Capid);
                meetingInfo = await meetingService.GetNextMeeting(callDownResponse.MeetingDate);

                // Initialize available requests
                availableRequests = meetingService.GetAvailableRequestsForCadet(personalCadetTracker)
                    .Select(r => new RequestItem { Name = r, IsSelected = false }).ToList();

                availableRequests.Where(r => callDownResponse.Requests.Contains(r.Name)).ToList().ForEach(r => r.IsSelected = true);

                formModel = new CallDownModel
                {
                    Attending = callDownResponse.Attending ?? false,
                    Comments = callDownResponse.Comments,
                    Reason = callDownResponse.Reason,
                    Requests = callDownResponse.Requests.ToList()
                };
            }
        }

        private async Task HandleValidSubmit()
        {
            if (availableRequests is not null && availableRequests.Count != 0)
            {
                formModel.Requests = availableRequests
                    .Where(r => r.IsSelected)
                    .Select(r => r.Name!)
                    .ToList();
            }

            var cdr = new CallDownResponse
            {
                Attending = formModel.Attending,
                CapId = cadet.Capid,
                Comments = formModel.Comments,
                MeetingDate = meetingInfo!.Date!.Value,
                Reason = formModel.Reason,
                Requests = formModel.Requests
            };

            await meetingService.RecordCallDownAsync(cdr);

            // Invoke the callback to notify the parent component
            await OnCallDownUpdated.InvokeAsync(cdr);
        }
    }
}
