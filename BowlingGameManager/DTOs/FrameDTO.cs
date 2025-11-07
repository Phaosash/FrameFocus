namespace BowlingGameManager.DTOs;

public class FrameDTO {
    public int FrameId { get; set; }
    public float FirstShotValue { get; set; }
    public float SecondShotValue { get; set; }
    public float BonusShotValue { get; set; }
    public float FrameTotal { get; set; }
}