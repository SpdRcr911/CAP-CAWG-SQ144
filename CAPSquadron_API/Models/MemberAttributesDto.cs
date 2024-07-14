namespace CAPSquadron_API.Models
{
    public class MemberAttributesDto
    {
        public int CAPID { get; set; }
        public string? Name { get; set; }
        public bool HasWrightBrothersAchievement { get; set; }
        public bool HasGESCertification { get; set; }
        public bool HasCurryAchievement { get; set; }
        public bool HasCawgcapEmail { get; set; }
    }
}
