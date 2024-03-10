using System;

namespace TestAdministration.Models
{
    public class Grade
    {
      
        //Property
        public int Id { get; set; }

        public int StudentID { get; set; }

        public int ExamId { get; set; }

        public double ExamGrade { get; set; }

        public DateTime ExecutionDate { get; set; }

        public Grade()
        {

        }


        public Grade(int studentId, int examId, double examGrade, DateTime executionDate)
        {
            StudentID =studentId;
            ExamId = examId;
            ExamGrade = examGrade;
            ExecutionDate = executionDate;

        }



        public override string ToString()
        {
            return " Test: " +  ExamId  + " -Grade: " + ExamGrade + "\r\n -ExecutionDate: " + ExecutionDate;

        }


    }
}
