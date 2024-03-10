using System;

namespace TestAdministration.Models
{
    public class Question
    {

        //Property
        public int Id { get; set; }
        public string QuestionGuid { get; set; }
        public string TestGuid { get; set; }

        public string TextQuestion { get; set; }

        public bool IsRendomAnswerOrder { get; set; }



        public Question()
        {


        }


        public Question(string testguid, string textQuestion, bool? isRendomAnswerOrder)
        {
            QuestionGuid = Guid.NewGuid().ToString();
            TestGuid = testguid;
            TextQuestion = textQuestion;
            IsRendomAnswerOrder = Convert.ToBoolean(isRendomAnswerOrder);
        }


        public Question(string questionguid, string testguid, string textQuestion, bool? isRendomAnswerOrder)
        {
            QuestionGuid = questionguid;
            TestGuid = testguid;
            TextQuestion = textQuestion;
            IsRendomAnswerOrder = Convert.ToBoolean(isRendomAnswerOrder);
        }

        public Question(int id,string questionguid,string testguid, string textQuestion, bool? isRendomAnswerOrder)
        {
            Id = id;
            QuestionGuid = questionguid;
            TestGuid = testguid;
            TextQuestion = textQuestion;
            IsRendomAnswerOrder = Convert.ToBoolean(isRendomAnswerOrder);
        }

        public override string ToString()
        {
            return TextQuestion;
        }

    }

}

