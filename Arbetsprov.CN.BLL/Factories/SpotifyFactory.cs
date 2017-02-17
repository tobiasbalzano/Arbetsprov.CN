using System;
using System.Collections.Generic;
using Arbetsprov.CN.Integrations.Spotify.Models;
using Arbetsprov.CN.Models.Entities;

namespace Arbetsprov.CN.BLL.Factories
{
    public static class SpotifyFactory
    {
        public static Category CreateCategory(SpotifyCategory inCategory)
        {
            Category returnCategory = new Category
            {
                Id = inCategory.Id,
                Name = inCategory.Name,
                Images = ConvertImageList(inCategory.Icons)
            };
            return returnCategory;
        }

        public static ImageInfo CreateImageInfo(SpotifyImage inImage)
        {
            return new ImageInfo
            {
                Height = inImage.Height ?? 0,
                Width = inImage.Width ?? 0,
                Url = inImage.Url
            };
        }

        public static Playlist CreatePlaylist(SpotifyPlaylist inPlaylist)
        {
            return new Playlist
            {
                Id = inPlaylist.Id,
                Uri = inPlaylist.Uri,
                Name = inPlaylist.Name,
                Images = ConvertImageList(inPlaylist.Images),
                Owner = CreateUserData(inPlaylist.Owner),
                Tracks = CreatePlaylistTracksInfo(inPlaylist.PlaylistTracks)
            };
        }

        public static UserData CreateUserData(SpotifyUser inUser)
        {
            return new UserData
            {
                Uri = inUser.Uri,
                Id = inUser.Id,
                Type = inUser.Type,
                Href = inUser.Href
            };
        }

        public static PlaylistTracksInfo CreatePlaylistTracksInfo(SpotifyPlaylistTracksObject inPlaylistTracks)
        {
            return new PlaylistTracksInfo
            {
                Href = inPlaylistTracks.Href,
                Total = inPlaylistTracks.Total
            };
        }

        public static Track CreateTrack(SpotifyTrack inTrack)
        {
            return new Track
            {
                Id = inTrack.Id,
                Uri = inTrack.Uri,
                Name = inTrack.Name,
                Album = CreateAlbum(inTrack.Album),
                Type = inTrack.Type,
                Artists = CreateArtists(inTrack.Artists),
                TrackNumber = inTrack.TrackNumber,
                DiscNumber = inTrack.DiscNumber,
                Duration = TimeSpan.FromMilliseconds(inTrack.DurationMs).ToString("g")
            };
        }

        public static Album CreateAlbum(SpotifyAlbum inAlbum)
        {
            return new Album
            {
                Uri = inAlbum.Uri,
                Name = inAlbum.Name,
                Id = inAlbum.Id,
                Type = inAlbum.Type,
                Images = ConvertImageList(inAlbum.Images),
                AlbumType = inAlbum.AlbumType,
                Artists = CreateArtists(inAlbum.Artists),
                AvailableMarkets = inAlbum.AvailableMarkets,
                Href = inAlbum.Href
            };
        }

        public static Artist CreateArtist(SpotifyArtist inArtist)
        {
            return new Artist
            {
                Id = inArtist.Id,
                Name = inArtist.Name,
                Uri = inArtist.Uri,
                Type = inArtist.Type,
                Href = inArtist.Href
            };
        }


        private static IList<ImageInfo> ConvertImageList(IList<SpotifyImage> inList)
        {
            ImageInfo[] returnA = new ImageInfo[inList.Count];
            int index = 0;
            foreach (SpotifyImage spotifyImageObject in inList)
            {
                returnA[index] = CreateImageInfo(spotifyImageObject);
            }
            return returnA;
        }

        private static IList<Artist> CreateArtists(IList<SpotifyArtist> inArtists)
        {
            if (inArtists != null)
            {

                IList<Artist> artists = new List<Artist>();
                foreach (var artist in inArtists)
                {
                    artists.Add(CreateArtist(artist));
                }
                return artists;
            }
            return null;
        }
    }
}
