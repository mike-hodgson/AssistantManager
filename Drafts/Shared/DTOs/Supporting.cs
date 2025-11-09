public class PositionPreference
{
    public int Id { get; set; }
    public string PositionCode { get; set; } // e.g. "CB", "ST"
    public int ComfortLevel { get; set; } // 1–10
    public int PlayerId { get; set; }
    public Player Player { get; set; }
}

public class InjuryRecord
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Notes { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; }
}

public class PlayerAvailability
{
    public int Id { get; set; }
    public AvailabilityStatus Status { get; set; } // Enum: Available, Unavailable, Maybe
    public string Notes { get; set; }
    public int PlayerId { get; set; }
    public int EventId { get; set; }
    public Player Player { get; set; }
    public Event Event { get; set; }
}