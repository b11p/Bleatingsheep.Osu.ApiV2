using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bleatingsheep.Osu.ApiV2.Models;
using Bleatingsheep.Osu.ApiV2.Utils;
using Newtonsoft.Json;

namespace Bleatingsheep.Osu.ApiV2
{
    public class OsuApiV2Client
    {
        public OsuApiV2Client(string username, string password) => _auth = new Authorization(username, password);

        /// <summary>
        /// Get user information with default mode set by user.
        /// </summary>
        public async Task<UserV2> GetUserAsync(int osuId) => await GetAsync<UserV2>(UserUrl(osuId));

        public async Task<UserV2> GetUserAsync(int osuId, Mode mode) => await GetAsync<UserV2>(UserUrl(osuId, mode));

        public async Task<Beatmapsets[]> SearchBeatMapAsync(string keyword) =>
            await GetAsync<Beatmapsets[]>(BeatmapsetsUrl(keyword, null));
        public async Task<Beatmapsets[]> SearchBeatMapAsync(string keyword, BeatmapsetsSearchOptions options) =>
            await GetAsync<Beatmapsets[]>(BeatmapsetsUrl(keyword, options));


        #region private members

        private readonly Authorization _auth;

        private const string BaseUrl = "https://osu.ppy.sh/api/v2/";

        private static string UserUrl(int osuId, Mode? mode = null)
        {
            string result = BaseUrl + $"users/{osuId}";
            string modeString = mode?.ModeString();
            if (modeString != null)
            {
                result += "/" + modeString;
            }

            return result;
        }

        private static string BeatmapsetsUrl(string keyword, BeatmapsetsSearchOptions options)
        {
            var paramDic = new Dictionary<string, string> { { "q", keyword } };
            if (options != null) AddParams(options, paramDic);

            string paramStr = paramDic.ToUrlParamString();
            const string setsUrl = BaseUrl + "beatmapsets/search";

            string result = setsUrl + paramStr;

            return result;
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var (status, accessToken) = await _auth.GetAccessTokenAsync();

            switch (status)
            {
                case ApiStatus.NetworkFail:
                    throw new NetworkFailException("Network error.");
                case ApiStatus.NotFound:
                    throw new NotFoundException("Can not find specific data.");
                case ApiStatus.AuthorizationFail:
                    throw new AuthorizationFailException("Authorization Fail. Please check the username and password.");
                case ApiStatus.BadData:
                    throw new BadDataException("Received Data is invalid.");
            }

            HttpResponseMessage response;
            try
            {
                response = HttpClientUtil.HttpGet(url, null, new Dictionary<string, string>
                {
                    {"Authorization", "Bearer " + accessToken}
                });
                if (response == null)
                    throw new NetworkFailException();
                response = response.EnsureSuccessStatusCode();
            }
            catch (TaskCanceledException)
            {
                throw new NetworkFailException($"Request timed out. ({HttpClientUtil.Timeout}ms)");
            }
            catch (Exception e)
            {
                throw new NetworkFailException("Network error.", e);
            }

            if (!response.Content.Headers.ContentType.MediaType.Equals("application/json",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new NotFoundException("Can not find specific data.");
            }

            string responseText = await response.Content.ReadAsStringAsync();

            try
            {
                T result = JsonConvert.DeserializeObject<T>(responseText);
                return result;
            }
            catch (Exception e)
            {
                throw new BadDataException("Received Data is invalid.", e);
            }
        }

        private static void AddParams(BeatmapsetsSearchOptions options, IDictionary<string, string> param)
        {
            const string mode = "m";
            const string status = "s";
            const string genre = "g";
            const string language = "l";
            const string extra = "e";
            switch (options.Mode)
            {
                case Mode.Taiko:
                    param.Add(mode, ((int)Mode.Taiko).ToString());
                    break;
                case Mode.Fruits:
                    param.Add(mode, ((int)Mode.Fruits).ToString());
                    break;
                case Mode.Mania:
                    param.Add(mode, ((int)Mode.Mania).ToString());
                    break;
            }

            switch (options.Status)
            {
                case BeatmapStatus.Favourites:
                    param.Add(status, ((int)BeatmapStatus.Favourites).ToString());
                    break;
                case BeatmapStatus.Qualified:
                    param.Add(status, ((int)BeatmapStatus.Qualified).ToString());
                    break;
                case BeatmapStatus.PendingWip:
                    param.Add(status, ((int)BeatmapStatus.PendingWip).ToString());
                    break;
                case BeatmapStatus.Graveyard:
                    param.Add(status, ((int)BeatmapStatus.Graveyard).ToString());
                    break;
                case BeatmapStatus.Any:
                    param.Add(status, ((int)BeatmapStatus.Any).ToString());
                    break;
            }

            switch (options.Genre)
            {
                case Genre.Unspecified:
                    param.Add(genre, ((int)Genre.Unspecified).ToString());
                    break;
                case Genre.VideoGame:
                    param.Add(genre, ((int)Genre.VideoGame).ToString());
                    break;
                case Genre.Anime:
                    param.Add(genre, ((int)Genre.Anime).ToString());
                    break;
                case Genre.Rock:
                    param.Add(genre, ((int)Genre.Rock).ToString());
                    break;
                case Genre.Pop:
                    param.Add(genre, ((int)Genre.Pop).ToString());
                    break;
                case Genre.Other:
                    param.Add(genre, ((int)Genre.Other).ToString());
                    break;
                case Genre.Novelty:
                    param.Add(genre, ((int)Genre.Novelty).ToString());
                    break;
                case Genre.HipHop:
                    param.Add(genre, ((int)Genre.HipHop).ToString());
                    break;
            }

            switch (options.Language)
            {
                case Language.Other:
                    param.Add(language, ((int)Language.Other).ToString());
                    break;
                case Language.English:
                    param.Add(language, ((int)Language.English).ToString());
                    break;
                case Language.Japanese:
                    param.Add(language, ((int)Language.Japanese).ToString());
                    break;
                case Language.Chinese:
                    param.Add(language, ((int)Language.Chinese).ToString());
                    break;
                case Language.Instrumental:
                    param.Add(language, ((int)Language.Instrumental).ToString());
                    break;
                case Language.Korean:
                    param.Add(language, ((int)Language.Korean).ToString());
                    break;
                case Language.French:
                    param.Add(language, ((int)Language.French).ToString());
                    break;
                case Language.German:
                    param.Add(language, ((int)Language.German).ToString());
                    break;
                case Language.Swedish:
                    param.Add(language, ((int)Language.Swedish).ToString());
                    break;
                case Language.Spanish:
                    param.Add(language, ((int)Language.Spanish).ToString());
                    break;
                case Language.Italian:
                    param.Add(language, ((int)Language.Italian).ToString());
                    break;
            }

            switch (options.Extra)
            {
                case Extra.HasStoryboard:
                    param.Add(extra, "storyboard");
                    break;
                case Extra.HasVideo:
                    param.Add(extra, "video");
                    break;
                case Extra.HasStoryboard | Extra.HasVideo:
                    param.Add(extra, "storyboard.video");
                    break;
            }

            if (options.Page > 1)
            {
                param.Add("page", options.Page.ToString());
            }
        }

        #endregion private members
    }
}
