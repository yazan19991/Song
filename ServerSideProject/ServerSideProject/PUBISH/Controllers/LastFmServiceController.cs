using Microsoft.AspNetCore.Mvc;
using ServerSideProject.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LastFmServiceController : ControllerBase
    {
        // GET: api/<LastFmServiceController>
        [HttpGet("GetArtistInfo")]
        public Task<string> Get([FromQuery] string ArtistName)
        {
            return LastFmService.GetArtistInfo(ArtistName);
        }
        // GET: api/<LastFmServiceController>
        [HttpGet("SongInfo")]
        public Task<string> SongInfo([FromQuery] string artistName, [FromQuery] string songName)
        {
            return LastFmService.SongInfo(artistName,songName);
        }

    }
}
