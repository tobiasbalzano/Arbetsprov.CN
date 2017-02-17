using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyExternalIds
    {

        [JsonProperty("isrc")]
        public string Isrc { get; set; }
    }
}
