using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ServerSideProject.Model;

namespace ServerSideProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeSearchController : ControllerBase
    {
        private YouTubeSearchService _searchService;

        public YoutubeSearchController()
        {
            _searchService = new YouTubeSearchService();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string searchTerm, string searchType, int HowMush)
        {
            var result = await _searchService.Search(searchTerm, searchType, HowMush);
            return Ok(result);
        }
    }
}
