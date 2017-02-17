using System.Collections.Generic;

namespace Arbetsprov.CN.Models.Entities
{
    public class Album
    {
        public string AlbumType { get; set; }

        public IList<Artist> Artists { get; set; }

        public IList<string> AvailableMarkets { get; set; }

        public string Href { get; set; }

        public string Id { get; set; }

        public IList<ImageInfo> Images { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Uri { get; set; }
    }
}