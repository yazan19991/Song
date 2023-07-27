using Microsoft.AspNetCore.Mvc;
using ServerSideProject.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpComingEventsController : ControllerBase
    {
        // GET: api/<UpComingEventsController>
        [HttpGet("GetArtistUpComingEvents")]
        public async Task<ActionResult<IEnumerable<string>>> GetEvents(string artistName)
        {
            var events = await UpComingEvents.GetArtistUpComingEvents(artistName);
            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }

        [HttpGet("GetArtistInfo")]
        public async Task<ActionResult<IEnumerable<string>>> GetArtistInfo(string artistName)
        {
            var ArtistInfo = await UpComingEvents.GetArtistInfo(artistName);
            if (ArtistInfo == null)
            {
                return NotFound();
            }

            return Ok(ArtistInfo);
        }

    }
}
