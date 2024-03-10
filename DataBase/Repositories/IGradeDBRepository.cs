using DataBase.Models;

namespace DataBase.Repositories
{
    public interface IGradeDBRepository
    {
        List<Grade> GetAllGrades();

        List<Grade> GetAllGradesById(int StudentId);

        double GetAverageGrade();

        void InsertGrade(Grade newGrade);

        bool UpdateGrade(Grade GradeToUpdate);

        bool DeleteGrade(int id);


    }
}
