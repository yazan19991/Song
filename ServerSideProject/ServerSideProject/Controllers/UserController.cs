using Microsoft.AspNetCore.Mvc;
using ServerSideProject.Model;
using System.Diagnostics.Metrics;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("api/User/CreateNewUserIfNotE/")]
        public bool CreateNewUserIfNotE([FromBody] User user)
        {
            return user.CreateNewUser();
        }
        // POST api/<UserController>
        [HttpPost]
        [Route("/LogIn/{email}/{pass}")]
        public IActionResult LogIn(string email, string pass)
        {
            User user = new User(0, email, pass, 050050505, "Img_url", "name", false, Convert.ToDateTime("2023-07-24"));
            User x = user.IsUserExist();/*, Convert.ToDateTime("2023-07-24")*/
            if (x == null)
            {
                return NotFound();
            }
            string json = JsonSerializer.Serialize(x);
            HttpContext.Response.ContentType = "application/json";
            return Ok(json);
        }
        [HttpPost]
        [Route("UpdateUserDetails")]
        public bool UpdateUserDetails([FromBody] User user,[FromQuery] string oldemail)
        {
            return user.UpdateUserDetails(oldemail);
        }

    
    }
}
