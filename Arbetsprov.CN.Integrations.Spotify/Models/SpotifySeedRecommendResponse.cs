using Newtonsoft.Json;
using System.Collections.Generic;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifySeedRecommendResponse
    {
        [JsonProperty("tracks")]
        public IList<SpotifyTrack> Tracks { get; set; }

        [JsonProperty("seeds")]
        public IList<SpotifySeedObject> Seeds { get; set; }
    }
}