namespace TestAdministration.Models
{
    public class UserLogin
    {
        //Property
        public string Username { get; set; }

        public int Password { get; set; }

        public UserLogin()
        {
        }

        public UserLogin(string userName, int pass)
        {
            Username = userName;
            Password = pass;
        }
    }
}
