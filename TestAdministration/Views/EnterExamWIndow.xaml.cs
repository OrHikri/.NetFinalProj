using System;
using System.Windows;
using System.Windows.Controls;

namespace TestAdministration
{
    /// <summary>
    /// Interaction logic for EnterExamWIndow.xaml
    /// </summary>
    public partial class EnterExamWIndow : Window
    {

        HttpRequestor httpRequestor;


        public double ExamTotalTime = 0;

        public bool IsRendomOrder;

        public DateTime Examdate;

        public string TestGuid;

        private int StudentId { get; set; }





        public EnterExamWIndow(int StudentId)
        {
            InitializeComponent();
            httpRequestor = new HttpRequestor();
            listBoxTests.SelectionChanged += listBoxTests_SelectionChanged;
            this.StudentId = StudentId;

        }

        //use load event to add items or changes before load Window
        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            listBoxTests.ItemsSource = await httpRequestor.GetAllTestsAsync();
        }
        public async void listBoxTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            if (this.listBoxTests.SelectedItem is Models.Test selectedTest)
            {
                TestName.Text = selectedTest.Name;
                TestId.Text = selectedTest.Id.ToString();
                ExamTotalTime = selectedTest.TotalTime;
                IsRendomOrder = selectedTest.IsRendomOrder;
                Examdate = selectedTest.Date;
                TestGuid = selectedTest.TestGuid;
                btnrunexam.IsEnabled = true;

            }
            else
            {
                TestName.Text = string.Empty;
                TestId.Text = string.Empty;
            }

           
        }




        private void btnRunExam_Click(object sender, RoutedEventArgs e)
        {
            var Date = DateTime.Now;
           if (Examdate != Date.Date)
            {
                MessageBox.Show("תאריך הבחינה לא היום");
            }
            else
            {
                      
            RunExamWindow R1 = new RunExamWindow(TestGuid, TestId.Text, TestName.Text, StudentId, ExamTotalTime, IsRendomOrder);
            R1.ShowDialog();
            }
        }


    }
}
    
 

