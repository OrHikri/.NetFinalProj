using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestAdministration.Models;
using TestAdministration.Views;


namespace TestAdministration
{
    /// <summary>
    /// Interaction logic for FindAndUpdateExam.xaml
    /// </summary>
    public partial class FindAndUpdateExamWindow : Window
    {
        HttpRequestor httpRequestor;

        string Q1ExamGuid = null;

        string TestGuid = null;

        // field for checking if first value had been updata in UpdateTest_btn_Click metod 
        string testNameBase;
        string testIdBase;
        string datePicker1Base;
        string testTeacherNameBase;
        string testHHBase;
        string testMMBase;
        string testTotalTimeBase;
        bool McCheckBoBase;

        public FindAndUpdateExamWindow()
        {
            InitializeComponent();
            httpRequestor = new HttpRequestor();
            UpdateTestbtn.Click += UpdateTest_btn_Click;
            DeleteTestbtn.Click += DeleteTest_btn_Click;
            SearchTxT.TextChanged += textChangedEventHandler;
        }

        //private async void OnLoad(object sender, RoutedEventArgs e)
        //{
            
        //}

            public async void textChangedEventHandler(object sender, TextChangedEventArgs e)
            {   
            
            listBoxTests.ItemsSource = await httpRequestor.GetAllTestsByTestGuidOrName(SearchTxT.Text);
             }

        public void listBoxTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.listBoxTests.SelectedItem is Test selectedTest)
            {
                testName.Text = selectedTest.Name;
                testId.Text = selectedTest.Id.ToString();
                datePicker1.Text = selectedTest.Date.ToString();
                testTeacherName.Text = selectedTest.TeacherName;
                testHH.Text = selectedTest.StartTime.Hours.ToString();
                testMM.Text = selectedTest.StartTime.Minutes.ToString();
                testTotalTime.Text = selectedTest.TotalTime.ToString();
                McCheckBox.IsChecked = selectedTest.IsRendomOrder;
                TestGuid = selectedTest.TestGuid;

                // field for checking if had been updata in UpdateTest_btn_Clic metod 
                 testNameBase = selectedTest.Name;
                 testIdBase = selectedTest.Id.ToString();
                 datePicker1Base = selectedTest.Date.ToString();
                 testTeacherNameBase = selectedTest.TeacherName;
                 testHHBase = selectedTest.StartTime.Hours.ToString();
                 testMMBase = selectedTest.StartTime.Minutes.ToString();
                 testTotalTimeBase = selectedTest.TotalTime.ToString();
                 McCheckBoBase = selectedTest.IsRendomOrder;

            }
        }

        public async void  UpdateTest_btn_Click(object sender, RoutedEventArgs e)
        { if (testName.Text != testNameBase || testId.Text != testIdBase || datePicker1.Text != datePicker1Base ||
                testTeacherName.Text != testTeacherNameBase || testHH.Text != testHHBase || testMM.Text != testMMBase ||
               testTotalTimeBase != testTotalTime.Text || McCheckBox.IsChecked != McCheckBoBase)

            {
            Models.Test T1 = new Models.Test(int.Parse(testId.Text), TestGuid, testName.Text, datePicker1.Text, testTeacherName.Text, (testHH.Text + txtStartTime.Text + testMM.Text), testTotalTime.Text, McCheckBox.IsChecked);
            
            if (await httpRequestor.UpdateTestAsync(T1) == true)
            {
                MessageBox.Show("עודכן בהצלחה");
            }
            else
            {
                MessageBox.Show("לא בוצע עדכון");

            }
         }

        }
        
        
        public async void DeleteTest_btn_Click(object sender, RoutedEventArgs e)
        {

            if (await httpRequestor.DeleteTestAsync(int.Parse(testId.Text)) == true)
            {
                List<Models.Question> Q1 = await httpRequestor.GetAllQuestionsByTestGuid(TestGuid);
                
                foreach (Models.Question q in Q1)
                {
                    await httpRequestor.DeleteAllAnswersAsync(q.QuestionGuid);
                }
                await httpRequestor.DeleteAllQuestionsAsync(TestGuid);

                testName.Clear();
                testId.Clear();
                datePicker1.Text = string.Empty;
                testTeacherName.Clear();
                testHH.Clear();
                testMM.Clear();
                testTotalTime.Clear();
                McCheckBox.IsChecked = false;

                MessageBox.Show("נמחק בהצלחה");
            }
            else
            {
                MessageBox.Show("לא בוצעה מחיקה");

            }
        }

        public void UpadteQusAndAns_btn_Click(object sender, RoutedEventArgs e)
        {
            UpdateQuestionsAndAnswersWindow U1 = new UpdateQuestionsAndAnswersWindow(TestGuid);
            U1.Show();
        }



        // Validating a Textbox field for only numeric input
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }


}
}
