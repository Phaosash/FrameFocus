namespace DataManager.DTOs;

internal struct StatisticsData {
    public float Average { get; set; }
    public float StrikePercentage { get; set; }
    public float SparePercentage { get; set; }
    public float OpenFramePercentage { get; set; }
    public float HighGame { get; set; }
    public float HighSeries { get; set; }
    public float HighAverage { get; set; }
    public float GamesBowled { get; set; }
}