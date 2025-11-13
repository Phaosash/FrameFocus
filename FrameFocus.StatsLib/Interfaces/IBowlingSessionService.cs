using FrameFocus.StatsLib.DTOs;

namespace FrameFocus.StatsLib.Interfaces;

public interface IBowlingSessionService {
    SessionStats CalculateSessionStats(float gameCount, float totalScore, float totalStrikes, float totalSpares, float totalOpenFrames);

    void SaveSession(SessionStats session);

    IEnumerable<SessionStats> GetAllSessions();
    SessionStats GetMostRecentSession();
}