using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Arbetsprov.CN.Integrations.Generic;
using Arbetsprov.CN.Integrations.Spotify.Models;
using Newtonsoft.Json;

namespace Arbetsprov.CN.Integrations.Spotify
{
    public static class Spotify
    {
        private static readonly string ClientId = "996d0037680544c987287a9b0470fdbb";
        private static readonly string ClientSecret = "5a3c92099a324b8f9e45d77e919fec13";

        private static readonly Uri BaseAddress = new Uri("https://api.spotify.com");

        private static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = BaseAddress,
            Timeout = TimeSpan.FromSeconds(30),
        };

        private static readonly HttpClient AuthClient = new HttpClient
        {
            BaseAddress = new Uri("https://accounts.spotify.com"),
            Timeout = TimeSpan.FromSeconds(30),
        };


        public static ApiRequestObject<SpotifyCategoriesResponse> GetCategories()
        {
            var endpoint = "/v1/browse/categories";

            CheckAuthValidity();
            Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{SpotifyToken.Instance.Type} {SpotifyToken.Instance.Token}");

            var response = Client.GetAsync(endpoint).Result;
            var returnObject = new ApiRequestObject<SpotifyCategoriesResponse>();
            if (returnObject.IsSuccess = response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                returnObject.Content = JsonConvert.DeserializeObject<SpotifyCategoriesResponse>(responseContent);
            }
            return returnObject;
        }

        public static ApiRequestObject<SpotifyPlaylistsResponse> GetPlaylists(string category)
        {
            string endpoint = $"/v1/browse/categories/{category}/playlists";

            CheckAuthValidity();
            Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{SpotifyToken.Instance.Type} {SpotifyToken.Instance.Token}");

            var response = Client.GetAsync(endpoint).Result;
            var returnObject = new ApiRequestObject<SpotifyPlaylistsResponse>();
            if (returnObject.IsSuccess = response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                returnObject.Content = JsonConvert.DeserializeObject<SpotifyPlaylistsResponse>(responseContent);
            }
            return returnObject;
        }

        public static ApiRequestObject<SpotifyPagingObject<SpotifyPlaylistTrack>> GetPlaylistTracks(string pageAddress)
        {
            CheckAuthValidity();
            Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{SpotifyToken.Instance.Type} {SpotifyToken.Instance.Token}");

            var response = Client.GetAsync(new Uri(pageAddress)).Result;
            var returnObject = new ApiRequestObject<SpotifyPagingObject<SpotifyPlaylistTrack>>();
            if (returnObject.IsSuccess = response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                returnObject.Content = JsonConvert.DeserializeObject<SpotifyPagingObject<SpotifyPlaylistTrack>>(responseContent);
            }
            return returnObject;
        }

        public static ApiRequestObject<T> GetNextPageContent<T>(string pageAddress)
        {
            CheckAuthValidity();
            Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{SpotifyToken.Instance.Type} {SpotifyToken.Instance.Token}");

            var response = Client.GetAsync(new Uri(pageAddress)).Result;
            var returnObject = new ApiRequestObject<T>();
            if (returnObject.IsSuccess = response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                returnObject.Content = JsonConvert.DeserializeObject<T>(responseContent);
            }
            return returnObject;
        }

        public static ApiRequestObject<SpotifySeedRecommendResponse> GetSeedRecommendations(Dictionary<string, string> seedVariables)
        {
            string endpoint = $"/v1/recommendations?{string.Join("&", seedVariables.Select(i => $"{i.Key}={i.Value}"))}";

            CheckAuthValidity();
            Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{SpotifyToken.Instance.Type} {SpotifyToken.Instance.Token}");

            var result = Client.GetAsync(endpoint).Result;
            var returnObject = new ApiRequestObject<SpotifySeedRecommendResponse>();
            if (returnObject.IsSuccess = result.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = result.Content.ReadAsStringAsync().Result;
                returnObject.Content = JsonConvert.DeserializeObject<SpotifySeedRecommendResponse>(responseContent);
            }
            return returnObject;
        }

        public static ApiRequestObject<SpotifyTracksAnalysisResponse> GetTracksFeatures(List<string> trackIdsList)
        {
            string endpoint = $"/v1/audio-features?ids={string.Join(",", trackIdsList)}";
            CheckAuthValidity();
            Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{SpotifyToken.Instance.Type} {SpotifyToken.Instance.Token}");

            var response = Client.GetAsync(endpoint).Result;
            var returnObject = new ApiRequestObject<SpotifyTracksAnalysisResponse>();
            if (returnObject.IsSuccess = response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                returnObject.Content = JsonConvert.DeserializeObject<SpotifyTracksAnalysisResponse>(responseContent);
            }
            return returnObject;
        }

        public static ApiRequestObject<SpotifyTracksResponse> GetArtistTopTracks(string artistId)
        {
            string endpoint = $"/v1/artists/{artistId}/top-tracks?country=SE";
            CheckAuthValidity();
            Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{SpotifyToken.Instance.Type} {SpotifyToken.Instance.Token}");

            var response = Client.GetAsync(endpoint).Result;
            var returnObject = new ApiRequestObject<SpotifyTracksResponse>();
            if (returnObject.IsSuccess = response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                returnObject.Content = JsonConvert.DeserializeObject<SpotifyTracksResponse>(responseContent);
            }
            return returnObject;
        }

        private static void CheckAuthValidity()
        {
            if (SpotifyToken.Instance == null || SpotifyToken.Instance.ValidThrough < DateTime.Now)
                Spotify.GetToken();
        }

        private static void GetToken()
        {
            AuthClient.DefaultRequestHeaders.Authorization =
                AuthenticationHeaderValue.Parse(
                    $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret))}");
            var grantType = new KeyValuePair<string, string>("grant_type", "client_credentials");
            var postBodyList = new List<KeyValuePair<string, string>>() { grantType };
            var postBody = new FormUrlEncodedContent(postBodyList);

            var response = AuthClient.PostAsync("/api/token", postBody).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var tokenResponse =
                    JsonConvert.DeserializeObject<SpotifyAuthResponse>(content);
                SpotifyToken.Instance.Token = tokenResponse.AccessToken;
                SpotifyToken.Instance.Type = tokenResponse.TokenType;
                SpotifyToken.Instance.ValidThrough = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn);
            }
        }
    }


}

