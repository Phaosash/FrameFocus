using BowlingGameManager.DTOs;
using ErrorLogging;

namespace BowlingGameManager;

public class BowlingScoreManager {
    public List<FrameDTO> Frames { get; private set; }
    public List<float> FrameScores { get; private set; }
    public List<ShotValuesDTO> Shots { get; private set; }

    public BowlingScoreManager (){
        Frames = [];
        FrameScores = [];
        Shots = [];
    }

    public void ValidateProvidedInput (int frameIndex, char value){
        List<char> validNonNumberCharacters = ['X', '/', '-', 'F'];
        float score;

        try {
            if (value == validNonNumberCharacters[0]){
                score = 10.0f;
            } else if (value == validNonNumberCharacters[1]){
                score = CalculateSpareValue(frameIndex);
            } else if (value == validNonNumberCharacters[2] || value == validNonNumberCharacters[3]){
                score = 0.0f;
            } else {
                score = ConverCharToFloat(value);
            }

            AddValueToFrame(frameIndex, score);
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to validate the provided inputted character.");
        }
    }

    private static float ConverCharToFloat (char value){
        if (float.TryParse(value.ToString(), out float number)){
            return number;
        } else {
            return -1.0f;
        }
    }

    private void AddValueToFrame (int frameIndex, float value){
        var thisFrame = Frames[frameIndex];

        if (DidFirstShotStrike(value)){
            thisFrame.FirstShotValue = value;
            CalculateFrameTotal(thisFrame, thisFrame.FirstShotValue);
        } else {
            thisFrame.SecondShotValue = ValidateSecondShotResult(thisFrame.FirstShotValue, value);
            CalculateFrameTotal(thisFrame, thisFrame.SecondShotValue);
        }
    }

    private static void CalculateFrameTotal (FrameDTO frame, float value){
        frame.FrameTotal += value;
    }

    private static float ValidateSecondShotResult (float firstValue, float secondValue){
        if (firstValue + secondValue > 10){
            return -1.0f;
        }

        if (firstValue + secondValue <= 10){
            return secondValue;
        }

        return -1.0f;
    }

    private static bool DidFirstShotStrike (float value){
        float strikeValue = 10.0f;

        if (value == strikeValue){ 
            return true; 
        } else {
            return false;
        }
    }

    private float CalculateSpareValue (int frameIndex){
        FrameDTO frame = Frames[frameIndex];
        float value = 0.0f;
        float maxFrameCount = 10;

        if (frame != null && frame.FirstShotValue != maxFrameCount){
            try {
                for (int i = (int)frame.FirstShotValue; i < maxFrameCount; i++){
                    value++;
                }

                return value;
            } catch (IndexOutOfRangeException ex){
                LoggingManager.Instance.LogError(ex, "Failed to calculate the spare value.");
                return -1.0f;
            } catch (Exception ex){
                LoggingManager.Instance.LogError(ex, "Failed to calculate the spare value.");
                return -1.0f;
            }
        } else {
            return -1.0f;
        }
    }
}