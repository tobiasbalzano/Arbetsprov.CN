﻿using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify.Models
{
    public class SpotifyTrackFeatures
    {

        [JsonProperty("danceability")]
        public double Danceability { get; set; }

        [JsonProperty("energy")]
        public double Energy { get; set; }

        [JsonProperty("key")]
        public int Key { get; set; }

        [JsonProperty("loudness")]
        public double Loudness { get; set; }

        [JsonProperty("mode")]
        public int Mode { get; set; }

        [JsonProperty("speechiness")]
        public double Speechiness { get; set; }

        [JsonProperty("acousticness")]
        public double Acousticness { get; set; }

        [JsonProperty("instrumentalness")]
        public double Instrumentalness { get; set; }

        [JsonProperty("liveness")]
        public double Liveness { get; set; }

        [JsonProperty("valence")]
        public double Valence { get; set; }

        [JsonProperty("tempo")]
        public double Tempo { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("track_href")]
        public string TrackHref { get; set; }

        [JsonProperty("analysis_url")]
        public string AnalysisUrl { get; set; }

        [JsonProperty("duration_ms")]
        public int DurationMs { get; set; }

        [JsonProperty("time_signature")]
        public int TimeSignature { get; set; }
    }
}
