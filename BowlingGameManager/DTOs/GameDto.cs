namespace BowlingGameManager.DTOs;

public class GameDto {
    public int GameIndex { get; set; }
    public float Strikes { get; set; }
    public float Spares { get; set; }
    public float OpenFrames { get; set; }
    public float TotalScore { get; set; }
}