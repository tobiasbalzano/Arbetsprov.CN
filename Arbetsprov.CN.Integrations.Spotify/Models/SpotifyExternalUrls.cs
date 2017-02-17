using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }
}

