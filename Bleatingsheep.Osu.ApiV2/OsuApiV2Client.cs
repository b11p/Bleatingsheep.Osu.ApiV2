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

        #endregion private members
    }
}
