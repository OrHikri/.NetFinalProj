using System.Windows;
using TestAdministration.Views;

namespace TestAdministration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             Program.Main();
        }



 

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register R1 = new Register();   
            R1.ShowDialog();
         

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login L1 = new Login(); 
            L1.ShowDialog();
            this.Close();
        }
    }
}
