using BowlingGameManager.DTOs;
using ErrorLogging;

namespace BowlingGameManager;

public class CentralisedGameManager {
    public DateTime SessionDate { get; set; } = DateTime.Now;

    private readonly List<BowlingGame> _games = [];
    private int _numberOfGames;

    public IReadOnlyList<BowlingGame> Games => _games.AsReadOnly();

    public void AddGame (){
        _games.Add(new BowlingGame());
    }

    public void SetNumberOfGames (int value){
        _numberOfGames = value;
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

    public GameSessionDto GetSessionTotals (){
        try {
            var sessionDto = new GameSessionDto {
                SessionDate = SessionDate
            };

            foreach (var game in _games){
                var gameDto = CreateNewGameDto(game);

                CreateSessionDto(gameDto, sessionDto);
            }

            return sessionDto;
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Exception encountered when attempting to get the session totals.");
            return new();
        }
    }

    private GameDto CreateNewGameDto (BowlingGame game){
        return new GameDto {
            GameIndex = _games.IndexOf(game),
            GameCount = _numberOfGames,
            Strikes = game.Frames.Count(f => f.IsStrike),
            Spares = game.Frames.Count(f => f.IsSpare),
            OpenFrames = game.Frames.Count(f => !f.IsStrike && !f.IsSpare && (f.FirstShot.HasValue || f.SecondShot.HasValue)),
            TotalScore = game.TotalScore()
        };
    }

    private static void CreateSessionDto (GameDto gameDto, GameSessionDto sessionDto){
        try {
            sessionDto.Games.Add(gameDto);
            sessionDto.GameCount = gameDto.GameCount;
            sessionDto.TotalStrikes += gameDto.Strikes;
            sessionDto.TotalSpares += gameDto.Spares;
            sessionDto.TotalOpenFrames += gameDto.OpenFrames;
            sessionDto.TotalScore += gameDto.TotalScore;
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Excpetion encountered when creating the session dto.");
        }
    }

    public bool AreAllGamesComplete (){
        return _games.All(game => game.IsGameComplete());
    }
}