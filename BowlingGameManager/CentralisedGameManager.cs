using BowlingGameManager.DTOs;

namespace BowlingGameManager;

public class CentralisedGameManager {
    public DateTime SessionDate { get; set; } = DateTime.Now;

    private readonly List<BowlingGame> _games = [];

    public IReadOnlyList<BowlingGame> Games => _games.AsReadOnly();

    public void AddGame (){
        _games.Add(new BowlingGame());
    }

    public void RemoveGame (){
        if (_games.Count > 0){
            _games.RemoveAt(_games.Count - 1);
        }
    }

    public void AddShotToGame (int gameIndex, float value){
        if (IsValidGameIndex(gameIndex)){
            _games[gameIndex].AddValueToScore(value);
        }
    }

    private bool IsValidGameIndex(int index) => index >= 0 && index < _games.Count;

    public void RemoveLastShotFromGame (int gameIndex){
        if (IsValidGameIndex(gameIndex)){
            _games[gameIndex].RemoveLastValue();
        }
    }

    public (float strikes, float spares, float openFrames) GetGameStats (int gameIndex){
        if(!IsValidGameIndex(gameIndex)){
            return (0, 0, 0);
        }

        var game = _games[gameIndex];

        float strikes = game.Frames.Count(f => f.IsStrike);
        float spares = game.Frames.Count(f => f.IsSpare);
        float openFrames = game.Frames.Count(f => !f.IsStrike && !f.IsSpare && (f.FirstShot.HasValue || f.SecondShot.HasValue));

        return (strikes, spares, openFrames);
    }

    public GameSessionDto GetSessionTotals (){
        var sessionDto = new GameSessionDto {
            SessionDate = SessionDate
        };

        foreach (var game in _games){
            var gameDto = new GameDto {
                GameIndex = _games.IndexOf(game),
                GameCount = _games.Count,
                Strikes = game.Frames.Count(f => f.IsStrike),
                Spares = game.Frames.Count(f => f.IsSpare),
                OpenFrames = game.Frames.Count(f => !f.IsStrike && !f.IsSpare && (f.FirstShot.HasValue || f.SecondShot.HasValue)),
                TotalScore = game.TotalScore()
            };

            sessionDto.Games.Add(gameDto);

            sessionDto.TotalStrikes += gameDto.Strikes;
            sessionDto.TotalSpares += gameDto.Spares;
            sessionDto.TotalOpenFrames += gameDto.OpenFrames;
            sessionDto.TotalScore += gameDto.TotalScore;
        }

            return sessionDto;
    }

    public bool AreAllGamesComplete (){
        return _games.All(game => game.IsGameComplete());
    }
}