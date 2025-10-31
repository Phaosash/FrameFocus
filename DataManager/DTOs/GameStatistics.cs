namespace DataManager.DTOs;

internal class GameStatistics {
    public int Id { get; set; }
    public float Score { get; set; }
    public float Games { get; set; }
    public float Strikes { get; set; }
    public float Spares { get; set; }
    public float OpenFrames { get; set; }
}