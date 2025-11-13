namespace BowlingGameManager.DTOs;

public class GameSessionDto {
    public DateTime SessionDate { get; set; }
    public int GameCount { get; set; }
    public float TotalStrikes { get; set; }
    public float TotalSpares { get; set; }
    public float TotalOpenFrames { get; set; }
    public float TotalScore { get; set; }
    public List<GameDto> Games { get; set; } = [];
}