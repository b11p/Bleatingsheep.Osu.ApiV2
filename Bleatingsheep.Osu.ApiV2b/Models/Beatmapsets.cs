using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Bleatingsheep.Osu.ApiV2.Models
{
    public class Beatmapsets
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "artist")] public string Artist { get; set; }
        [JsonProperty(PropertyName = "play_count")] public int PlayCount { get; set; }
        [JsonProperty(PropertyName = "favourite_count")] public int FavouriteCount { get; set; }
        [JsonProperty(PropertyName = "has_favourited")] public bool HasFavourited { get; set; }
        [JsonProperty(PropertyName = "submitted_date")] public DateTime SubmittedDate { get; set; }
        [JsonProperty(PropertyName = "last_updated")] public DateTime LastUpdated { get; set; }
        [JsonProperty(PropertyName = "ranked_date")] public DateTime RankedDate { get; set; }
        [JsonProperty(PropertyName = "creator")] public string Creator { get; set; }
        [JsonProperty(PropertyName = "user_id")] public long UserId { get; set; }
        [JsonProperty(PropertyName = "bpm")] public double Bpm { get; set; }
        [JsonProperty(PropertyName = "source")] public string Source { get; set; }
        [JsonProperty(PropertyName = "covers")] public BeatmapsetsCovers Covers { get; set; }
        [JsonProperty(PropertyName = "preview_url")] public string PreviewUrl { get; set; }
        [JsonProperty(PropertyName = "tags")] public string Tags { get; set; }
        [JsonProperty(PropertyName = "video")] public bool Video { get; set; }
        [JsonProperty(PropertyName = "storyboard")] public bool Storyboard { get; set; }
        [JsonProperty(PropertyName = "ranked")] public int Ranked { get; set; }
        [JsonProperty(PropertyName = "status")] public string Status { get; set; }
        [JsonProperty(PropertyName = "has_scores")] public bool HasScores { get; set; }
        [JsonProperty(PropertyName = "discussion_enabled")] public bool DiscussionEnabled { get; set; }
        [JsonProperty(PropertyName = "can_be_hyped")] public bool CanBeHyped { get; set; }
        [JsonProperty(PropertyName = "hype")] public HypeNominations Hype { get; set; }
        [JsonProperty(PropertyName = "nominations")] public HypeNominations Nominations { get; set; }
        [JsonProperty(PropertyName = "legacy_thread_url")] public string LegacyThreadUrl { get; set; }
        [JsonProperty(PropertyName = "beatmaps")] public Beatmaps[] Beatmaps { get; set; }

    }

    public class BeatmapsetsCovers
    {
        [JsonProperty(PropertyName = "cover")] public string CoverUrl { get; set; }
        [JsonProperty(PropertyName = "cover@2x")] public string Cover2XUrl { get; set; }
        [JsonProperty(PropertyName = "card")] public string CardUrl { get; set; }
        [JsonProperty(PropertyName = "card@2x")] public string Card2XUrl { get; set; }
        [JsonProperty(PropertyName = "list")] public string ListUrl { get; set; }
        [JsonProperty(PropertyName = "list@2x")] public string List2XUrl { get; set; }
        [JsonProperty(PropertyName = "slimcover")] public string SlimCoverUrl { get; set; }
        [JsonProperty(PropertyName = "slimcover@2x")] public string SlimCover2XUrl { get; set; }
    }

    public class HypeNominations
    {
        [JsonProperty(PropertyName = "current")] public int Current { get; set; }
        [JsonProperty(PropertyName = "required")] public int Required { get; set; }
    }
}
