using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ServerSideProject.Model
{
    public class UpComingEvents
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string api_key = "GtXKrwgVz5NmG4XUAcbj1busyYiUKEJc";
        private static readonly Dictionary<string, CachedData> ArtistIDcache = new Dictionary<string, CachedData>();
        private static readonly Dictionary<string, CachedData> ArtistInfoCache = new Dictionary<string, CachedData>();


        /// Fetches the ID of an artist. This is used to determine which artists are in the ticketmaster
        /// 
        /// @param artistName - The name of the artist
        /// 
        /// @return The artist's ID as a string or null if none is found in the ticketmaster's
        private async static Task<string> FetchArtistId(string artistName)
        {
            string artistIDUrl = $"https://app.ticketmaster.com/discovery/v2/attractions.json?keyword={artistName}&apikey={api_key}";
            var artistIDResponse = await client.GetAsync(artistIDUrl);
            artistIDResponse.EnsureSuccessStatusCode();
            var responseBodyForArtistID = await artistIDResponse.Content.ReadAsStringAsync();
            var data = JObject.Parse(responseBodyForArtistID);
            // Check if "_embedded" and "attractions" properties exist and attractions array is not empty
            if (data["_embedded"] != null && data["_embedded"]["attractions"] != null && data["_embedded"]["attractions"].HasValues)
            {
                return (string)data["_embedded"]["attractions"][0]["id"];  //Unique id of the entity in the discovery API to use it in GetArtistUpComingEvents
            }
            return null;
        }

        /// Gets the Coming events for an artist. This is used to determine which artists have been added to the ticketmaster
        /// 
        /// @param artistName - The name of the artist
        /// 
        /// @return A string containing the artist's events as JSON. It will be empty if there are no events 
        public async static Task<string> GetArtistUpComingEvents(string artistName)
        {
            try
            {
                if (!ArtistIDcache.ContainsKey(artistName))
                {
                    string artistId = await FetchArtistId(artistName);
                    if (artistId == null)
                    {
                        return null;
                    }
                    ArtistIDcache[artistName] = new CachedData(artistId);
                }
                var eventsUrl = $"https://app.ticketmaster.com/discovery/v2/events.json?attractionId={ArtistIDcache[artistName].Value}&apikey={api_key}";
                var eventsResponse = await client.GetAsync(eventsUrl);
                eventsResponse.EnsureSuccessStatusCode();
                var responseBodyForEvents = await eventsResponse.Content.ReadAsStringAsync();
                return responseBodyForEvents;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Gets the metadata for an artist. Caches the result for subsequent requests. Use this method to avoid hitting the ticketmaster API every time a new artist is added or removed
        /// 
        /// @param artistName - The name of the artist
        /// 
        /// @return A string containing the artists metadata in JSON format ( no charset or charset = utf - 8 )
        public async static Task<string> GetArtistInfo(string artistName)
        {
            if (ArtistInfoCache.ContainsKey(artistName))
            {
                ArtistInfoCache[artistName].LastAccessed = DateTime.Now; // Update the last accessed time
                return ArtistInfoCache[artistName].Value;
            }
            else
            {
                try
                {
                    if (!ArtistIDcache.ContainsKey(artistName))
                    {
                        string artistId = await FetchArtistId(artistName);
                        if (artistId == null)
                        {
                            return null;
                        }
                        ArtistIDcache[artistName] = new CachedData(artistId);
                    }
                    var artistUrl = $"https://app.ticketmaster.com/discovery/v2/attractions/{ArtistIDcache[artistName].Value}.json?apikey={api_key}";
                    var artistInfoResponse = await client.GetAsync(artistUrl);
                    artistInfoResponse.EnsureSuccessStatusCode();
                    var responseBodyForArtistInfo = await artistInfoResponse.Content.ReadAsStringAsync();
                    ArtistInfoCache[artistName] = new CachedData(responseBodyForArtistInfo);
                    return responseBodyForArtistInfo;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public static void CleanUpCache()
        {
            DateTime cutoff = DateTime.Now.AddDays(-7);
            var idsToRemove = ArtistIDcache.Where(kvp => kvp.Value.LastAccessed < cutoff).Select(kvp => kvp.Key).ToList();
            foreach (var id in idsToRemove)
            {
                ArtistIDcache.Remove(id);
            }

            var infoToRemove = ArtistInfoCache.Where(kvp => kvp.Value.LastAccessed < cutoff).Select(kvp => kvp.Key).ToList();
            foreach (var info in infoToRemove)
            {
                ArtistInfoCache.Remove(info);
            }
        }

    }
}





