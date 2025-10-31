namespace FrameFocus.PageManagers.DTOs;

public class OverallDataDTO {
    public float Average { get; set; }
    public float HighGame { get; set; }
    public float HighSeries { get; set; }
    public float HighAverage { get; set; }
    public float Handicap { get; set; }
    public string StrikePercentage { get; set; } = string.Empty;
    public string SparePercentage { get; set; } = string.Empty;
    public string OpenPercentage { get; set; } = string.Empty;
}