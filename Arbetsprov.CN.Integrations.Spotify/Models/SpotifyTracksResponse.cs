using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyTracksResponse
    {
        [JsonProperty("tracks")]
        public IList<SpotifyTrack> Tracks { get; set; }
    }
}
