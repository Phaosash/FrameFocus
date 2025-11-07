namespace BowlingGameManager.DTOs;

public class Frame {
    private readonly float frameScoreTarget = 10;
    private readonly float lowestValue = 0;

    public float? FirstShot { get; set; }
    public float? SecondShot { get; set; }
    public float? BonusShot { get; set; }
    public bool IsStrike => FirstShot == frameScoreTarget;
    public bool IsSpare => FirstShot.HasValue && SecondShot.HasValue && !IsStrike && (FirstShot.Value + SecondShot.Value == frameScoreTarget);

    public bool IsValidShot (float count){
        return count >= lowestValue && count <= frameScoreTarget;
    }

    public float FrameScore {
        get {
            float score = (FirstShot ?? 0) + (SecondShot ?? 0);

            if ((IsStrike || IsSpare) && BonusShot.HasValue){
                score += BonusShot.Value;
            }
            return score;
        }
    }

    public Frame (float? firstValue = null, float? secondValue = null, float? bonusValue = null){
        if (firstValue.HasValue && !IsValidShot(firstValue.Value)){
            throw new ArgumentException("Invalid roll: First roll exceeds valid pin range.");
        }

        if (secondValue.HasValue && (secondValue.Value < 0 || (firstValue.HasValue && firstValue.Value < 10 && firstValue.Value + secondValue.Value > 10))){
            throw new ArgumentException("Invalid roll: Total pins in a frame can't exceed 10.");
        }

        if (bonusValue.HasValue && ((firstValue.GetValueOrDefault() + secondValue.GetValueOrDefault() < 10) || bonusValue.Value < 0)){
            throw new ArgumentException("Invalid third roll: Only allowed in the 10th frame on strike or spare.");
        }

        FirstShot = null;
        SecondShot = null;
        BonusShot = null;
    }
}