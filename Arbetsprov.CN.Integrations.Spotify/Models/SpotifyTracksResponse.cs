using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyTracksResponse
    {
        [JsonProperty("tracks")]
        public IList<SpotifyTrack> Tracks { get; set; }
    }
}
