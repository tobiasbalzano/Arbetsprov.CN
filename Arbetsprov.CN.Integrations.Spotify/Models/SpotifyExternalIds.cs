using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyExternalIds
    {

        [JsonProperty("isrc")]
        public string Isrc { get; set; }
    }
}
