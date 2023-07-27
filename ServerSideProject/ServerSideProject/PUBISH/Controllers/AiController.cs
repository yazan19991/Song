using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerSideProject.Model;

namespace ServerSideProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly Gpt3Service _gpt3Service;

        public AiController(Gpt3Service gpt3Service)
        {
            _gpt3Service = gpt3Service;
        }

        [HttpPost("getAnswer")]
        public async Task<IActionResult> GetAnswer([FromBody] Gpt3Request aiRequest)
        {
            if (aiRequest == null || string.IsNullOrEmpty(aiRequest.Prompt))
            {
                return BadRequest("Invalid request.");
            }

            var gpt3Response = await _gpt3Service.GetCompletion(aiRequest.Prompt);

            if (gpt3Response == null)
            {
                return StatusCode(500, "Error retrieving response from GPT-3 API.");
            }

            return Ok(gpt3Response);
        }
    }
}
