using BowlingGameManager.DTOs;
using ErrorLogging;

namespace BowlingGameManager;

public class BowlingGame {
    public List<Frame> Frames { get; private set; }
    private int _currentFrameIndex = 0;
    private bool _isFinished = false;

    public BowlingGame (){
        Frames = [];
        InitialiseFrames();
    }

    private void InitialiseFrames (){
        int frameCount = 10;

        for (int i = 0; i < frameCount; i++){
            Frames.Add(new Frame(0, 0));
        }
    }

    public void ResetGame (){
        Frames.Clear();
        _currentFrameIndex = 0;

        InitialiseFrames();
    }

    public void Shot (float count){
        float zeroValue = 0;
        float maxCountValue = 10;
        int tenthFrameIndex = 9;
        
        if (_isFinished){
            LoggingManager.Instance.LogInformation("The game is over, no more shots can be made");
            return;
        }

        if (count < zeroValue || count > maxCountValue){
            LoggingManager.Instance.LogWarning($"Invalid shot: Number of pins must be between {zeroValue} and {maxCountValue}.");
            return;
        }

        var currentFrame = Frames[_currentFrameIndex];

        if (currentFrame.FirstShot == zeroValue){
            if (count > maxCountValue){
                LoggingManager.Instance.LogWarning($"Invalid shot: A single shot can't exceed {maxCountValue}.");
                return;
            }
            
            currentFrame.FirstShot = count;

            if (count == maxCountValue && _currentFrameIndex < tenthFrameIndex){
                _currentFrameIndex++;
            }
        } else if (currentFrame.SecondShot == zeroValue){
            if (currentFrame.FirstShot + count > maxCountValue){
                LoggingManager.Instance.LogWarning($"Invalid shot: Total pins in a frame cannot exceed {maxCountValue}.");
                return;
            }

            currentFrame.SecondShot = count;

            if (_currentFrameIndex == tenthFrameIndex && (currentFrame.IsStrike || currentFrame.IsSpare)){
                currentFrame.BonusShot = count;
            } else {
                _currentFrameIndex++;
            }
        } else {
            LoggingManager.Instance.LogWarning("Invalid state: This frame already has two rolls.");
        }

        if (_currentFrameIndex == 10){
            _isFinished = true;
        }
    }

    public float TotalScore (){
        float score = 0;
        int tenthFrameIndex = 9;
        int zeroValue = 0;

        try {
            for (int i = zeroValue; i < tenthFrameIndex; i++){
                var frame = Frames[i];

                score += frame.FrameScore;

                if (frame.IsStrike){
                    var nexFrame = Frames[i + 1];

                    score += nexFrame.FirstShot;

                    if (nexFrame.IsStrike && i < tenthFrameIndex - 1){
                        score += Frames[i + 2].FirstShot;
                    } else {
                        score += nexFrame.SecondShot;
                    }
                } else if (frame.IsSpare){
                    score += Frames[i + 1].FirstShot;
                }

            }

            var lastFrame = Frames[tenthFrameIndex];
            score += lastFrame.FrameScore;

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