public class PlayerDto
{
    public int PlayerId { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Nickname { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string RegistrationNumber { get; set; }

    public List<PositionPreferenceDto> PositionPreferences { get; set; }
    public List<InjuryRecordDto> InjuryHistory { get; set; }
}