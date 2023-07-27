using Microsoft.AspNetCore.Mvc;
using ServerSideProject.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {

        // GET api/<ArtistController>/5
        [HttpGet("GetAllArtists")]
        public List<Artist> GetAllArtists()
        {
            return Artist.GetAllArtists();
        }

    }
}
