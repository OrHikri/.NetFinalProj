using DataBase.Models;

namespace DataBase.Repositories
{
    public interface IErrorDbRepository
    {
        List<Error> GetAllErrors();

        List<Error> GetAllMyErrorsInTest(int studentId, int ExamId);
        void InsertError(List<Error> ErrorsList);

        bool UpdateError(Error ErrorToUpdate);

        bool DeleteError(int id);




    }
}