using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TestAdministration.Views
{
    /// <summary>
    /// Interaction logic for AllExamsHistoryandStatistics.xaml
    /// </summary>
    public partial class AllExamsHistoryandStatistics : Window
    {

        string UserType = "סטודנט";

        HttpRequestor httpRequestor;
        public AllExamsHistoryandStatistics()
        {
            InitializeComponent();
            httpRequestor = new HttpRequestor();
        }

        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            studentsList.ItemsSource = await httpRequestor.GetAllUsersByType(UserType);
            AvgAllExamsTxt.Text = String.Format("{0:0.00}", (await httpRequestor.GetAllGradesAsync()).Average(item => item.ExamGrade));
            studentsList.SelectionChanged += studentsList_SelectionChanged;

        }

        private async void studentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (this.studentsList.SelectedItem is Models.User selectedUser)
            {
                int Studentid = selectedUser.UserId;
                studentNameTxt.Text = selectedUser.UserName;
                ExamsList.ItemsSource = await httpRequestor.GetAllGradesById(Studentid);
                AvgExamsForStudent.Text = String.Format("{0:0.00}", (await httpRequestor.GetAllGradesById(Studentid)).Select(x => x.ExamGrade).Average());


            }
        }


    }
}
