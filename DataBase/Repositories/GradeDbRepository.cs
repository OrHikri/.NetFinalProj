using DataBase.Models;

namespace DataBase.Repositories
{
    public class GradeDbRepository : IGradeDBRepository
    {
        readonly string connectionString;

        private List<Grade> _Grades;

        static private GradeDbRepository _instance = null;


        //01 change to private
        public GradeDbRepository()
        {
            connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _Grades = new List<Grade>();
        }

        public GradeDbRepository(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }



        //03 Get Factory Of GradeDbRepository  as singelton
        public static GradeDbRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GradeDbRepository();

                }
                return _instance;
            }
        }

        public List<Grade> GetAllGrades()
        {
            using (TestDbContext db = new TestDbContext())
            {
                return db.Grades.ToList();
            }
        }

        public List<Grade> GetAllGradesById(int StudentId)
        {
            using (TestDbContext db = new TestDbContext())
            {
                List<Grade> ListGrades = db.Grades.Where(x => x.StudentID == StudentId).ToList();

                return ListGrades;
            }
        }

        public double GetAverageGrade()
        {
            using (TestDbContext db = new TestDbContext())
            {

                double average = db.Grades.Select(x => x.ExamGrade).Average();

                return Math.Ceiling(average);

            }
        }

        public void InsertGrade(Grade newGrade)
        {
            using (TestDbContext db = new TestDbContext())
            {

                db.Grades.Add(newGrade);
                db.SaveChanges();
            }
        }

        public bool UpdateGrade(Grade GradeToUpdate)
        {
            using (TestDbContext db = new TestDbContext())
            {
                var foundGrade = db.Grades.SingleOrDefault(c => c.Id == GradeToUpdate.Id);
                if (foundGrade != null)
                {
                    foundGrade = GradeToUpdate;
                    db.SaveChanges();
                }
                return true;

            }
        }

        public bool DeleteGrade(int id)
        {
            using (TestDbContext db = new TestDbContext())
            {
                var foundGrade = db.Grades.SingleOrDefault(c => c.Id == id);
                if (foundGrade != null)
                {
                    db.Grades.Remove(foundGrade);
                    db.SaveChanges();
                }
                return true;
            }
        }





    }
}

