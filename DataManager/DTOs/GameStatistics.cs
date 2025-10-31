namespace DataManager.DTOs;

internal class GameStatistics {
    public float[] Score { get; set; } = [];
    public float Games { get; set; }
    public float Strikes { get; set; }
    public float Spares { get; set; }
    public float OpenFrames { get; set; }
}