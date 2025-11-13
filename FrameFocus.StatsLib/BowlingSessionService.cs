using ErrorLogging;
using FrameFocus.StatsLib.DTOs;
using FrameFocus.StatsLib.Interfaces;
using System.Text.Json;

namespace FrameFocus.StatsLib;

public class BowlingSessionService: IBowlingSessionService {
    private readonly string _filePath;
    private readonly List<SessionStats> _sessions = [];

    public BowlingSessionService (string filePath = "sessions.json"){
        _filePath = filePath;

        if (File.Exists(filePath)){
            try {
                string json = File.ReadAllText(_filePath);
                _sessions = JsonSerializer.Deserialize<List<SessionStats>>(json) ?? [];
            } catch {
                _sessions = [];
            }
        } else {
            _sessions = [];
        }
    }

    public SessionStats CalculateSessionStats (float gameCount, float totalScore, float totalStrikes, float totalSpares, float totalOpenFrames){
        return new SessionStats {
            Id = (_sessions.Count + 1),
            SessionDate = DateTime.Now,
            GameCount = gameCount,
            TotalScore = totalScore,
            TotalStrikes = totalStrikes,
            TotalSpares = totalSpares,
            TotalOpenFrames = totalOpenFrames
        };
    }

    public void SaveSession (SessionStats session){
        _sessions.Add(session);
        SaveToFile();
    }

    private void SaveToFile (){
        try {
            string json = JsonSerializer.Serialize(_sessions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to save the file.");
        }
    }

    public IEnumerable<SessionStats> GetAllSessions () => _sessions;

    public SessionStats GetMostRecentSession() => _sessions.OrderByDescending(s => s.SessionDate).FirstOrDefault();
}