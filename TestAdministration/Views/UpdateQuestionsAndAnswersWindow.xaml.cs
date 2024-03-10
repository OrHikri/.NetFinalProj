using TestAdministration.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TestAdministration.Views
{
    /// <summary>
    /// Interaction logic for UpdateQuestionsAndAnswersWindow.xaml
    /// </summary>
    public partial class UpdateQuestionsAndAnswersWindow : Window
    {

        HttpRequestor httpRequestor;

        string QusGuid = string.Empty;

        string TestGuid = string.Empty;

        int QusId;


        List<Models.Answer> ListAnswers = new List<Models.Answer>();

        //fields to check in the UpadteQusAndAns_btn method if the first value changed

        string TxtQbase;
        bool MycheckBoxBase;
        string TxtAnswer1Base;
        string TxtAnswer2Base;
        string TxtAnswer3Base;
        string TxtIndxCrctAnsBase;
         

        public UpdateQuestionsAndAnswersWindow(string TestGuid)
        {
            InitializeComponent();
            httpRequestor = new HttpRequestor();
           // listBoxQuestions.SelectionChanged += listBoxQuestions_SelectionChanged;
            DeleteQusAndAns_btn.Click += DeleteQusAndAns_btn_Click;
            UpadteQusAndAns_btn.Click += UpadteQusAndAns_btn_Click;

                     
            this.TestGuid = TestGuid;
        }

        //use load event to add items or changes before load Window
        private async void OnLoad(object sender, RoutedEventArgs e)
        {
         listBoxQuestions.ItemsSource = await httpRequestor.GetAllQuestionsByTestGuid(TestGuid);
         
        }

            public async void listBoxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.listBoxQuestions.SelectedItem is Question selectedQuestion)
            {
                txtQ.Text = selectedQuestion.TextQuestion;
                MyCheckBox.IsChecked = selectedQuestion.IsRendomAnswerOrder;
                QusId = selectedQuestion.Id;
                QusGuid = selectedQuestion.QuestionGuid;
                 ListAnswers = await httpRequestor.GetAllAnswersByQusGuid(QusGuid);
                if (ListAnswers != null)
                {
                    txtAnswer1.Text = ListAnswers[0].TextAnswer;
                    txtAnswer2.Text = ListAnswers[1].TextAnswer;
                    txtAnswer3.Text = ListAnswers[2].TextAnswer;
                    txtIndxCrctAns.Text = ListAnswers[0].CorrectAnswerIndex.ToString();

                    //fields to check in the UpadteQusAndAns_btn method if the first value changed
                    TxtQbase = selectedQuestion.TextQuestion;
                    MycheckBoxBase = selectedQuestion.IsRendomAnswerOrder;
                    TxtAnswer1Base = ListAnswers[0].TextAnswer;
                    TxtAnswer2Base = ListAnswers[1].TextAnswer;
                    TxtAnswer3Base = ListAnswers[2].TextAnswer;
                    TxtIndxCrctAnsBase = ListAnswers[0].CorrectAnswerIndex.ToString();
                }
                else
                {
                    MessageBox.Show("לא נמצאו תשובות לשאלה שנבחרה");
                }

            }
        }


        public async void DeleteQusAndAns_btn_Click(object sender, RoutedEventArgs e)
        {
            if (await httpRequestor.DeleteAllAnswersAsync(QusGuid) == true && await httpRequestor.DeleteQuestionAsync(QusGuid) == true)
            {
                txtQ.Clear();
                MyCheckBox.IsChecked = false;
                txtAnswer1.Clear();
                txtAnswer2.Clear();
                txtAnswer3.Clear();
                txtIndxCrctAns.Clear();
                IEditableCollectionView items = listBoxQuestions.Items; //Cast to interface
                if (items.CanRemove)
                {
                    items.Remove(listBoxQuestions.SelectedItem);
                }
                 MessageBox.Show("מחיקה בוצעה בהצלחה");
            }
            else
            {
                    MessageBox.Show("לא בוצעה מחיקה");
            }
        }


        

        private async void UpadteQusAndAns_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if (TxtQbase != txtQ.Text || MycheckBoxBase != MyCheckBox.IsChecked)
            {
                Models.Question Q1 = new Models.Question( QusId ,QusGuid,TestGuid, txtQ.Text, MyCheckBox.IsChecked);
                await httpRequestor.UpdateQuestionAsync(Q1);
                MessageBox.Show(" בוצע עדכון בהצלחה");
            }

            if(TxtAnswer1Base != txtAnswer1.Text)
            {
                 ListAnswers[0].TextAnswer = txtAnswer1.Text ;
                await httpRequestor.UpdateAnserAsync(ListAnswers[0]);
                MessageBox.Show(" בוצע עדכון בהצלחה");
            }

            if (TxtAnswer2Base != txtAnswer2.Text)
            {
                ListAnswers[1].TextAnswer = txtAnswer2.Text;
                await httpRequestor.UpdateAnserAsync(ListAnswers[1]);
                MessageBox.Show(" בוצע עדכון בהצלחה");
            }

            if (TxtAnswer2Base != txtAnswer2.Text)
            {
                ListAnswers[2].TextAnswer = txtAnswer3.Text;
                await httpRequestor.UpdateAnserAsync(ListAnswers[2]);
                MessageBox.Show(" בוצע עדכון בהצלחה");
            }

            if (TxtIndxCrctAnsBase != txtIndxCrctAns.Text)
            {
                ListAnswers[0].CorrectAnswerIndex = ListAnswers[1].CorrectAnswerIndex = ListAnswers[2].CorrectAnswerIndex = int.Parse(txtIndxCrctAns.Text);
                await httpRequestor.UpdateAnserAsync(ListAnswers[0]);
                await httpRequestor.UpdateAnserAsync(ListAnswers[1]);
                await httpRequestor.UpdateAnserAsync(ListAnswers[2]);
                MessageBox.Show(" בוצע עדכון בהצלחה");
            }
            else
            { 
            MessageBox.Show("לא בוצע עדכון");
            }
        }

    }



}

