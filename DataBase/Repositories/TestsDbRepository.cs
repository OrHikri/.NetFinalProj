using DataBase.Models;

namespace DataBase.Repositories
{
    public class TestsDbRepository : ITestsDBRepository
    {

        readonly string connectionString;

        private List<Test> _Tests;

        static private TestsDbRepository _instance = null;

        public TestsDbRepository()
        {
            connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _Tests = new List<Test>();
        }


        public TestsDbRepository(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        //Get Factory Of TestsDbRepository  as singelton
        public static TestsDbRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TestsDbRepository();

                }
                return _instance;
            }
        }


        public List<Test> GetAllTests()
        {
            using (TestDbContext db = new TestDbContext())
            {
                return db.Tests.ToList();
            }
        }

        public List<Test> GetAllTestsByTestGuidOrName(string TestGuidOrName)
        {
            using (TestDbContext db = new TestDbContext())
            {
                List<Test> T1 = db.Tests.Where(x => x.TestGuid.Contains(TestGuidOrName) || x.Name.Contains(TestGuidOrName)).ToList();

                return T1;
            }
        }

        public void InsertTest(Test newTest)
        {
            using (TestDbContext db = new TestDbContext())
            {
               
                db.Tests.Add(newTest);
                db.SaveChanges();
            }
        }

        public bool UpdateTest(Test TestToUpdate)
        {
            using (TestDbContext db = new TestDbContext())
            {   // first make sure the object is retrieved from the database 
                Test Test1 = db.Tests.FirstOrDefault(p => p.Id == TestToUpdate.Id);
                if (Test1 != null)
                {     // then update its properties
                    Test1.Name = TestToUpdate.Name;
                    Test1.Date = TestToUpdate.Date;
                    Test1.TeacherName = TestToUpdate.TeacherName;
                    Test1.StartTime = TestToUpdate.StartTime;
                    Test1.IsRendomOrder = TestToUpdate.IsRendomOrder;
                    Test1.TotalTime = TestToUpdate.TotalTime;
                    db.Tests.Update(Test1);
                    db.SaveChanges();
                    return true;
                }
                return false;

            }
            
        }

        public bool DeleteTest(int Id)
        {
     
                using (TestDbContext db = new TestDbContext())
                {
                    Test TestToDelete = db.Tests.SingleOrDefault(x => x.Id == Id);
                if (TestToDelete != null)
                { 
                    db.Tests.Remove(TestToDelete);
                    db.SaveChanges();
                   
                }
                return true;
            }


        }


  

    }
}
