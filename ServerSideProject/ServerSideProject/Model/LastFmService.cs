using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerSideProject.Model
{
    public class LastFmService
    {
        private static readonly HttpClient client = new HttpClient();

        /// Gets information about an artist. This is a public method.
        /// 
        /// @param artistName - The name of the artist. For example " John Smith "
        /// 
        /// @return A JSON object containing artist

        public async static Task<string> GetArtistInfo(string artistName)
        {
            try
            {
                var apiKey = "";
                var response = await client.GetAsync($"http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist={artistName}&api_key={apiKey}&format=json");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async static Task<string> SongInfo(string artistName,string songName)
        {
            try
            {
                var apiKey = "";
                var encodedArtist = System.Net.WebUtility.UrlEncode(artistName);
                var encodedSong = System.Net.WebUtility.UrlEncode(songName);
                var response = await client.GetAsync($"https://ws.audioscrobbler.com/2.0/?method=track.getInfo&api_key={apiKey}&artist={encodedArtist}&track={encodedSong}&format=json");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
