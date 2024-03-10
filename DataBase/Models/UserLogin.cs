namespace DataBase.Models
{
    public class UserLogin
    {

        public string UserName { get; set; }

        public int Password { get; set; }

        public UserLogin()
        {
        }

        public UserLogin(string username, int pass)
        {
            UserName = username;
            Password = pass;
        }
    }
}
