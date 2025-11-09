public class FieldZone
{
    public string Label { get; set; }
    public string ZoneGroup { get; set; }
    public PlayerDto Player { get; set; }

    public List<PlayerDto> SuggestedPlayers { get; set; } = new(); // Top 3 candidates
    public bool IsOccupied => Player != null;   // Convenience property
    public bool IsLocked { get; set; } = false;                    // Prevents auto-replacement
}