using System.Collections.Generic;

namespace Arbetsprov.CN.Models.Entities
{
    public class Track
    {
        public Album Album { get; set; }

        public IList<Artist> Artists { get; set; }

        public IList<string> AvailableMarkets { get; set; }

        public int DiscNumber { get; set; }

        public string Duration { get; set; }

        public bool Explicit { get; set; }

        public string Href { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }

        public string PreviewUrl { get; set; }

        public int TrackNumber { get; set; }

        public string Type { get; set; }

        public string Uri { get; set; }
    }
}