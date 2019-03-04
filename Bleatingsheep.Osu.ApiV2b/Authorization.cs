using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bleatingsheep.Osu.ApiV2b.Utils;
using Newtonsoft.Json;

namespace Bleatingsheep.Osu.ApiV2b
{
    internal class Authorization
    {
        private readonly string _username;
        private readonly string _password;
        private TokenInfo _tokenInfo = TokenInfo.Default;
        private readonly object _refreshLock = new object();

        public Authorization(string username, string password)
        {
            _username = username;
            _password = password;
        }

        private ApiStatus Refresh(TokenInfo oldInfo)
        {
            lock (_refreshLock)
            {
                if (oldInfo != _tokenInfo) return ApiStatus.Success;

                string responseText;
                var now = DateTime.UtcNow;
                TokenResponse tokenResponse;

                // Get Token Response.

                var tokenInfo = new TokenRequest
                {
                    Username = _username,
                    Password = _password,
                };
                try
                {
                    HttpResponseMessage response =
                        HttpClientUtil.HttpPost("https://osu.ppy.sh/oauth/token", tokenInfo);
                    try
                    {
                        response.EnsureSuccessStatusCode();
                    }
                    catch (Exception)
                    {
                        return ApiStatus.AuthorizationFail;
                    }
                    if (!response.Content.Headers.ContentType.MediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
                    {
                        return ApiStatus.AuthorizationFail;
                    }
                    responseText = response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception)
                {
                    return ApiStatus.NetworkFail;
                }

                try
                {
                    tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseText);
                }
                catch (Exception)
                {
                    return ApiStatus.BadData;
                }

                DateTime expire = now + new TimeSpan(0, 0, tokenResponse.ExpiresInSeconds);
                DateTime prefer;
                try
                {
                    checked
                    {
                        prefer = now + new TimeSpan(0, 0, tokenResponse.ExpiresInSeconds * 9 / 10);
                    }
                }
                catch (OverflowException)
                {
                    prefer = now;
                }
                var newInfo = new TokenInfo(tokenResponse.AccessToken, expire, prefer);
                _tokenInfo = newInfo;
                return ApiStatus.Success;
            }
        }

        private ApiStatus EnsureValid(ref TokenInfo tokenInfo)
        {
            if (!tokenInfo.IsValid)
            {
                var status = Refresh(tokenInfo);
                if (status != ApiStatus.Success) return status;
                tokenInfo = _tokenInfo;
                return ApiStatus.Success;
            }
            return ApiStatus.Success;
        }

        private async void UpdateIfNotPreferredBackground()
        {
            try
            {
                await Task.Run(() =>
                {
                    var tokenInfo = _tokenInfo;
                    if (!tokenInfo.IsPreferred)
                    {
                        Refresh(tokenInfo);
                    }
                });
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public (ApiStatus, string) GetAccessToken()
        {
            var tokenInfo = _tokenInfo;
            var apiStatus = EnsureValid(ref tokenInfo);
            if (apiStatus != ApiStatus.Success) return (apiStatus, null);
            string result = tokenInfo.AccessToken;
            UpdateIfNotPreferredBackground();
            return (ApiStatus.Success, result);
        }

        public async Task<(ApiStatus, string)> GetAccessTokenAsync() => await Task.Run(() => GetAccessToken());
    }
}
