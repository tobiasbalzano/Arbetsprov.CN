using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyImage
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("height")]
        public int? Height { get; set; }

        [JsonProperty("width")]
        public int? Width { get; set; }
    }
}
