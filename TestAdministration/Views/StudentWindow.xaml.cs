using System.Windows;
using TestAdministration.Views;

namespace TestAdministration
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        private  int StudentId;

        
        public StudentWindow(int StudentId)
        {
            InitializeComponent();
            this.StudentId = StudentId;
        }

        private void RunExam(object sender, RoutedEventArgs e)
        {
                
                EnterExamWIndow Exm1 = new EnterExamWIndow(StudentId);
                Exm1.ShowDialog();
            
        }

        private void ExamHistoryAndErorrs(object sender, RoutedEventArgs e)
        {
            StudentExamsHistoryAndErorrsWindow Exm1 = new StudentExamsHistoryAndErorrsWindow(StudentId);
            Exm1.ShowDialog();
        }
    }
}
