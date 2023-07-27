namespace ServerSideProject.Model
{
    public class Artist
    {
        int ArtistID;
        string ArtistName;
        string AdditionalInfo;

        public Artist(int artistID, string artistName)
        {
            ArtistID1 = artistID;
            ArtistName1 = artistName;
        }
        public Artist(int artistID, string artistName, string additionalInfo)
        {
            ArtistID1 = artistID;
            ArtistName1 = artistName;
            AdditionalInfo1 = additionalInfo;
        }

        public int ArtistID1 { get => ArtistID; set => ArtistID = value; }
        public string ArtistName1 { get => ArtistName; set => ArtistName = value; }
        public string AdditionalInfo1 { get => AdditionalInfo; set => AdditionalInfo = value; }


        /// Gets all artists in the database. 
        /// 
        /// 
        /// @return List of all artists

        static public List<Artist> GetAllArtists()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllArtists();
        }

        /// Gets the information of Artist. This method is used to get the artist information from Last.fm
        /// 
        /// @param ArtistID - The ID of the artist you want to get information about. This is required.
        /// @param ArtistName - The name of the artist you want to get information about. This is required.
        /// 
        /// @return Artist object with information about the artist you want to get information about. 
        static public Artist GetInfoArtist(int ArtistID,string ArtistName)
        {
            Task<string> AdditionalInfo = LastFmService.GetArtistInfo(ArtistName);
             Artist artist1 = new Artist(
                    ArtistID,
                    ArtistName,
                    AdditionalInfo.ToString()
            );
            return artist1;
        }



        
    }
}
