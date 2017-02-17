using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyCategoriesResponse
    {
        [JsonProperty("categories")]
        public SpotifyPagingObject<SpotifyCategory> Categories { get; set; }
    }
}
