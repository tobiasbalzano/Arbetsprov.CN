using System;
using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyPlaylistTrack
    {

        [JsonProperty("added_at")]
        public DateTime? AddedAt { get; set; }

        [JsonProperty("added_by")]
        public SpotifyUser User { get; set; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty("track")]
        public SpotifyTrack Track { get; set; }
    }
}
