using CAPSquadron_Shared;
using CAPSquadron_Shared.Services;
using CAPSquadron_Shared.Models;
using BlazorBootstrap;

namespace CAPSquadron_Cadet.Pages
{
    public partial class CallDown : CadetSessionComponentBase
    {
        private Member? Cadet;
        private CallDownModel FormModel = new();
        private CallDownResponse? CallDownResposne = null;

        private int? CapId { get; set; }
        private MeetingInfoDto? MeetingInfo { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnCapidRetrievedAsync()
        {
            if (!string.IsNullOrEmpty(capid))
            {
                CapId = Convert.ToInt32(capid);
                Cadet = await MemberService.GetAsync(CapId.Value);
                MeetingInfo = await meetingService.GetNextMeeting();
                CallDownResposne = (await meetingService.GetCallDownsAsync(MeetingInfo!.Date!.Value, CapId)).FirstOrDefault();
            }
        }

        protected async Task HandleCallDownUpdated()
        {
            CallDownResposne = (await meetingService.GetCallDownsAsync(MeetingInfo!.Date!.Value, CapId)).FirstOrDefault();
        }
    }
}
