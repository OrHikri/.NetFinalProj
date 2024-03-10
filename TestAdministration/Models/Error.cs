namespace TestAdministration.Models
{
    public class Error
    {

        //Property
        public int Id { get; set; }

        public int StudentID { get; set; }

        public int ExamId { get; set; }

        public string QuestionText { get; set; }

        public string CorrectAnswer { get; set; }

        public string SelectedAnswer { get; set; }


        public Error()
        {

        }


        public Error(int studentId, int examId, string questionText, string correctAnswer, string selectedAnswer)
        {
            StudentID = studentId;
            ExamId = examId;
            QuestionText = questionText;
            CorrectAnswer = correctAnswer;
            SelectedAnswer = selectedAnswer;

        }

        public override string ToString()
        {
            return QuestionText + " - " + CorrectAnswer + "\r\n - " + SelectedAnswer;

        }
    }
}
