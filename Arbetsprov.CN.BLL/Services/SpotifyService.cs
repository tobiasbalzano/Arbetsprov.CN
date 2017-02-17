using Arbetsprov.CN.BLL.Factories;
using Arbetsprov.CN.Integrations.Spotify;
using Arbetsprov.CN.Integrations.Spotify.Models;
using System.Collections.Generic;
using System.Linq;
using Arbetsprov.CN.Models.Entities;


namespace Arbetsprov.CN.BLL.Services
{
    public class SpotifyService
    {

        public IList<Category> GetCategories()
        {
            var request = Spotify.GetCategories();
            if (request.IsSuccess)
            {
                var categoriesResponse = request.Content;

                var categories = categoriesResponse.Categories;
                while (categoriesResponse.Categories.Next != null)
                {
                    request = Spotify.GetNextPageContent<SpotifyCategoriesResponse>(categoriesResponse.Categories.Next);
                    if (request.IsSuccess)
                    {
                        categoriesResponse = request.Content;
                        foreach (var item in categoriesResponse.Categories.Items)
                            categories.Items.Add(item);
                    }
                }

                IList<Category> returnList = new List<Category>();
                foreach (var spotifyCategory in categories.Items)
                {
                    returnList.Add(SpotifyFactory.CreateCategory(spotifyCategory));
                }
                return returnList;
            }
            return null;
        }

        public IList<Playlist> GetPlaylists(string category)
        {
            var request = Spotify.GetPlaylists(category);
            if (request.IsSuccess)
            {
                var spotifyPlaylistResponse = request.Content;

                var playlists = spotifyPlaylistResponse.Playlists;
                while (spotifyPlaylistResponse.Playlists.Next != null)
                {
                    request = Spotify.GetNextPageContent<SpotifyPlaylistsResponse>(spotifyPlaylistResponse.Playlists.Next);
                    if (request.IsSuccess)
                    {
                        spotifyPlaylistResponse = request.Content;
                        foreach (var item in spotifyPlaylistResponse.Playlists.Items)
                            playlists.Items.Add(item);
                    }
                }
                IList<Playlist> returnList = new List<Playlist>();
                foreach (var playlist in playlists.Items)
                {
                    returnList.Add(SpotifyFactory.CreatePlaylist(playlist));
                }
                return returnList;
            }
            return null;
        }

        public IList<Track> GetPlaylistTracks(string href)
        {
            var request = Spotify.GetPlaylistTracks(href);
            if (request.IsSuccess)
            {
                var playlistTracksResponse = request.Content;

                var spotifyTracks = playlistTracksResponse.Items;
                while (playlistTracksResponse.Next != null)
                {
                    request = Spotify.GetNextPageContent<SpotifyPagingObject<SpotifyPlaylistTrack>>(playlistTracksResponse.Next);
                    if (request.IsSuccess)
                    {
                        playlistTracksResponse = request.Content;
                        foreach (var track in playlistTracksResponse.Items)
                        {
                            spotifyTracks.Add(track);
                        }
                    }
                }
                IList<Track> returnList = new List<Track>();
                foreach (var spotifyTrack in spotifyTracks)
                {
                    returnList.Add(SpotifyFactory.CreateTrack(spotifyTrack.Track));
                }
                return returnList;
            }
            return null;
        }

        public IList<Track> GetRecommendations(List<Artist> artists, List<Track> tracks)
        {
            //Fix for Double.ToString() using , 
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;


            Dictionary<string, string> seedDictionary = new Dictionary<string, string>();
            TrackFeatures recommendationFeatures = new TrackFeatures();
            if (tracks.Count > 0)
            {
                recommendationFeatures = AnalyzeTracks(tracks);
                seedDictionary.Add("seed_tracks", string.Join(",", tracks.ConvertAll(t => t.Id)));
            }
            else if (artists.Count > 0)
            {
                var artistsTopTracks = new List<SpotifyTrack>();
                foreach (var artist in artists)
                {
                    var response = Spotify.GetArtistTopTracks(artist.Id);
                    if (response.IsSuccess)
                    {
                        artistsTopTracks.AddRange(response.Content.Tracks);
                    }
                }
                recommendationFeatures = AnalyzeTracks(artistsTopTracks.ConvertAll(SpotifyFactory.CreateTrack));
                seedDictionary.Add("seed_artists", string.Join(",", artists.ConvertAll(t => t.Id)));
            }
            foreach (var feature in recommendationFeatures.GetType().GetProperties())
            {
                seedDictionary.Add($"target_{feature.Name.ToLower()}", feature.GetValue(recommendationFeatures).ToString());
            }
            var recommendationResponse = Spotify.GetSeedRecommendations(seedDictionary);
            if (recommendationResponse.IsSuccess)
            {
                var responseContent = recommendationResponse.Content;
                var returnObject = new List<Track>();
                foreach (var spotifyTrack in responseContent.Tracks)
                {
                    returnObject.Add(SpotifyFactory.CreateTrack(spotifyTrack));
                }
                return returnObject;
            }
            return null;
        }

        public IList<Track> GetArtistTopTracks(string artistId)
        {
            var request = Spotify.GetArtistTopTracks(artistId);
            if (request.IsSuccess)
            {
                var artistTopTracks = request.Content;

                IList<Track> returnList = new List<Track>();
                foreach (var spotifyTrack in artistTopTracks.Tracks)
                {
                    returnList.Add(SpotifyFactory.CreateTrack(spotifyTrack));
                }
                return returnList;
            }
            return null;
        }

        private TrackFeatures AnalyzeTracks(List<Track> tracks)
        {
            var trackIds = tracks.ConvertAll(t => t.Id);
            var response = Spotify.GetTracksFeatures(trackIds);
            var responseContent = response.Content;

            int nTracks = responseContent.AudioFeatures.Count;

            return new TrackFeatures
            {
                Key = (responseContent.AudioFeatures.Sum(o => o.Key) / nTracks),
                Acousticness = (responseContent.AudioFeatures.Sum(o => o.Acousticness) / nTracks),
                Danceability = (responseContent.AudioFeatures.Sum(o => o.Danceability) / nTracks),
                Energy = (responseContent.AudioFeatures.Sum(o => o.Energy) / nTracks),
                Instrumentalness = (responseContent.AudioFeatures.Sum(o => o.Instrumentalness) / nTracks),
                Liveness = (responseContent.AudioFeatures.Sum(o => o.Liveness) / nTracks),
                Loudness = (responseContent.AudioFeatures.Sum(o => o.Loudness) / nTracks),
                Mode = (responseContent.AudioFeatures.Sum(o => o.Mode) / nTracks),
                Speechiness = (responseContent.AudioFeatures.Sum(o => o.Speechiness) / nTracks),
                Tempo = (responseContent.AudioFeatures.Sum(o => o.Tempo) / nTracks),
                Valence = (responseContent.AudioFeatures.Sum(o => o.Valence) / nTracks)
            };
        }
    }
}