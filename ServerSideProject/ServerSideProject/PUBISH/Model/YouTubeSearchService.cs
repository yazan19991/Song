using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ServerSideProject.Model
{
    public class YouTubeSearchService
    {
        /// Searches YouTube for videos matching the search term. This is a shortcut for search ( string ) but can be used as a more convenient way to search for videos
        /// 
        /// @param searchTerm - The search term to search for
        /// @param searchType - The type of search you are searching for
        /// @param HowMush
        /// 
        /// @return A list of search results that match the search term and search type or null if no results are found
        public async Task<SearchResult> Search(string searchTerm, string searchType, int HowMush)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAq0QPZNMWc1TwZ4f4R6z5hxJBJ6lLylYk",

               ApplicationName = this.GetType().ToString()
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchTerm;
            searchListRequest.MaxResults = HowMush;

            // Include the searchType in the request
            searchListRequest.Type = searchType;

            var searchListResponse = await searchListRequest.ExecuteAsync();
            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();
            int counter = 0;
            foreach (var searchResult in searchListResponse.Items)
            {
                counter++;
                /// Add the search result to the search result.
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":

                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }

            }

            return new SearchResult
            {
                Videos = videos,
                Channels = channels,
                Playlists = playlists
            };
        }
    }

    public class SearchResult
    {
        public List<string> Videos { get; set; }
        public List<string> Channels { get; set; }
        public List<string> Playlists { get; set; }
    }
}
