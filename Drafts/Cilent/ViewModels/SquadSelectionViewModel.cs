public class SquadSelectionViewModel
{
    public List<FieldZone> FieldZones { get; private set; } = new();
    public List<PlayerDto> BenchPlayers { get; private set; } = new();
    public PlayerDto? DraggedPlayer { get; private set; }

    private readonly ISquadService _squadService;

    public SquadSelectionViewModel(ISquadService squadService)
    {
        _squadService = squadService;
    }

    public async Task InitializeAsync()
    {
        var availablePlayers = await _squadService.GetAvailablePlayersAsync();
        BenchPlayers = availablePlayers;
        FieldZones = _squadService.GetDefaultFieldZones();
    }

    public void StartDrag(PlayerDto player)
    {
        DraggedPlayer = player;
    }

    public void DropPlayer(FieldZone zone)
    {
        if (DraggedPlayer == null) return;

        zone.Player = DraggedPlayer;
        BenchPlayers.Remove(DraggedPlayer);
        DraggedPlayer = null;
    }

    public async Task AutoSuggestAsync()
    {
        var suggestion = await _squadService.GetSuggestedSquadAsync();
        FieldZones = suggestion.FieldZones;
        BenchPlayers = suggestion.Bench;
    }
}