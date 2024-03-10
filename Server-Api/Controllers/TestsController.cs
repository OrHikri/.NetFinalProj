using DataBase;
using DataBase.Models;
using DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server_APi.Controllers
{
    [Route("api/Tests")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestsDBRepository testsRepo = null;
        public TestsController(ITestsDBRepository testsRepo)
        {
            this.testsRepo = testsRepo;
        }

        // GET: api/Tests
        [HttpGet]
        public ActionResult<List<Test>> GetAllTests()
        {
            return testsRepo.GetAllTests();

        }

        // GET: api/Tests/GetAllTestsByTestGuidOrName/TestGuidOrName
        [HttpGet("GetAllTestsByTestGuidOrName")]
        public ActionResult<List<Test>> GetAllTestsByTestGuidOrName(string TestGuidOrName)
        {
            return testsRepo.GetAllTestsByTestGuidOrName(TestGuidOrName);

        }

         // POST api/Tests/Insert
        [HttpPost("Insert")]
        public ActionResult Post([FromBody] Test newTest)
        {

            if (newTest != null)
            {
                testsRepo.InsertTest(newTest);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // Post api/tests/Update
        [HttpPut("Update")]
        public ActionResult<bool> Update(Test TestToUpdate)
        {
            using (TestDbContext db = new TestDbContext())
            {
                if (TestToUpdate != null)
                {
                    testsRepo.UpdateTest(TestToUpdate);
                    db.SaveChanges();
                    return true;
                }
                return false;

            }
        }

        // DELETE api/<TestsController>/5
        [HttpDelete("Delete/{id}")]
        public ActionResult<bool> Delete(int id)
        {
            using (TestDbContext db = new TestDbContext())
            {
               
                if (id != null)
                {
                    testsRepo.DeleteTest(id);
                    db.SaveChanges();
                    return true;

                }
                return false;
                

            }
        }
    }
}
