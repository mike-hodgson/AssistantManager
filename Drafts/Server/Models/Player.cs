public class Player
{
    public int PlayerId { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Nickname { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string RegistrationNumber { get; set; }

    public ICollection<PositionPreference> PositionPreferences { get; set; }
    public ICollection<InjuryRecord> InjuryHistory { get; set; }
    public ICollection<PlayerAvailability> AvailabilityRecords { get; set; }
}