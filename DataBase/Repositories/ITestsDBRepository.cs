using DataBase.Models;

namespace DataBase.Repositories

{
    public interface ITestsDBRepository
    {
        List<Test> GetAllTests();
        List<Test> GetAllTestsByTestGuidOrName(string GuidIDOrName);
        void InsertTest(Test newTest);
        public bool UpdateTest(Test TestToUpdate);

        public bool DeleteTest(int Id);

    }
}



