using DataBase.Models;


namespace DataBase.Repositories
{
    public class QuestionDbRepository : IQuestinDBRepository
    {

        readonly string connectionString;

        private List<Question> _Questions;

        static private QuestionDbRepository _instance = null;

        public QuestionDbRepository()
        {
            connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _Questions = new List<Question>();
        }


        public QuestionDbRepository(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        // Get Factory Of QuestionDbRepository  as singelton
        public static QuestionDbRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QuestionDbRepository();

                }
                return _instance;
            }
        }

        public bool DeleteAllQuestionsByTestGuid(string TestGuid)
        {
            try
            {
                using (TestDbContext db = new TestDbContext())
                {
                    List<Question> QusToDelete = db.Questions.Where(x => x.TestGuid == TestGuid).ToList();
                    foreach (Question q in QusToDelete)
                    {
                        db.Questions.Remove(q);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }


        }



        public List<Question> GetAllQuestionsByTestGuid(string TestGuid)
        {
            using (TestDbContext db = new TestDbContext())
            {
                List<Question> ListQuestions = db.Questions.Where(x => x.TestGuid == TestGuid).ToList();

                return ListQuestions;
            }
        }

        public Question FindQuestion(string examid)
        {
            using (TestDbContext db = new TestDbContext())
            {

                Question Question1 = db.Questions.SingleOrDefault(c => c.TestGuid == examid);
                return Question1;


            }
        }

        public void InsertQuestion(Question newQuestion)
        {
            using (TestDbContext db = new TestDbContext())
            {
                db.Questions.Add(newQuestion);
                db.SaveChanges();
            }
        }

        public bool UpdateQuestion(Question QuestionToUpdate)
        {
            using (TestDbContext db = new TestDbContext())
            {    // first make sure the object is retrieved from the database 
                Question Question1 = db.Questions.FirstOrDefault(p => p.Id == QuestionToUpdate.Id);
                if (Question1 != null)
                {
                    // then update its properties
                    Question1.TextQuestion = QuestionToUpdate.TextQuestion;
                    Question1.IsRendomAnswerOrder = QuestionToUpdate.IsRendomAnswerOrder;
                    db.Questions.Update(Question1);
                    db.SaveChanges();
                    return true;
                }
                return false;

            }
        }

        public bool DeleteQuestion(string qusGuid)
        {
            using (TestDbContext db = new TestDbContext())
            {
                Question QuestionToDelete = db.Questions.SingleOrDefault(c => c.QuestionGuid == qusGuid);
                if(QuestionToDelete != null)
                { 
                db.Questions.Remove(QuestionToDelete);
                db.SaveChanges();
                return true;
                }
                return false;

            }
        }



    }
}
