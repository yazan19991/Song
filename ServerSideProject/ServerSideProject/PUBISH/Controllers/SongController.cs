using Microsoft.AspNetCore.Mvc;
using ServerSideProject.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        [HttpPost("AddSongToFavorites")]
        public bool AddSongToFavorites([FromQuery] int userid,[FromQuery] int songid)
        {
            return Song.AddSongToFavorites(userid,songid);
        }
        [HttpDelete("RemoveSongFromFavorites")]
        public bool RemoveSongFromFavorites([FromQuery] int userid,[FromQuery] int songid)
        {
            return Song.RemoveSongFromFavorites(userid,songid);
        }
        [HttpGet("GetSongByName")]
        public List<Song> GetSongByName([FromQuery] string SongName)
        {
            return Song.GetSongByName(SongName);
        }
        [HttpGet("GetSongBySingerID")]
        public List<Song> GetSongBySingerID([FromQuery] int SingerID)
        {
            return Song.GetSongBySingerID(SingerID);
        }
        [HttpGet("GetSongBySongID")]
        public List<Song> GetSongBySongID([FromQuery] int SongID)
        {
            return Song.GetSongBySongID(SongID);
        }
        [HttpGet("GetSongBySingerName")]
        public List<Song> GetSongBySingerName([FromQuery] string SingerName)
        {
            return Song.GetSongBySingerName(SingerName);
        }
        [HttpGet("GetSongBySingerNameOnlyname")]
        public List<Song> GetSongBySingerNameOnlyname([FromQuery] string SingerName)
        {
            return Song.GetSongBySingerNameOnlyname(SingerName);
        }
        [HttpGet("GetSongBySonglyrics")]
        public List<Song> GetSongBySonglyrics([FromQuery] string Songlyric)
        {
            return Song.GetSongBySonglyrics(Songlyric);
        }
        [HttpGet("GetallSongs")]
        public List<Song> GetallSongs()
        {
            return Song.GetallSongs();
        }
        [HttpGet("GetFavoritesSong")]
        public List<Song> GetFavoritesSong([FromQuery] int userID)
        {
            return Song.GetFavoritesSong(userID);
        }
        [HttpGet("IsSongInUserFavorites")]
        public bool IsSongInUserFavorites(int userID, int songID)
        {
            return Song.IsSongInUserFavorites(userID,songID);
        }
        [HttpGet("GetSongFavoriteCount")]
        public int GetSongFavoriteCount(int songID)
        {
            return Song.GetSongFavoriteCount(songID);
        }
    }
}
