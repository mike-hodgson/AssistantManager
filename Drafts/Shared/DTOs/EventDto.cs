public class EventDto
{
    public int EventId { get; set; }
    public DateTime EventDate { get; set; }
    public EventType EventType { get; set; } // Enum
    public string Location { get; set; }
    public string Notes { get; set; }

    public List<PlayerAvailabilityDto> PlayerAvailability { get; set; }
}