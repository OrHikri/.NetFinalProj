namespace DataBase.Models
{
    public class User
    {
        public User()
        { 
        
        }


        //Property
     public int Id { get; set; }

    public int UserId { get; set; }

    public string UserType { get; set; }
    public string UserName { get; set; }

    public int Password { get; set; }

  


    public User(string userType, string userName, int userId, int password)
    {
       UserType = userType;
       UserName = userName;
       UserId = userId;
       Password = password;

    }
        public override string ToString()
        {
            return UserName + "-" + UserId ;

        }
    }
}
