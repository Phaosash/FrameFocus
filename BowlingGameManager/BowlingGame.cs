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

        //if (frame.FirstShot == null){
        //    frame.FirstShot = value;

        //    if (frame.IsStrike && _currentFrameIndex < 9){

        //        _currentFrameIndex++;
        //    }
        //    return;
        //}

        //if (frame.SecondShot == null){
        //    frame.SecondShot = value;

        //    if (_currentFrameIndex < 9){
        //        _currentFrameIndex++;
        //    }
        //    return;
        //}

        //if (_currentFrameIndex == 9 && (frame.IsStrike || frame.IsSpare)){
        //    frame.BonusShot = value;
        //    _currentFrameIndex++;
        //}
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
        
        /*float score = 0;
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
        }*/
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
}