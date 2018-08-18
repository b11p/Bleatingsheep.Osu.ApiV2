using System;

namespace Bleatingsheep.Osu.ApiV2b
{
    [Flags]
    public enum ApiStatus
    {
        Success = 0,
        NetworkFail = 1,
        NotFound = 2,
        AuthorizationFail = 4,
        BadData = 8,
    }
}
