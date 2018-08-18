using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Bleatingsheep.Osu.ApiV2.Models
{
    public class Beatmaps
    {
        [JsonProperty(PropertyName = "id")] public long Id { get; set; }
        [JsonProperty(PropertyName = "beatmapset_id")] public long BeatmapsetId { get; set; }
        [JsonProperty(PropertyName = "mode")] public string Mode { get; set; }
        [JsonProperty(PropertyName = "mode_int")] public int ModeInt { get; set; }
        [JsonProperty(PropertyName = "convert")] public object Convert { get; set; } // TODO: what's this
        [JsonProperty(PropertyName = "difficulty_rating")] public double DifficultyRating { get; set; }
        [JsonProperty(PropertyName = "version")] public string Version { get; set; }
        [JsonProperty(PropertyName = "total_length")] public int TotalLength { get; set; }
        [JsonProperty(PropertyName = "cs")] public double Cs { get; set; }
        [JsonProperty(PropertyName = "drain")] public double Drain { get; set; }
        [JsonProperty(PropertyName = "accuracy")] public double Accuracy { get; set; }
        [JsonProperty(PropertyName = "ar")] public double Ar { get; set; }
        [JsonProperty(PropertyName = "playcount")] public int PlayCount { get; set; }
        [JsonProperty(PropertyName = "passcount")] public int PassCount { get; set; }
        [JsonProperty(PropertyName = "count_circles")] public int CountCircles { get; set; }
        [JsonProperty(PropertyName = "count_sliders")] public int CountSliders { get; set; }
        [JsonProperty(PropertyName = "count_spinners")] public int CountSpinners { get; set; }
        [JsonProperty(PropertyName = "count_total")] public int CountTotal { get; set; }
        [JsonProperty(PropertyName = "last_updated")] public DateTime LastUpdated { get; set; }
        [JsonProperty(PropertyName = "ranked")] public int Ranked { get; set; }
        [JsonProperty(PropertyName = "status")] public string Status { get; set; }
        [JsonProperty(PropertyName = "url")] public string Url { get; set; }
        [JsonProperty(PropertyName = "deleted_at")] public DateTime? DeletedAt { get; set; }
    }
}
