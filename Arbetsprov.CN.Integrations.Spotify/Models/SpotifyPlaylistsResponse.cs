using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyPlaylistsResponse
    {
        [JsonProperty("playlists")]
        public SpotifyPagingObject<SpotifyPlaylist> Playlists { get; set; }
    }
}
