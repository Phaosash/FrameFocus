using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameManager;

public class CentralisedGameManager {
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

    public (int strikes, int spares, int openFrames) GetGameStats (int gameIndex){
        if(!IsValidGameIndex(gameIndex)){
            return (0, 0, 0);
        }

        var game = _games[gameIndex];

        int strikes = game.Frames.Count(f => f.IsStrike);
        int spares = game.Frames.Count(f => f.IsSpare);
        int openFrames = game.Frames.Count(f => !f.IsStrike && !f.IsSpare && (f.FirstShot.HasValue || f.SecondShot.HasValue));

        return (strikes, spares, openFrames);
    }
}