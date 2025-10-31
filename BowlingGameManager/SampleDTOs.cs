using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameManager;

internal class SampleDTOs { }

public class DashboardDto {
    public LastSessionDto LastSession { get; set; }
    public OverallStatsDto OverallStats { get; set; }
    public List<TrendPointDto> TrendData { get; set; }
}

public class LastSessionDto {
    public string SessionId { get; set; }
    public DateTime Date { get; set; }
    public double Average { get; set; }
    public double StrikePercentage { get; set; }
    public double SparePercentage { get; set; }
    public double OpenFramePercentage { get; set; }
    public List<ExampleGameDTO> Games { get; set; }
}

public class ExampleGameDTO2 {
    public int GameNumber { get; set; }
    public int Score { get; set; }
    public int Strikes { get; set; }
    public int Spares { get; set; }
    public int OpenFrames { get; set; }
}

public class OverallStatsDto {
    public int TotalGames { get; set; }
    public double Average { get; set; }
    public double StrikePercentage { get; set; }
    public double SparePercentage { get; set; }
    public double OpenFramePercentage { get; set; }
    public int HighGame { get; set; }
    public int HighestSeries { get; set; }
    public double HighestAverage { get; set; }
    public int Handicap { get; set; }
}

public class TrendPointDto {
    public DateTime Date { get; set; }
    public double Average { get; set; }
}
