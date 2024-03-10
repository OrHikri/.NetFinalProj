using DataBase.Models;


namespace DataBase.Repositories

{
    public interface IQuestinDBRepository
    {
        bool DeleteAllQuestionsByTestGuid(string Examid);

       
        //Task<List<Question>> GetAllAsync();
        List<Question> GetAllQuestionsByTestGuid(string TestGuid);
        void InsertQuestion(Question newQuestin);
        bool UpdateQuestion(Question questinToUpdate);

        bool DeleteQuestion(string qusid);
        public Question FindQuestion(string examid);

    }
}
