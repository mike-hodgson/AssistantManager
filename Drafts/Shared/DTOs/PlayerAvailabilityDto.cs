public class PlayerAvailabilityDto
{
    public int PlayerId { get; set; }
    public int EventId { get; set; }
    public AvailabilityStatus Status { get; set; } // Enum
    public string Notes { get; set; }
}