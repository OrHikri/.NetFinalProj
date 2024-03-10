namespace TestAdministration.Models
{
    public class Answer
    {


        //Property

        public int Id { get; set; }

        public string TextAnswer { get; set; }

        public string QuestionGuid { get; set; }

        public int CorrectAnswerIndex { get; set; }


        public Answer()
        {

        }
        public Answer(int id, string questionguid, string? textAnswer, string correctAnswerIndex)
        {
            Id = id;
            QuestionGuid = questionguid;
            CorrectAnswerIndex = int.Parse(correctAnswerIndex);
            TextAnswer = textAnswer;

        }

        public Answer(string questionguid, string? textAnswer, string correctAnswerIndex)
        {
            QuestionGuid = questionguid;
            CorrectAnswerIndex = int.Parse(correctAnswerIndex);
            TextAnswer = textAnswer;

        }
    }
}

