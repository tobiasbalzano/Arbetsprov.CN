using System.Collections.Generic;

namespace Arbetsprov.CN.Models.Entities
{
    public class Playlist
    {
        public string Id { get; set; }

        public IList<ImageInfo> Images { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public UserData Owner { get; set; }

        public PlaylistTracksInfo Tracks { get; set; }

    }
}