using DataBase.Models;
using System.Security.Cryptography;


namespace DataBase.Repositories
{
    public class UserDbRepository : IUserDbRepository
    {


        readonly string connectionString;

        private List<User> _Users;

        static private UserDbRepository _instance = null;

        public UserDbRepository()
        {
            connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _Users = new List<User>();
        }


        public UserDbRepository(string dbConnectionString)
        {
            connectionString = dbConnectionString;

        }

        // Get Factory Of UserDbRepository  as singelton
        public static UserDbRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserDbRepository();

                }
                return _instance;
            }
        }

        public List<User> GetAllUsers()
        {
            using (TestDbContext db = new TestDbContext())
            {
                return db.Users.ToList();
            }
        }

        public List<User> GetAllUsersByType(string UserType)
        {
            using (TestDbContext db = new TestDbContext())
            {
                List<User> U1 = db.Users.Where(x => x.UserType == UserType).ToList();


                return U1;
            }
        }

        public User GetUserByNameAndPsw(string name, string password)
        {
            using (TestDbContext db = new TestDbContext())
            {
                try
                {
                    byte[] tmpSource = BitConverter.GetBytes(int.Parse(password));
                    byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                    int psw = BitConverter.ToInt32(tmpHash);
                    User User1;
                    User1 = db.Users.SingleOrDefault(c => c.UserName == name && c.Password == psw);
                    return User1;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public bool FindUser(User newUser)
        {
            using (TestDbContext db = new TestDbContext())
            {
                User User1;
                User1 = db.Users.SingleOrDefault(c => c.UserId == newUser.UserId );
                if (User1 != null)
                {
                    return true;
                }
                return false;

            }
        }



        public void InsertUser(User newUser)
        {
            byte[] tmpSource = BitConverter.GetBytes(newUser.Password);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            newUser.Password = BitConverter.ToInt32(tmpHash);
            using (TestDbContext db = new TestDbContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }

        public bool UpdateUser(User UserToUpdate)
        {
            using (TestDbContext db = new TestDbContext())
            {
                var foundUser = db.Users.SingleOrDefault(c => c.UserId == UserToUpdate.UserId);
                if (foundUser != null)
                {
                    foundUser = UserToUpdate;
                    db.SaveChanges();
                }
                return true;

            }
        }

        public bool DeleteUser(int id)
        {
            using (TestDbContext db = new TestDbContext())
            {
                var foundUser = db.Users.SingleOrDefault(c => c.Id == id);
                if (foundUser != null)
                {
                    db.Users.Remove(foundUser);
                    db.SaveChanges();
                }
                return true;
            }
        }




    }
}
