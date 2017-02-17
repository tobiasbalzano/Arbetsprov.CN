using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyPlaylistsResponse
    {
        [JsonProperty("playlists")]
        public SpotifyPagingObject<SpotifyPlaylist> Playlists { get; set; }
    }
}
