using DataBase.Models;
using DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server_Api.Controllers
{
    [Route("api/Errors")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
 
            private readonly IErrorDbRepository errorsRepo = null;

            public ErrorsController(IErrorDbRepository errorsRepo)
            {
                this.errorsRepo = errorsRepo;
            }

         // GET: api/<ErrorsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Errors/GetAllMyErrorsInTest
        [HttpGet("GetAllMyErrorsInTest/")]
        public ActionResult<List<Error>> GetAllMyErrorsInTest(int StudentId, int ExamId)
        {
            return errorsRepo.GetAllMyErrorsInTest(StudentId, ExamId);

        }

        // POST api/Errors/Insert
        [HttpPost("Insert")]
        public ActionResult Post(List<Error> ErrorsList)
        {
            if (ErrorsList != null)
            {
                errorsRepo.InsertError(ErrorsList);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        // PUT api/Errors/Update
        [HttpPut("Update")]
        public bool Put(int id, [FromBody] Error ErrorToUpdate)
        {
            bool isOk = errorsRepo.UpdateError(ErrorToUpdate);
            return isOk;
        }

        // DELETE api/Errors/Delete/2
        [HttpDelete("Delete/{id}")]
        public bool DeleteError(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            bool isOk = errorsRepo.DeleteError(id);
            return isOk;

        }
    }
}
