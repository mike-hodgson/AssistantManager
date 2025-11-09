public class Event
{
    public int EventId { get; set; }
    public DateTime EventDate { get; set; }
    public EventType EventType { get; set; } // Enum: Training, Match
    public string Location { get; set; }
    public string Notes { get; set; }

    public ICollection<PlayerAvailability> PlayerAvailability { get; set; }
}