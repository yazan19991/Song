using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using ServerSideProject.Model;
public class DBservices
{
    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }
    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, Dictionary<string, object> paramDic)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        if (paramDic != null)
            foreach (KeyValuePair<string, object> param in paramDic)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);

            }


        return cmd;
    }

    //////////////////////////////////////////user/////////////////////////////////////////////////////////

    public int CreateNewUserIfNotE(User user)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Email", user.email);
        paramDic.Add("@Password", user.password);
        paramDic.Add("@PhoneNumber", user.phoneNumber);
        paramDic.Add("@Img_url", user.img_url);
        paramDic.Add("@IsAdmin", 0);
        //paramDic.Add("@Verification_code", user.Verification_code);
        //paramDic.Add("@IsVerif", 0);
        paramDic.Add("@Name", user.name);
        cmd = CreateCommandWithStoredProcedure("SP_Proj_CreateNewUser", con, paramDic); // create the command

        try
        {
            // Execute the command and read the returned value
            int returnValue = (int)cmd.ExecuteScalar();
            return returnValue;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    public User IsUserExist(string email, string password)
    {
        SqlConnection con;
        SqlCommand cmd;
        User user = null;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Email", email);
        paramDic.Add("@password", password);

        cmd = CreateCommandWithStoredProcedure("SP_Proj_IsUserExist", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command

            // Check if any records are returned
            if (reader.HasRows)
            {
                // Read the user data
                reader.Read();
                user = new User(
                    Convert.ToInt32(reader["id"]),
                    reader["Email"].ToString(),
                    reader["password"].ToString(),
                    Convert.ToInt32(reader["PhoneNumber"]),
                    reader["Img_url"].ToString(),
                    reader["Name"].ToString(),
                    Convert.ToBoolean(reader["isadmin"]),
                    Convert.ToDateTime(reader["register_date"])
                );
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return user;
    }
    public bool UpdateUserDetails(User user, string oldemail)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = new SqlCommand("SP_Proj_UpdateUserDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", user.id);
        cmd.Parameters.AddWithValue("@Email", user.email);
        cmd.Parameters.AddWithValue("@Password", user.password);
        cmd.Parameters.AddWithValue("@PhoneNumber", user.phoneNumber);
        cmd.Parameters.AddWithValue("@Img_url", user.img_url);
        cmd.Parameters.AddWithValue("@isadmin", user.isadmin);
        cmd.Parameters.AddWithValue("@Name", user.name);
        cmd.Parameters.AddWithValue("@oldemail", oldemail);

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected > 0; // return true if at least one row was affected
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    //////////////////////////////////////////Favorites/////////////////////////////////////////////////////////
   public bool AddSongToFav(int userid,int songid)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = new SqlCommand("SP_proj_AddSongToFavorites", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@userid",userid);
        cmd.Parameters.AddWithValue("@songid",songid);


        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected > 0; // return true if at least one row was affected
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
       public bool RemoveSongFromFavorites(int userid,int songid)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = new SqlCommand("SP_proj_RemoveSongFromFavorites", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@userid",userid);
        cmd.Parameters.AddWithValue("@songid",songid);


        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected > 0; // return true if at least one row was affected
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    public int GetSongFavoriteCount(int songid)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        // New stored procedure name that gets the count of a song in Favorites_proj
        cmd = new SqlCommand("SP_proj_CountSongInFav", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@songid", songid);

        try
        {
            // Execute the command and read the returned value
            int favoriteCount = (int)cmd.ExecuteScalar();
            return favoriteCount;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    ////////////////////////////////////////// Song /////////////////////////////////////////////////////////
    public List<Song> GetSongByName(string SongName)
    {
        List<Song> Songs = new List<Song>();
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@SongName", SongName);

        cmd = CreateCommandWithStoredProcedure("SP_proj_GetSongByName", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command and get the SqlDataReader

            while (reader.Read())
            {
                Song Song = new Song(
                    Convert.ToInt32(reader["id"]),
                    reader["artist_name"].ToString(),
                    reader["song_name"].ToString(),
                    reader["link"].ToString(),
                    reader["lyrics"].ToString(),
                    reader["songimg"].ToString()

                );
                Songs.Add(Song);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return Songs; // return the list of flats
    }
    public List<Song> GetSongBySingerID(int SingerID)
    {
        List<Song> Songs = new List<Song>();
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@ArtistsID", SingerID);

        cmd = CreateCommandWithStoredProcedure("SP_proj_GetSongBySingerID", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command and get the SqlDataReader

            while (reader.Read())
            {
                Song Song = new Song(
                    Convert.ToInt32(reader["id"]),
                    reader["artist_name"].ToString(),
                    reader["song_name"].ToString(),
                    reader["link"].ToString(),
                    reader["lyrics"].ToString(),
                    reader["songimg"].ToString()

                );
                Songs.Add(Song);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return Songs; // return the list of flats
    }
    public List<Song> GetSongBySongID(int SongID)
    {
        List<Song> Songs = new List<Song>();
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@SongID", SongID);

        cmd = CreateCommandWithStoredProcedure("SP_proj_GetSongBySongID", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command and get the SqlDataReader

            while (reader.Read())
            {
                Song Song = new Song(
                    Convert.ToInt32(reader["id"]),
                    reader["artist_name"].ToString(),
                    reader["song_name"].ToString(),
                    reader["songimg"].ToString()

                );
                Songs.Add(Song);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return Songs; // return the list of flats
    }

    public List<Song> GetSongBySingerName(string SingerName)
    {
        List<Song> Songs = new List<Song>();
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@SingerName", SingerName);

        cmd = CreateCommandWithStoredProcedure("SP_proj_GetSongBySingerName", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command and get the SqlDataReader

            while (reader.Read())
            {
                Song Song = new Song(
                    Convert.ToInt32(reader["id"]),
                    reader["artist_name"].ToString(),
                    reader["song_name"].ToString(),
                    reader["link"].ToString(),
                    reader["lyrics"].ToString(),
                    reader["songimg"].ToString()

                );
                Songs.Add(Song);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return Songs; // return the list of flats
    }
    public List<Song> GetSongBySingerNameOnlyname(string SingerName)
    {
        List<Song> Songs = new List<Song>();
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@SingerName", SingerName);

        cmd = CreateCommandWithStoredProcedure("SP_proj_GetSongBySingerName", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command and get the SqlDataReader

            while (reader.Read())
            {
                Song Song = new Song(
                    Convert.ToInt32(reader["id"]),
                    reader["artist_name"].ToString(),
                    reader["song_name"].ToString(),
                    reader["songimg"].ToString()
                );
                Songs.Add(Song);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return Songs; // return the list of flats
    }

    public List<Song> GetallSongs()
    {
        List<Song> Songs = new List<Song>();
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();


        cmd = CreateCommandWithStoredProcedure("SP_proj_GetAllSongs", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command and get the SqlDataReader

            while (reader.Read())
            {
                Song Song = new Song(
                    Convert.ToInt32(reader["id"]),
                    reader["artist_name"].ToString(),
                    reader["song_name"].ToString(),
                    reader["songimg"].ToString()
                );
                Songs.Add(Song);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return Songs; // return the list of flats
    }




    public List<Song> GetFavoritesSong(int userID)
    {
        List<Song> Songs = new List<Song>();
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();


        cmd = CreateCommandWithStoredProcedure("SP_proj_getFavoritesSong", con, paramDic); // create the command
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@userid", userID);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command and get the SqlDataReader

            while (reader.Read())
            {
                Song Song = new Song(
                    Convert.ToInt32(reader["id"]),
                    reader["artist_name"].ToString(),
                    reader["song_name"].ToString(),
                    reader["songimg"].ToString()
                );
                Songs.Add(Song);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return Songs; // return the list of flats
    }

    public List<Song> GetSongBySonglyrics(string Songlyric)
    {
        List<Song> Songs = new List<Song>();
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Songlyrics", Songlyric);

        cmd = CreateCommandWithStoredProcedure("SP_proj_GetSongBySonglyrics", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command and get the SqlDataReader

            while (reader.Read())
            {
                Song Song = new Song(
                    Convert.ToInt32(reader["id"]),
                    reader["artist_name"].ToString(),
                    reader["song_name"].ToString(),
                    reader["link"].ToString(),
                    reader["lyrics"].ToString(),
                    reader["songimg"].ToString()
                );
                Songs.Add(Song);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return Songs; // return the list of flats
    }

    public bool IsSongInUserFavorites(int userID, int songID)
    {
        SqlConnection con;
        SqlCommand cmd;
        bool isInFavorites = false;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", userID);
        paramDic.Add("@SongID", songID);

        cmd = CreateCommandWithStoredProcedure("SP_proj_isSongFavorites", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command

            // Check if any records are returned
            if (reader.HasRows)
            {
                // Read the count
                reader.Read();
                int count = Convert.ToInt32(reader[0]);
                isInFavorites = count > 0;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return isInFavorites;
    }






    ////////////////////////////////////////// Artist /////////////////////////////////////////////////////////
    public List<Artist> GetAllArtists()
    {
        List<Artist> Artists = new List<Artist>();
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        cmd = CreateCommandWithStoredProcedure("SP_proj_GetAllArtists", con, paramDic); // create the command

        try
        {
            SqlDataReader reader = cmd.ExecuteReader(); // execute the command and get the SqlDataReader

            while (reader.Read())
            {
                Artist Artist = new Artist(
                    Convert.ToInt32(reader["ArtistID"]),
                    reader["ArtistName"].ToString(),
                    reader["PhotoLink"].ToString()
                );
                Artists.Add(Artist);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return Artists; // return the list of flats
    }
}
