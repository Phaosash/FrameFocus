using BowlingGameManager.DTOs;
using ErrorLogging;

namespace BowlingGameManager;

public class BowlingGame {
    public List<Frame> Frames { get; private set; }
    public List<float> FrameScores { get; private set; }

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

                    score += nextFrame.FirstShot;

                    if (nextFrame.IsStrike && i < tenthFrameIndex - 1){
                        score += Frames[i + 2].FirstShot;
                    } else {
                        score += nextFrame.SecondShot;
                    }
                } else if (frame.IsSpare){
                    score += Frames[i + 1].FirstShot;
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