namespace ServerSideProject.Model
{
    public class User
    {
        int Id;
        string Email;
        string Password;
        int PhoneNumber;
 
        string Img_url;

        bool IsAdmin;
       
        string Name;
        DateTime Register_date;
        static List<User> UserList = new List<User>();

        public User(int id, string email, string password, int phoneNumber, string img_url, string name, bool isadmin, DateTime register_date )
        {
            Id = id;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Img_url = img_url;
            IsAdmin = isadmin;
            Name = name;
            Register_date = register_date;
        }


        public int id { get => Id; set => Id = value; }
        public string email { get => Email; set => Email = value; }
        public string password { get => Password; set => Password = value; }
        public int phoneNumber { get => PhoneNumber; set => PhoneNumber = value; }
        public string img_url { get => Img_url; set => Img_url = value; }
        public bool isadmin { get => IsAdmin; set => IsAdmin = value; }
        //public bool isVerif { get => IsVerif; set => IsVerif = value; }
        //public int verification_code { get => Verification_code; set => Verification_code = value; }
        public string name { get => Name; set => Name = value; }
        public DateTime register_date { get => Register_date; set => Register_date = value; }
        //public DateTime verificationDATE { get => VerificationDATE; set => VerificationDATE = value; }
        /// Checks if user exist in database. If user exist return User object else return null. This method is used by Administrators to check if user exist in database.
        /// 
        /// 
        /// @return User object if user exist else null is returned in case user doesn't exist in database or user is
        public User IsUserExist()
        {
            DBservices dbs = new DBservices();
            User user = dbs.IsUserExist(this.email, this.password);
            /// Gets the value of the user property.
            if (user != null)
            {
                return user;
            }
            else { return null; }
        }
        /// Creates a new user in the database if not exists. This is used to check if user is already in the database
        /// 
        /// 
        /// @return true if user was
        public bool CreateNewUser()
        {
            DBservices dbs = new DBservices();
            /// Returns true if this user is a new user.
            if (dbs.CreateNewUserIfNotE(this) == 1)
            {
                return true;
            }
            else { return false; }
        }
        /// Update the user details. This is a wrapper for UpdateUserDetails ( DbService object String ). It returns true if the update was successful false otherwise
        /// 
        /// @param oldemail - OLDE Mail to update the user with
        /// 
        /// @return Boolean true if updated false if not ( could not update ) or error in DB service ( not found
        public bool UpdateUserDetails(string oldemail)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateUserDetails(this, oldemail);

        }

    }
}
