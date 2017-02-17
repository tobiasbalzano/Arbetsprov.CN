using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyTracksAnalysisResponse
    {
        [JsonProperty("audio_features")]
        public IList<SpotifyTrackFeatures> AudioFeatures { get; set; }
    }
}
