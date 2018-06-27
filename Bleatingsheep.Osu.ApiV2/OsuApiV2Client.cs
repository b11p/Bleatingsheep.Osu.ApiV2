using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bleatingsheep.Osu.ApiV2
{
    public class OsuApiV2Client
    {
        private readonly Authorization _auth;

        public OsuApiV2Client(string username, string password) => _auth = new Authorization(username, password);

        private const string BaseUrl = "https://osu.ppy.sh/api/v2/";

        private static string UserUrl(int osuId, Mode? mode = null)
        {
            string result = BaseUrl + $"users/{osuId}";
            string modeString = mode?.ModeString();
            if (modeString is string)
            {
                result += "/" + modeString;
            }
            return result;
        }

        /// <summary>
        /// Get user information with default mode set by user.
        /// </summary>
        public async Task<(ApiStatus, UserV2)> GetUserAsync(int osuId) => await GetAsync<UserV2>(UserUrl(osuId), _auth);

        public async Task<(ApiStatus, UserV2)> GetUserAsync(int osuId, Mode mode) => await GetAsync<UserV2>(UserUrl(osuId, mode), _auth);

        private static async Task<(ApiStatus, T)> GetAsync<T>(string url, Authorization authorization)
        {
            string responseText;
            var (status, accessToken) = await authorization.GetAccessTokenAsync();
            if (status != ApiStatus.Success) return (status, default(T));
            using (var httpClient = new HttpClient())
            {
                var message = new HttpRequestMessage(HttpMethod.Get, url);
                message.Headers.Add("Authorization", "Bearer " + accessToken);
                try
                {
                    var response = await httpClient.SendAsync(message);
                    try
                    {
                        response = response.EnsureSuccessStatusCode();
                    }
                    catch (Exception)
                    {
                        return (ApiStatus.NotFound, default(T));
                    }
                    if (!response.Content.Headers.ContentType.MediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
                    {
                        return (ApiStatus.AuthorizationFail, default(T));
                    }
                    responseText = await response.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {
                    return (ApiStatus.NetworkFail, default(T));
                }
            }
            try
            {
                T result = JsonConvert.DeserializeObject<T>(responseText);
                return (ApiStatus.Success, result);
            }
            catch (Exception)
            {
                return (ApiStatus.BadData, default(T));
            }
        }
    }
}
