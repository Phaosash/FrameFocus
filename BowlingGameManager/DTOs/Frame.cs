namespace BowlingGameManager.DTOs;

public class Frame {
    private const float MaxPins = 10;
    private const float MinPins = 0;

    public float? FirstShot { get; set; }
    public float? SecondShot { get; set; }
    public float? BonusShot { get; set; }
    public bool IsStrike => FirstShot == MaxPins;
    public bool IsSpare => FirstShot.HasValue && SecondShot.HasValue && !IsStrike && (FirstShot.Value + SecondShot.Value == MaxPins);

    public bool IsValidShot (float count){
        return count >= MinPins && count <= MaxPins;
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

    public Frame (float? firstShot = null, float? secondShot = null, float? bonusShot = null){
        if (firstShot.HasValue && !IsValidShot(firstShot.Value)){
            throw new ArgumentException("Invalid first shot: must be between 0 and 10.");
        }

        if (secondShot.HasValue && !IsValidShot(secondShot.Value)){
            throw new ArgumentException("Invalid second shot: must be between 0 and 10.");
        }

        if (firstShot.HasValue && secondShot.HasValue && firstShot.Value + secondShot.Value > MaxPins){
            throw new ArgumentException("Invalid frame: total pins in first two shots cannot exceed 10.");
        }

        if (bonusShot.HasValue && bonusShot.Value < 0){
            throw new ArgumentException("Invalid bonus shot: must be >= 0.");
        }

        FirstShot = firstShot;
        SecondShot = secondShot;
        BonusShot = bonusShot;
    }
}