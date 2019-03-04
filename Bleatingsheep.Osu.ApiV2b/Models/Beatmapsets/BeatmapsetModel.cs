using Newtonsoft.Json;

namespace Bleatingsheep.Osu.ApiV2b.Models
{
    public class BeatmapsetModel
    {
        [JsonProperty("beatmapsets")]
        public Beatmapset[] Beatmapsets { get; set; }

        [JsonProperty("cursor")]
        public Beatmapset Cursor { get; set; }
    }
}
