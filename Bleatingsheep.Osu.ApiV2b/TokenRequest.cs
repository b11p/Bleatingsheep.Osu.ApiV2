using Newtonsoft.Json;

namespace Bleatingsheep.Osu.ApiV2b
{
    internal class TokenRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("grant_type")]
        private string GrantType { get; set; } = "password";

        [JsonProperty("client_id")]
        private string ClientId { get; set; } = "5";

        [JsonProperty("client_secret")]
        private string ClientSecret { get; set; } = "FGc9GAtyHzeQDshWP5Ah7dega8hJACAJpQtw6OXk";
    }
}
