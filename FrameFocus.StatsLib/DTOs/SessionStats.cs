namespace FrameFocus.StatsLib.DTOs;

public class SessionStats {
    public int Id { get; set; }
    public DateTime SessionDate { get; set; }

    public float GameCount { get; set; }
    public float TotalScore { get; set; }
    public float TotalStrikes { get; set; }
    public float TotalSpares { get; set; }
    public float TotalOpenFrames { get; set; }

    public float AverageScore => (GameCount > 0) ? TotalScore / GameCount : 0;
    public float StrikePercent => (GameCount > 0) ? TotalStrikes / (GameCount * 10) * 100 : 0;
    public float SparePercent => (GameCount > 0) ? TotalSpares / (GameCount * 10) * 100 : 0;
    public float OpenFramePercent => (GameCount > 0) ? TotalOpenFrames / (GameCount * 10) * 100 : 0;
}