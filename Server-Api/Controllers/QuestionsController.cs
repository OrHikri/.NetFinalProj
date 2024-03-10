using DataBase;
using DataBase.Models;
using DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server_Api.Controllers
{
    [Route("api/Questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestinDBRepository questionsRepo = null;

        public QuestionsController(IQuestinDBRepository questionsRepo)
        {
            this.questionsRepo = questionsRepo;
        }

        // GET: api/<QuestionsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Questions/GetAllTestsByTestGuidOrName/TestGuid
        [HttpGet("GetAllQuestionsByTestGuid/{TestGuid}")]
        public ActionResult<List<Question>> GetAllQuestionsByTestGuid(string TestGuid)
        {
            return questionsRepo.GetAllQuestionsByTestGuid(TestGuid);

        }

        // POST api/<QuestionsController>
        [HttpPost("Insert")]
        public ActionResult Post([FromBody] Question newQuestion)
        {
            if (newQuestion != null)
            {
                questionsRepo.InsertQuestion(newQuestion);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // Post api/Questions/Update
        [HttpPut("Update")]
        public ActionResult<bool> Update(Question QuestionToUpdate)
        {

                if (QuestionToUpdate != null)
                {
                    questionsRepo.UpdateQuestion(QuestionToUpdate);
                     return true;
                }
                return false;

            
        }

        // DELETE api/Questions/DeleteAllQuestionsByTestGuid/5
        [HttpDelete("DeleteAllQuestionsByTestGuid/{TestGuid}")]
        public ActionResult<bool> DeleteAllQuestionsByTestGuid(string TestGuid)
        {

            using (TestDbContext db = new TestDbContext())
            {

                if (TestGuid != null)
                {
                    questionsRepo.DeleteAllQuestionsByTestGuid(TestGuid);
                    db.SaveChanges();
                    return true;

                }
                return false;
            }
        }

        // DELETE api/Questions/Delete/5
        [HttpDelete("Delete/{qusGuid}")]
        public ActionResult<bool> Delete(string qusGuid)
        {

            using (TestDbContext db = new TestDbContext())
            {

                if (qusGuid != null)
                {
                    questionsRepo.DeleteQuestion(qusGuid);
                    return true;

                }
                return false;
            }
        }
    }
}
