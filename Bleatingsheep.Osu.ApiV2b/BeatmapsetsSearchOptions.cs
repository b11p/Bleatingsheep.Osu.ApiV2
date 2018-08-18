using System;
using System.Collections.Generic;
using System.Text;

namespace Bleatingsheep.Osu.ApiV2
{
    public class BeatmapsetsSearchOptions
    {
        public BeatmapStatus Status { get; set; } = 0;
        public Genre Genre { get; set; } = 0;
        public Language Language { get; set; } = 0;
        public Extra Extra { get; set; } = 0;
        public Mode Mode { get; set; } = 0;
        public int Page { get; set; } = 1;
    }

    public enum BeatmapStatus
    {
        RankedApproved, Favourites = 2, Qualified, PendingWip, Graveyard, Any = 7
    }

    public enum Genre
    {
        Any, Unspecified, VideoGame, Anime, Rock, Pop, Other, Novelty, HipHop,
    }

    public enum Language
    {
        Any, Other, English, Japanese, Chinese, Instrumental, Korean, French, German, Swedish, Spanish, Italian
    }

    [Flags]
    public enum Extra
    {
        None = 0, HasVideo = 1, HasStoryboard = 2
    }
}
