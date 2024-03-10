using DataBase.Models;

namespace DataBase.Repositories
{
    public interface IUserDbRepository
    {
        bool DeleteUser(int id);

        List<User> GetAllUsers();
        void InsertUser(User newUser);
        bool UpdateUser(User UserToUpdate);

        User GetUserByNameAndPsw(string name, string password);

        List<User> GetAllUsersByType(string UserType);

        bool FindUser(User newUser);






    }
}
