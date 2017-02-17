using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyPlaylistTracksObject
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
