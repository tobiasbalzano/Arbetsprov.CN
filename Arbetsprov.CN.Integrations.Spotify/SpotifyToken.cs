using System;

namespace Arbetsprov.CN.Integrations.Spotify
{
    public sealed class SpotifyToken
    {
        private static SpotifyToken instance = null;
        private static readonly object padlock = new object();
        public string Token { get; set; }
        public string Type { get; set; }
        public DateTime ValidThrough { get; set; }

        private SpotifyToken()
        {
        }

        public static SpotifyToken Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance ?? (instance = new SpotifyToken());
                }
            }
        }
    }
}
