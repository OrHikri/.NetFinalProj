using DataBase.Models;

namespace DataBase.Repositories
{
    public interface IAnswerDbRepository
    {
     
        List<Answer> GetAllAnswersByQusGuid(string QusGuid);
        void InsertAnswer(Answer newAnswer);
        bool UpdateAnswer(Answer AnswerToUpdate);

        bool DeleteAllAnswersByQusGuid(string QusGuid);

    }
}

