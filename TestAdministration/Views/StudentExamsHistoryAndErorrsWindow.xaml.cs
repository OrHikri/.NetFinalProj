using System.Windows;
using System.Windows.Controls;


namespace TestAdministration.Views
{
    /// <summary>
    /// Interaction logic for StudentExamsHistoryAndErorrsWindow.xaml
    /// </summary>
    public partial class StudentExamsHistoryAndErorrsWindow : Window
    {
        HttpRequestor httpRequestor;

        private int StudentId;
        public StudentExamsHistoryAndErorrsWindow(int StudentId)
        {
            InitializeComponent();
            httpRequestor = new HttpRequestor();
            this.StudentId = StudentId;
            ExamHistoryList.SelectionChanged += ExamHistoryList_SelectionChanged;
            ErrorsList.SelectionChanged += ErrorsList_SelectionChanged;

        }

        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            ExamHistoryList.ItemsSource = await httpRequestor.GetAllGradesById(StudentId);
        }
            private async void ExamHistoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ExamHistoryList.SelectedItem is Models.Grade selectedGarde)
            {
                ExamIdTxt.Text = selectedGarde.ExamId.ToString();
                GradeTxt.Text = selectedGarde.ExamGrade.ToString();
                ExecutionDate.Text = selectedGarde.ExecutionDate.ToString();
                ErrorsList.ItemsSource = await httpRequestor.GetAllMyErrorsInTest(StudentId, int.Parse(ExamIdTxt.Text));

            }
            else
            {
                ExamIdTxt.Text = string.Empty;
                GradeTxt.Text = string.Empty;
                ExecutionDate.Text = string.Empty;
            }
        }

        private async void ErrorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ErrorsList.SelectedItem is Models.Error selectedError)
            {
                SelectesdAnsTxt.Text = selectedError.SelectedAnswer.ToString();
                CorrectAnsTxt.Text = selectedError.CorrectAnswer.ToString();
            }
            else {
                SelectesdAnsTxt.Text= string.Empty;
                CorrectAnsTxt.Text = string.Empty;  
            }
        }


    }
}

