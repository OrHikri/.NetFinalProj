using DataBase.Models;

namespace DataBase.Repositories
{
    public class ErrorDbRepository : IErrorDbRepository
    {
        readonly string connectionString;

        private List<Error> _Errors;

        static private ErrorDbRepository _instance = null;

        //01 change to private
        public ErrorDbRepository()
        {
            connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _Errors = new List<Error>();
        }



        public ErrorDbRepository(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }




        // Get Factory Of ErrorDbRepository  as singelton
        public static ErrorDbRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ErrorDbRepository();

                }
                return _instance;
            }
        }



        public List<Error> GetAllErrors()
        {
            using (TestDbContext db = new TestDbContext())
            {
                return db.Errors.ToList();
            }
        }

        public List<Error> GetAllMyErrorsInTest(int studentId, int examId)
        {
            using (TestDbContext db = new TestDbContext())
            {

                List<Error> E1 = db.Errors.Where(x => x.StudentID == studentId && x.ExamId == examId).ToList();

                return E1;
            }

        }

        public void InsertError(List<Error> ErrorsList)
        {
            using (TestDbContext db = new TestDbContext())
            { foreach (Error error in ErrorsList) 
                {
                db.Errors.Add(error);
                db.SaveChanges();
                }
            }
        }

        public bool UpdateError(Error ErrorToUpdate)
        {
            using (TestDbContext db = new TestDbContext())
            {
                var foundError = db.Errors.SingleOrDefault(c => c.Id == ErrorToUpdate.Id);
                if (foundError != null)
                {
                    foundError = ErrorToUpdate;
                    db.SaveChanges();
                }
                return true;

            }
        }

        public bool DeleteError(int id)
        {
            using (TestDbContext db = new TestDbContext())
            {
                var foundError = db.Errors.SingleOrDefault(c => c.Id == id);
                if (foundError != null)
                {
                    db.Errors.Remove(foundError);
                    db.SaveChanges();
                }
                return true;
            }
        }


    }
}
