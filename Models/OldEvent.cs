namespace EventCardCopilotApp.Models
{
    public class OldEvent
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public int MaxParticipants { get; set; } = 0;
        public List<string> SignedUpUserIds { get; set; } = new();

        public int SignedUpCount => SignedUpUserIds.Count;
    }
}