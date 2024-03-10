using DataBase.Models;
using DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server_APi.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //Every Request is using the same object usersRepo
        private readonly IUserDbRepository usersRepo = null;

        //This constractor is inilized by ASP.NET API
        //IUsersRepository usersRepo is injected as UserDbRepository instance (initilized by asp.NET Injection)
        //UserDbRepository is singelton - same instance for all usages
        //Every Request is using the constractor
        public UsersController(IUserDbRepository usersRepo)
        {
         this.usersRepo = usersRepo;
        }

        // GET: api/Users
        [HttpGet]
        public List<User> Get()
        {
            return usersRepo.GetAllUsers();
        }

        // GET: api/Users/UserType
        [HttpGet("{UserType}")]
        public List<User> GetAllUsersByType(string UserType) 
        {;
            return usersRepo.GetAllUsersByType(UserType);
        }

        // Post: api/Users/FindtUser/User
        [HttpPost("FindtUser")]
        public ActionResult<bool> FindtUser( User newUser)
        {
            bool IsOk = usersRepo.FindUser(newUser);
             return IsOk; 
           
        }

        // Post api/Users/5,name
        [HttpPost("Login")]
        public ActionResult<User>? GetUserByNameAndPsw([FromBody] UserLogin userLogin)
        {
            
            User user = usersRepo.GetUserByNameAndPsw(userLogin.UserName, userLogin.Password.ToString());
            if (user != null)
            {
                return user;
            }
            else
            {
                return NotFound("User Not Exsist");
            }
            
        }

        // POST api/Users/Insert
        [HttpPost("Insert")]
        public ActionResult Post([FromBody] User newUser)
        {
            newUser.Id = 0;
            if (newUser != null)
            { 
             usersRepo.InsertUser(newUser);

            return Ok();
            }
        else
            { return BadRequest();
            }

        }

        // PUT api/Users/5
        [HttpPut("Update/{id}")]
        public bool Put(int id, [FromBody] User userToUpdate)
        {
            bool isOk = usersRepo.UpdateUser(userToUpdate);
            return isOk;
        }

        // DELETE api/Users/2
        [HttpDelete("Delete/{id}")]
        public bool DeleteUser(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            bool isOk = usersRepo.DeleteUser(id);
            return isOk;

        }
    }
}
    
