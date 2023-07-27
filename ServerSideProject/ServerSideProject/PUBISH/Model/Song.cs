namespace ServerSideProject.Model
{
    public class Song
    {
        int SongID;
        string Artist_name;
        string SongName;
        string Link;
        string Lyrics;
        string Img_url;
        public Song(int songID, string artist_name, string songName,string link, string lyrics, string img_url)
        {
            SongID = songID;
            Artist_name = artist_name;
            SongName1 = songName;
            Link =link;
            Lyrics1 = lyrics;
            SongName1 = songName;
            Img_url = img_url;
        }
        public Song(int songID, string artist_name, string songName, string img_url)
        {
            SongID = songID;
            Artist_name = artist_name;
            SongName1 = songName;
            Img_url = img_url;
        }
        public int SongID1 { get => SongID; set => SongID = value; }
        public string ArtistID1 { get => Artist_name; set => Artist_name = value; }
        public string SongName1 { get => SongName; set => SongName = value; }
        public string Lyrics1 { get => Lyrics; set => Lyrics = value; }
        public string Link1 { get => Link; set => Link = value; }

        public string img_url { get => Img_url; set => Img_url = value; }
 /////////////////////////  Favorites  ///////////////////////// 

        /// Add a song to favorites. Song will be added to favorites if it's not already in favorites
        /// 
        /// @param userid - User to whom the song belongs
        /// @param songid - ID of the song to add to favorites
        /// 
        /// @return True if successful false if not ( could not add song to favorites for some reason ) NOTE : this is a public

        public static bool AddSongToFavorites(int userid,int songid){
            DBservices dbs = new DBservices();
            return dbs.AddSongToFav(userid,songid);
        }
        /// Remove a song from favorites. Song will be removed from  favorite list 
        /// 
        /// @param userid - The userid of the user who favored the song
        /// @param songid - The id of the song to remove
        /// 
        /// @return True if successful false

        public static bool RemoveSongFromFavorites(int userid,int songid){
            DBservices dbs = new DBservices();
            return dbs.RemoveSongFromFavorites(userid,songid);
        }
 /////////////////////////  Song  ///////////////////////// 
        /// Gets a list of songs by name. 
        /// 
        /// @param SongName - The name of the song to look up
        /// 
        /// @return A list of songs or an empty list if not found or the song does not exist in the
        public static List<Song> GetSongByName(string SongName){
            DBservices dbs = new DBservices();
            return dbs.GetSongByName(SongName);
        }
        /// Gets a song by SingerID. This is used to get all songs that have been played by same Singer
        /// 
        /// @param SingerID - The ID of the song
        /// 
        /// @return A list of songs or null if not found or no songs were played in this song
        public static List<Song> GetSongBySingerID(int SingerID){
            DBservices dbs = new DBservices();
            return dbs.GetSongBySingerID(SingerID);
        }

        public static List<Song> GetSongBySongID(int SongID)
        {
            DBservices dbs = new DBservices();
            return dbs.GetSongBySongID(SongID);
        }
        /// Gets a list of songs by SingerName. This is used to search for song in DB
        /// 
        /// @param SingerName - The name of the song you want to look for
        /// 
        /// @return A list of songs that match the query or an empty list if no match is found. The list is sorted by time
        public static List<Song> GetSongBySingerName(string SingerName){
            DBservices dbs = new DBservices();
            return dbs.GetSongBySingerName(SingerName);
        }
        public static List<Song> GetSongBySingerNameOnlyname(string SingerName)
        {
            DBservices dbs = new DBservices();
            return dbs.GetSongBySingerNameOnlyname(SingerName);
        }
        /// Gets a list of songs by song lyric. This is used to search for song in database
        /// 
        /// @param Songlyric - The song lyric of the song you want to search for
        /// 
        /// @return A list of songs that match the search criteria. If no song is found null is returned. If there are multiple songs the first one is
        public static List<Song> GetSongBySonglyrics(string Songlyric){
            DBservices dbs = new DBservices();
            return dbs.GetSongBySonglyrics(Songlyric);
        }
        public static List<Song> GetallSongs()
        {
            DBservices dbs = new DBservices();
            return dbs.GetallSongs();
        }
        public static List<Song> GetFavoritesSong(int userID)
        {
            DBservices dbs = new DBservices();
            return dbs.GetFavoritesSong(userID);
        }
        public static bool IsSongInUserFavorites(int userID, int songID)
        {
            DBservices dbs = new DBservices();
            return dbs.IsSongInUserFavorites(userID, songID);
        }
        public static int GetSongFavoriteCount(int songID)
        {
            DBservices dbs = new DBservices();
            return dbs.GetSongFavoriteCount(songID);
        }
    }
}
