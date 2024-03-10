namespace DataBase.Models
{
    public class Error
    {
        public Error()
        {

        }


        //Property
        public int Id { get; set; }

        public int StudentID { get; set; }

        public int ExamId { get; set; }

        public string QuestionText { get; set; }

        public string CorrectAnswer { get; set; }

        public string SelectedAnswer { get; set; }

        public Error(string studentId, string examId, string questionText, string correctAnswer, string selectedAnswer)
        {
            StudentID = Int32.Parse(studentId);
            ExamId = Int32.Parse(examId);
            QuestionText = questionText;
            CorrectAnswer = correctAnswer;
            SelectedAnswer = selectedAnswer;

        }

        public override string ToString()
        {
            return QuestionText + "-" + CorrectAnswer + "-" + SelectedAnswer;

        }
    }
}
