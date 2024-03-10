using DataBase.Models;
using DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server_Api.Controllers
{
    [Route("api/Grades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeDBRepository gradesRepo = null;

        public GradesController(IGradeDBRepository gradesRepo)
        {
            this.gradesRepo = gradesRepo;
        }

        // GET: api/Grades
        [HttpGet]
        public ActionResult<List<Grade>> GetAllGrades()
        {
            return gradesRepo.GetAllGrades();

        }

        // GET api/Grades/{studentId}
        [HttpGet("GetAllGradesById/{studentId}")]
        public ActionResult<List<Grade>> GetAllGradesById(int studentId)
        {
            return gradesRepo.GetAllGradesById(studentId);

        }

        // POST api/Grades/Insert
        [HttpPost("Insert")]
        public ActionResult Post([FromBody] Grade newGrade)
        {
            if (newGrade != null)
            {
                gradesRepo.InsertGrade(newGrade);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/Grades/Update
        [HttpPut("Update")]
        public bool Put(int id, [FromBody] Grade GradeToUpdate)
        {
            bool isOk = gradesRepo.UpdateGrade(GradeToUpdate);
            return isOk;
        }

        // DELETE api/Grades/Delete/2
        [HttpDelete("Delete/{id}")]
        public bool DeleteGrade(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            bool isOk = gradesRepo.DeleteGrade(id);
            return isOk;

        }
    }
}
