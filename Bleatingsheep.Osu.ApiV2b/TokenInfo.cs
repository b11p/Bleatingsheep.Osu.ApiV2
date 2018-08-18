using System;

namespace Bleatingsheep.Osu.ApiV2
{
    internal class TokenInfo
    {
        private readonly DateTime _outDate;
        private readonly DateTime _preferDate;

        public TokenInfo(string accessToken, DateTime outDate, DateTime preferDate)
        {
            AccessToken = accessToken;
            _outDate = outDate;
            _preferDate = preferDate;
        }

        public string AccessToken { get; }

        public static TokenInfo Default => new TokenInfo(string.Empty, DateTime.MinValue, DateTime.MinValue);

        public bool IsPreferred => DateTime.UtcNow < _preferDate;

        public bool IsValid => DateTime.UtcNow < _outDate;
    }
}
