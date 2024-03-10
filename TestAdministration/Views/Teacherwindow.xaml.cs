using System.Windows;

namespace TestAdministration.Views
{
    /// <summary>
    /// Interaction logic for Teacherwindow.xaml
    /// </summary>
    public partial class Teacherwindow : Window
    {
        public Teacherwindow()
        {
            InitializeComponent();
        }

        private void BuildExam_Click(object sender, RoutedEventArgs e)
        {
            BuildExamWindow B1 = new BuildExamWindow();
            B1.ShowDialog();
        }

        private void StudentsExamsHistoryandStatistics_Click(object sender, RoutedEventArgs e)
        {
            AllExamsHistoryandStatistics S1 = new AllExamsHistoryandStatistics();
            S1.ShowDialog();
        }

        private void FindAndUpdateExam_Click(object sender, RoutedEventArgs e)
        {
            FindAndUpdateExamWindow F1 = new FindAndUpdateExamWindow();
            F1.ShowDialog();
        }
    }
}
