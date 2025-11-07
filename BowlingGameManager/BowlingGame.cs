using BowlingGameManager.DTOs;
using ErrorLogging;

namespace BowlingGameManager;

public class BowlingGame {
    public List<Frame> Frames { get; private set; }
    public List<float> FrameScores { get; private set; }

    private int _currentFrameIndex = 0;

    public BowlingGame (){
        Frames = [];
        FrameScores = [];
        InitialiseFrames();
    }

    private void InitialiseFrames (){
        int frameCount = 10;

        for (int i = 0; i < frameCount; i++){
            Frames.Add(new Frame(0, 0));
        }
    }

    public void RecordShot (float value){
        if (_currentFrameIndex >= 10){
            return;
        }

        var frame = Frames[_currentFrameIndex];

        if (frame.FirstShot == null){
            frame.FirstShot = value;

            if (frame.IsStrike && _currentFrameIndex < 9){
                _currentFrameIndex++;
            }
            return;
        }

        if (frame.SecondShot == null){
            frame.SecondShot = value;

            if (_currentFrameIndex < 9){
                _currentFrameIndex++;
            }
            return;
        }

        if (_currentFrameIndex == 9 && (frame.IsStrike || frame.IsSpare)){
            frame.BonusShot = value;
            _currentFrameIndex++;
        }
    }

    public float TotalScore (){
        float score = 0;
        int tenthFrameIndex = 9;
        int zeroValue = 0;

        try {
            FrameScores.Clear();

            for (int i = zeroValue; i < tenthFrameIndex; i++){
                var frame = Frames[i];

                score += frame.FrameScore;

                if (frame.IsStrike){
                    var nextFrame = Frames[i + 1];

                    score += nextFrame.FirstShot ?? 0;

                    if (nextFrame.IsStrike && i < tenthFrameIndex - 1){
                        score += Frames[i + 2].FirstShot ?? 0;
                    } else {
                        score += nextFrame.SecondShot ?? 0;
                    }
                } else if (frame.IsSpare){
                    score += Frames[i + 1].FirstShot ?? 0;
                }

                FrameScores.Add(score);
                LoggingManager.Instance.LogInformation($"Frame {i + 1} score: {score}");
            }

            var lastFrame = Frames[tenthFrameIndex];
            score += lastFrame.FrameScore;

            FrameScores.Add(score);
            LoggingManager.Instance.LogInformation($"Final Score: {score}");

            return score;
        } catch (IndexOutOfRangeException ex){
            LoggingManager.Instance.LogError(ex, "Failed to calculate the score an Index was out of range.");
            return -1;
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Encountered a problem when attempting to calculate the score.");
            return -1;
        }
    }
}