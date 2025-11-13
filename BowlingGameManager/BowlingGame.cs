using BowlingGameManager.DTOs;
using ErrorLogging;

namespace BowlingGameManager;

public class BowlingGame {
    public List<Frame> Frames { get; private set; }
    public List<float> FrameScores { get; private set; }

    public int CurrentFrameIndex { get; private set; } = 0;

    public BowlingGame (){
        Frames = [];
        FrameScores = [];
        InitialiseFrames();
    }

    private void InitialiseFrames (){
        int frameCount = 10;

        for (int i = 0; i < frameCount; i++){
            Frames.Add(new Frame(null, null));
        }
    }

    public void AddValueToScore (float value){
        RecordShot(value);
    }

    public void RecordShot (float value){
        if (CurrentFrameIndex >= 10){
            LoggingManager.Instance.LogWarning($"The current frame index was invalid. Index value: {CurrentFrameIndex}");
            return;
        }

        var frame = Frames[CurrentFrameIndex];

        if (CurrentFrameIndex < 9){
            if (frame.FirstShot == null){
                frame.FirstShot = value;

                if (frame.IsStrike){
                    CurrentFrameIndex++;
                }
            }
            else if (frame.SecondShot == null){
                frame.SecondShot = value;

                CurrentFrameIndex++;
            }
        } else {
            if (frame.FirstShot == null){
                frame.FirstShot = value;
            } else if (frame.SecondShot == null){
                frame.SecondShot = value;

                if (!(frame.IsStrike || frame.IsSpare)){
                    CurrentFrameIndex++;
                }
            } else if ((frame.IsStrike || frame.IsSpare) && frame.BonusShot == null){
                frame.BonusShot = value;
                CurrentFrameIndex++;
            }
        }
    }

    public float TotalScore (){
        float score = 0;
        FrameScores.Clear();

        for (int i = 0; i < 10; i++){
            var frame = Frames[i];

            float frameScore = frame.FrameScore;

            if (i < 9){
                if (frame.IsStrike){
                    frameScore += GetNextTwoShots(i);
                } else if (frame.IsSpare){
                    frameScore += GetNextShot(i);
                }
            }

            score += frameScore;
            FrameScores.Add(score);
        }

        return score;
    }

    private float GetNextShot (int frameIndex){
        if (frameIndex + 1 >= Frames.Count){
            return 0;
        }

        var nextFrame = Frames[frameIndex + 1];

        return nextFrame.FirstShot ?? 0;
    }

    private float GetNextTwoShots (int frameIndex){
        float first = 0;
        float second = 0;

        if (frameIndex + 1 < Frames.Count){
            var nextFrame = Frames[frameIndex + 1];
            first = nextFrame.FirstShot ?? 0;

            if (nextFrame.IsStrike && frameIndex + 2 < Frames.Count){
                second = Frames[frameIndex + 2].FirstShot ?? 0;
            } else {
                second = nextFrame.SecondShot ?? 0;
            }
        }

        return first + second;
    }

    public void RemoveLastValue (){
        if (CurrentFrameIndex == 0 && Frames[0].FirstShot == null){
            return;
        }

        int frameIndex = Math.Min(CurrentFrameIndex, 9);
        var frame = Frames[frameIndex];

        if (frameIndex == 9){
            if (frame.BonusShot.HasValue){
                frame.BonusShot = null;
                CurrentFrameIndex = 9;
                return;
            }

            if (frame.SecondShot.HasValue){
                frame.SecondShot = null;
                CurrentFrameIndex = 9;
                return;
            }

            if (frame.FirstShot.HasValue){
                frame.FirstShot = null;
                CurrentFrameIndex = 9;
                return;
            }
        } else {
            if (frame.SecondShot.HasValue){
                frame.SecondShot = null;
                CurrentFrameIndex = frameIndex;
                return;
            }

            if (frame.FirstShot.HasValue){
                frame.FirstShot = null;
                CurrentFrameIndex = frameIndex;
                return;
            } else if (frameIndex > 0){
                frameIndex--;
                frame = Frames[frameIndex];

                if (frame.SecondShot.HasValue){
                    frame.SecondShot = null;
                    CurrentFrameIndex = frameIndex;
                } else if (frame.FirstShot.HasValue){
                    frame.FirstShot = null;
                    CurrentFrameIndex = frameIndex;
                }
            }
        }
    }

    public bool IsGameComplete (){
        return CurrentFrameIndex >= 10;
    }
}