using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyCategoriesResponse
    {
        [JsonProperty("categories")]
        public SpotifyPagingObject<SpotifyCategory> Categories { get; set; }
    }
}
