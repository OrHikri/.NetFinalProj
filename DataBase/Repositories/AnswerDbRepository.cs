using DataBase.Models;

namespace DataBase.Repositories
{
    public class AnswerDbRepository : IAnswerDbRepository
    {


        readonly string connectionString;

        private List<Answer> _Answers;

        static private AnswerDbRepository _instance = null;

        public AnswerDbRepository()
        {
            connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _Answers = new List<Answer>();
        }


        public AnswerDbRepository(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        // Get Factory Of AnswerDbRepository  as singelton
        public static AnswerDbRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AnswerDbRepository();

                }
                return _instance;
            }
        }

        public List<Answer> GetAllAnswersByQusGuid(string QusGuid)
        {
            using (TestDbContext db = new TestDbContext())
            {
                List<Answer> ListAnswers = db.Answers.Where(x => x.QuestionGuid == QusGuid).ToList();

                return ListAnswers;
            }
        }


        public void InsertAnswer(Answer newAnswer)
        {
            using (TestDbContext db = new TestDbContext())
            {
                db.Answers.Add(newAnswer);
                db.SaveChanges();
            }
        }

        public bool UpdateAnswer(Answer AnswerToUpdate)
        {
            using (TestDbContext db = new TestDbContext())
            {   // first make sure the object is retrieved from the database 
                Answer Answer1 = db.Answers.FirstOrDefault(p => p.Id == AnswerToUpdate.Id);
               if (Answer1 != null)
                {    // then update its properties
                    Answer1.TextAnswer = AnswerToUpdate.TextAnswer; 
                    Answer1.CorrectAnswerIndex = AnswerToUpdate.CorrectAnswerIndex;
                    db.Answers.Update(Answer1);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            
        }

        public bool DeleteAllAnswersByQusGuid(string QusGuid)
        {
            try
            {
                using (TestDbContext db = new TestDbContext())
                {
                    List<Answer> AnsToDelete = db.Answers.Where(x => x.QuestionGuid == QusGuid).ToList();
                    foreach (Answer a in AnsToDelete)
                    {
                        db.Answers.Remove(a);
                        db.SaveChanges();

                    }
                    return true;
                }
            }

            catch (Exception e)
            {
                return false;
            }



        }

    }
}
