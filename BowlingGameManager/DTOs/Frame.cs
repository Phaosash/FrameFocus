namespace BowlingGameManager.DTOs;

internal class Frame {
    private readonly float frameScoreTarget = 10;
    private readonly float lowestValue = 0;
    private readonly float tenthFrameIndex = 9;

    public float FirstShot { get; set; }
    public float SecondShot { get; set; }
    public float BonusShot { get; set; }
    public bool IsStrike => FirstShot == frameScoreTarget;
    public bool IsSpare => !IsStrike && (FirstShot + SecondShot == frameScoreTarget);

    public bool IsValidShot (float count){
        return count >= lowestValue && count <= frameScoreTarget;
    }

    public bool CanTakeSecondShot (){
        return !IsStrike && (FirstShot + SecondShot) <= frameScoreTarget;
    }

    public bool CanTakeBonusShot (int frameIndex){
        return frameIndex == tenthFrameIndex && (IsStrike || IsSpare);
    }

    public float FrameScore => FirstShot + SecondShot + (IsStrike || IsSpare ? BonusShot : lowestValue);

    public Frame (float firstValue, float secondValue, float bonusValue = 0){
        if (!IsValidShot(firstValue)){
            throw new ArgumentException("Invalid roll: First roll exceeds valid pin range.");
        }

        if (secondValue < 0 || (firstValue < 10 && firstValue + secondValue > 10)){
            throw new ArgumentException("Invalid roll: Total pins in a frame can't exceed 10.");
        }

        if (bonusValue < 0 || (firstValue + secondValue < 10 && bonusValue != 0)){
            throw new ArgumentException("Invalid third roll: Only allowed in the 10th frame on strike or spare.");
        }
    }
}