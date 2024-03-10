using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using XAct;

namespace TestAdministration
{
    /// <summary>
    /// Interaction logic for RunExamWindow.xaml
    /// </summary>
    public partial class RunExamWindow : Window
    {
        HttpRequestor httpRequestor;

        public int _time;

        public DispatcherTimer Timer;

        public Random randomiser = new Random();

        public int StudentId;

        public string QusGuid;

        public string TestGuid;

        public bool IsRendomOrder;

        List<Models.Answer> TempAnswersList = new List<Models.Answer>();

        List<Models.Answer> AllCorrectAnswersList = new List<Models.Answer>();

        List<Models.Question> TempQusList = new List<Models.Question>();

        Models.Answer CorrectAnswer;



        public RunExamWindow(string TestGuid, string idFromTextId, string NameFromTextName, int StudentId, double ExamTotalTime, bool IsRendomOrder)
        {

            InitializeComponent();
            httpRequestor = new HttpRequestor();
            this.StudentId = StudentId;
            this.IsRendomOrder = IsRendomOrder;
            this.TestGuid = TestGuid;

            TestId.Text = idFromTextId;
            TestName.Text = NameFromTextName;
            listBoxQuestions.Items.Clear();
            listBoxQuestions.SelectionChanged += listBoxQuestions_SelectionChanged;
            finishTestBtn.Click += finishTestBtn_Click;

            //Timer
            _time = (int)(ExamTotalTime * 3600);
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1); // will 'tick' once every second
            Timer.Tick += Timer_Tick;
            Timer.Start();



        }


        //use load event to add items or changes before load Window
        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            listBoxQuestions.ItemsSource = null;


            // check if the order of questions is random
            if (IsRendomOrder == true)
            {
                List<Models.Question> TempQusLIst = new List<Models.Question>();
                TempQusLIst = await httpRequestor.GetAllQuestionsByTestGuid(TestGuid);
                int n = TempQusLIst.Count;
                while (n > 1)
                {
                    n--;
                    int k = randomiser.Next(n + 1);
                    TestAdministration.Models.Question value = TempQusLIst[k];
                    TempQusLIst[k] = TempQusLIst[n];
                    TempQusLIst[n] = value;

                }
                listBoxQuestions.ItemsSource = TempQusLIst;


            }
            else
            {
                listBoxQuestions.ItemsSource = await httpRequestor.GetAllQuestionsByTestGuid(TestGuid);


            }
            TotalQus.Text = listBoxQuestions.Items.Count.ToString();

        }

        public async void listBoxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TempAnswersList.Clear();

            if (listBoxQuestions.SelectedItem is Models.Question selectedQuestion)
            {
                txtQustion.Text = selectedQuestion.TextQuestion;
                btnPreviousQue.IsEnabled = true;
                btnNextQue.IsEnabled = true;

                // For each question save its correct answer according to the correctansweindex
                TempQusList.Add(selectedQuestion);
                QusGuid = selectedQuestion.QuestionGuid;
                TempAnswersList = await httpRequestor.GetAllAnswersByQusGuid(QusGuid);
                int CorrectAnswerIndex = TempAnswersList[0].CorrectAnswerIndex - 1;
                CorrectAnswer = TempAnswersList[CorrectAnswerIndex];
                if (AllCorrectAnswersList.IsNullOrEmpty())
                {
                    AllCorrectAnswersList.Add(CorrectAnswer);
                }
                else if (AllCorrectAnswersList.Exists(n => n.QuestionGuid != CorrectAnswer.QuestionGuid))
                {
                    AllCorrectAnswersList.Add(CorrectAnswer);
                }


                // check if the order of answers is random
                if (selectedQuestion.IsRendomAnswerOrder == true)
                {
                    int n = TempAnswersList.Count;
                    while (n > 1)
                    {
                        n--;
                        int k1 = randomiser.Next(n + 1);
                        TestAdministration.Models.Answer value1 = TempAnswersList[k1];
                        TempAnswersList[k1] = TempAnswersList[n];
                        TempAnswersList[n] = value1;

                    }

                    comboBox.Items[0] = TempAnswersList[0].TextAnswer;
                    comboBox.Items[1] = TempAnswersList[1].TextAnswer;
                    comboBox.Items[2] = TempAnswersList[2].TextAnswer;

                }
                else
                {
                    comboBox.Items[0] = TempAnswersList[0].TextAnswer;
                    comboBox.Items[1] = TempAnswersList[1].TextAnswer;
                    comboBox.Items[2] = TempAnswersList[2].TextAnswer;
                }
            }

            // If the question has already been answered put in the combobox the last selected answer of the question
            if (lastSelectedAnswerList.Exists(n => n.QuestionGuid == QusGuid))
            {
                comboBox.Text = lastSelectedAnswerList.Find(n => n.QuestionGuid == QusGuid).TextAnswer;
            }
        }


        private void btnPreviousQue_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxQuestions.SelectedIndex > 0)
            {
                listBoxQuestions.SelectedIndex = listBoxQuestions.SelectedIndex - 1;
                listBoxQuestions.Focus();
            }
        }

        private void btnNextQue_Click(object sender, RoutedEventArgs e)
        {

            if (listBoxQuestions.SelectedIndex < listBoxQuestions.Items.Count - 1)
            {
                listBoxQuestions.SelectedIndex = listBoxQuestions.SelectedIndex + 1;
            }
            if (listBoxQuestions.SelectedIndex == 0) { listBoxQuestions.Focus(); }

        }


        int counterOfAnsweredQus = 0;
        List<string> listFindItem = new List<string>();
        List<Models.Answer> lastSelectedAnswerList = new List<Models.Answer>();


        public async void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Count of answered questions
            if (comboBox.SelectedIndex > -1)

            {
                string currentQus = listBoxQuestions.SelectedItem.ToString();
                bool alreadyExist = listFindItem.Contains(currentQus);
                if (alreadyExist == false)
                {
                    listFindItem.Add(currentQus);
                    counterOfAnsweredQus++;
                    this.QusAnswered.Text = counterOfAnsweredQus.ToString();
                }
                if (counterOfAnsweredQus == listBoxQuestions.Items.Count)
                {
                    finishTestBtn.IsEnabled = true;
                }
            }


            // save in a list the last selected answer of each question 
            if (comboBox.SelectedIndex > -1)
            {

                if (lastSelectedAnswerList.Count == 0)
                {
                    lastSelectedAnswerList.Add(new Models.Answer() { TextAnswer = comboBox.SelectedItem.ToString(), QuestionGuid = QusGuid });
                }
                else if (lastSelectedAnswerList.Count > 0 && lastSelectedAnswerList.Exists(n => n.QuestionGuid != QusGuid))
                {
                    lastSelectedAnswerList.Add(new Models.Answer() { TextAnswer = comboBox.SelectedItem.ToString(), QuestionGuid = QusGuid });

                }
                else if (lastSelectedAnswerList.Count > 0 && lastSelectedAnswerList.Exists(n => n.QuestionGuid == QusGuid))
                {
                    lastSelectedAnswerList.Find(n => n.QuestionGuid == QusGuid).TextAnswer = comboBox.SelectedItem.ToString();

                }
            }

        }



        public async void finishTestBtn_Click(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            lastSelectedAnswerList = lastSelectedAnswerList.GroupBy(x => x.QuestionGuid).Select(x => x.Last()).ToList();
            AllCorrectAnswersList = AllCorrectAnswersList.GroupBy(x => x.QuestionGuid).Select(x => x.Last()).ToList();
            // check the answer in each question if it is correct and save the errors
            double grade = 0;
            int TotalQus = listBoxQuestions.Items.Count;
            int CorrectAns = listBoxQuestions.Items.Count;
            List<Models.Error> ErrorsList = new List<Models.Error>();

            foreach (Models.Answer lastAns in lastSelectedAnswerList)
            {
                foreach (Models.Answer crcAns in AllCorrectAnswersList)
                {

                    if (lastAns.TextAnswer != crcAns.TextAnswer && lastAns.QuestionGuid == crcAns.QuestionGuid)
                    {
                        Models.Question QuestionOfAnswer = TempQusList.FirstOrDefault(x => x.QuestionGuid == crcAns.QuestionGuid);
                        Models.Error newError = new Models.Error(StudentId, int.Parse(TestId.Text), QuestionOfAnswer.TextQuestion, crcAns.TextAnswer, lastAns.TextAnswer);                     
                        if (ErrorsList.Count == 0)
                        {
                          ErrorsList.Add(newError);
                        }
                        else if (ErrorsList.Contains(x => x.QuestionText != QuestionOfAnswer.TextQuestion))
                        {
                         ErrorsList.Add(newError);
                        }

                    }

                }


            }
            //https://stackoverflow.com/questions/18667633/how-can-i-use-async-with-foreach
            //List<T>.ForEach doesn't play particularly well with async 
            ErrorsList = ErrorsList.GroupBy(x => x.QuestionText).Select(x => x.Last()).ToList();
            //calculate grade and save in DB
            grade = (CorrectAns - ErrorsList.Count) * ((double)100 / (double)TotalQus);
            MessageBox.Show("ציונך במבחן:" + Math.Ceiling(grade));
            Models.Grade mygrade = new Models.Grade(StudentId, int.Parse(TestId.Text), Math.Ceiling(grade), DateTime.Now);
            await httpRequestor.InserGradeAsync(mygrade);
            // save errors in DB
            await httpRequestor.InsertErrorAsync(ErrorsList);



            this.Close();
        }


        public void Timer_Tick(object sender, EventArgs e)
        {
            if (_time > 0)
            {

                int h_time = _time / 3600;
                int m_time = (_time % 3600) / 60;
                int s_time = (_time % 3600) % 60;
                _time--;
                TBCountDown.Text = string.Format("{0:00}:{1:00}:{2:00}", h_time, m_time, s_time);
            }
            else
            {
                Timer.Stop();
                MessageBox.Show("זמן בחינה נגמר");
            }
        }
    }
}




