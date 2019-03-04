using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bleatingsheep.Osu.ApiV2b.Models
{
    public class Cursor
    {
        [JsonProperty("_score")]
        public double Score { get; set; }
        [JsonProperty("_id")]
        public long Id { get; set; }
    }
}
