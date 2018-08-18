using System;
using Newtonsoft.Json;

namespace Bleatingsheep.Osu.ApiV2.Models
{
    public class UserV2
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("username")] public string Username { get; set; }
        public string join_date { get; set; }
        [JsonProperty("country")] public Country Country { get; set; }
        public object age { get; set; }
        public string avatar_url { get; set; }
        public bool is_admin { get; set; }
        public bool is_supporter { get; set; }
        public bool is_gmt { get; set; }
        public bool is_qat { get; set; }
        public bool is_bng { get; set; }
        public bool is_bot { get; set; }
        public bool is_active { get; set; }
        public string interests { get; set; }
        public string occupation { get; set; }
        public object title { get; set; }
        public string location { get; set; }
        public DateTime lastvisit { get; set; }
        public string twitter { get; set; }
        public object lastfm { get; set; }
        public string skype { get; set; }
        public string website { get; set; }
        public string discord { get; set; }
        public string[] playstyle { get; set; }
        [JsonProperty("playmode")] public string Playmode { get; set; }
        public bool pm_friends_only { get; set; }
        public int post_count { get; set; }
        public object profile_colour { get; set; }
        public string[] profile_order { get; set; }
        public string cover_url { get; set; }
        public Cover cover { get; set; }
        public Kudosu kudosu { get; set; }
        public int max_blocks { get; set; }
        public int max_friends { get; set; }
        public object[] account_history { get; set; }
        public object[] active_tournament_banner { get; set; }
        public Badge[] badges { get; set; }
        public int[] favourite_beatmapset_count { get; set; }
        public int[] follower_count { get; set; }
        public int[] graveyard_beatmapset_count { get; set; }
        public int[] loved_beatmapset_count { get; set; }
        public MonthlyPlayCounts[] monthly_playcounts { get; set; }
        public Page page { get; set; }
        [JsonProperty("previous_usernames")] public string[] PreviousUsernames { get; set; }
        public int[] ranked_and_approved_beatmapset_count { get; set; }
        public ReplaysWatchedCounts[] replays_watched_counts { get; set; }
        public int[] scores_first_count { get; set; }
        [JsonProperty("statistics")] public Statistics Statistics { get; set; }
        public int[] unranked_beatmapset_count { get; set; }
        public UserAchievements[] user_achievements { get; set; }
        public RankHistory rankHistory { get; set; }
    }

    public class Badge
    {
        public DateTime awarded_at { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
    }

    public class Country
    {
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }

    public class Cover
    {
        public object custom_url { get; set; }
        public string url { get; set; }
        public string id { get; set; }
    }

    public class Kudosu
    {
        public int total { get; set; }
        public int available { get; set; }
    }

    public class Page
    {
        public string html { get; set; }
        public string raw { get; set; }
    }

    public class Statistics
    {
        [JsonProperty("level")] public Level Level { get; set; }
        [JsonProperty("pp")] public float Performance { get; set; }
        [JsonProperty("pp_rank")] public int PerformanceRank { get; set; }
        [JsonProperty("ranked_score")] public long RankedScore { get; set; }
        [JsonProperty("hit_accuracy")] private double RawAccuracy { get; set; }
        [JsonIgnore] public double Accuracy => RawAccuracy / 100.0;
        [JsonProperty("play_count")] public int PlayCount { get; set; }
        [JsonProperty("play_time")] public int PlaySeconds { get; set; }
        [JsonIgnore] public TimeSpan PlayTime => new TimeSpan(0, 0, PlaySeconds);
        [JsonProperty("total_score")] public long TotalScore { get; set; }
        [JsonProperty("total_hits")] public int TotalHits { get; set; }
        [JsonProperty("maximum_combo")] public int MaximumCombo { get; set; }
        [JsonProperty("replays_watched_by_others")] public int ReplaysWatchedByOthers { get; set; }
        [JsonProperty("is_ranked")] public bool IsRanked { get; set; }
        [JsonProperty("grade_counts")] public GradeCounts GradeCounts { get; set; }
        [JsonProperty("rank")] public Rank Rank { get; set; }
        [JsonProperty("scoreRanks")] public ScoreRanks ScoreRanks { get; set; }
    }

    public class Level
    {
        [JsonProperty("current")] public int Current { get; set; }
        [JsonProperty("progress")] public int Progress { get; set; }
    }

    public class GradeCounts
    {
        public int ss { get; set; }
        public int ssh { get; set; }
        public int s { get; set; }
        public int sh { get; set; }
        public int a { get; set; }
    }

    public class Rank
    {
        [JsonProperty("global")] public int? Global { get; set; }
        [JsonProperty("country")] public int? Country { get; set; }
    }

    public class ScoreRanks
    {
        public int XH { get; set; }
        public int SH { get; set; }
        public int X { get; set; }
        public int S { get; set; }
        public int A { get; set; }
    }

    public class RankHistory
    {
        public string mode { get; set; }
        public int[] data { get; set; }
    }

    public class MonthlyPlayCounts
    {
        public string start_date { get; set; }
        public int count { get; set; }
    }

    public class ReplaysWatchedCounts
    {
        public string start_date { get; set; }
        public int count { get; set; }
    }

    public class UserAchievements
    {
        public DateTime achieved_at { get; set; }
        public int achievement_id { get; set; }
    }
}
