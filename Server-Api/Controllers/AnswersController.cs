using DataBase;
using DataBase.Models;
using DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server_Api.Controllers
{
    [Route("api/Answers")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerDbRepository answersRepo = null;

        public AnswersController(IAnswerDbRepository answersRepo)
        {
            this.answersRepo = answersRepo;
        }

 

        // GET api/Answers/GetAllAnswersByQusGuid/QusGuid
        [HttpGet("GetAllAnswersByQusGuid/{QusGuid}")]
        public ActionResult<List<Answer>> GetAllAnswersByQusGuid(string QusGuid)
        {
            return answersRepo.GetAllAnswersByQusGuid(QusGuid);

        }

        // POST api/Answers/Insert
        [HttpPost("Insert")]
        public ActionResult Post([FromBody] Answer newAnswer)
        {
            if (newAnswer != null)
            {
                answersRepo.InsertAnswer(newAnswer);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // Post api/Answers/Update
        [HttpPut("Update")]
        public ActionResult<bool> Update(Answer AnswerToUpdate)
        {
                if (AnswerToUpdate != null)
                {
                    answersRepo.UpdateAnswer(AnswerToUpdate);
                    return true;
                }
                return false;

        }

        // Delete api/Answers/Delete
        [HttpDelete("DeleteAllAnswersByQusGuid/{QusGuid}")]
        public ActionResult<bool> Delete(string QusGuid)
        {

            using (TestDbContext db = new TestDbContext())
            {

                if (QusGuid != null)
                {
                    answersRepo.DeleteAllAnswersByQusGuid(QusGuid);
                    return true;

                }
                return false;
            }
        }
    }
}
