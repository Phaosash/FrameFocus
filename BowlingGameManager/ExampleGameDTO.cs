using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BowlingGameManager;

public class ExampleGameDTO {
    [JsonPropertyName("gameNumber")]
    public int GameNumber { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("strikes")]
    public int Strikes { get; set; }

    [JsonPropertyName("spares")]
    public int Spares { get; set; }

    [JsonPropertyName("openFrames")]
    public int OpenFrames { get; set; }
}
