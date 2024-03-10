using System;
using System.Windows;

namespace TestAdministration.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
         HttpRequestor httpRequestor;


        public Login()
        {
            InitializeComponent();
            httpRequestor = new HttpRequestor();
            login_Btn.Click += loginBtn;


        }

        private async void loginBtn(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrEmpty(userName.Text) ||  string.IsNullOrEmpty(password.Text) )
            {
                MessageBox.Show("מלא את כל השדות");
            }
            else
            {
                Models.UserLogin userLogin1 = new Models.UserLogin(userName.Text, Int32.Parse(password.Text));


                Models.User user1 = await httpRequestor.RequestLoginAsync(userLogin1);
                if (user1 == null)
                {
                    MessageBox.Show("שם משתמש או סיסמא שגויים, נסה שוב ");
                    
                    userName.Text = "";
                    password.Text = "";
                }
                else
                {
                    MessageBox.Show("משתמש התחבר בהצלחה");
                    this.Close();

                    if (user1.UserType == "סטודנט")
                    {
                        
                        this.Close();
                        StudentWindow S1 = new StudentWindow(user1.UserId);
                        S1.ShowDialog();
                    }
                    else
                    {
                        this.Close();
                        Teacherwindow T1 = new Teacherwindow();
                        T1.ShowDialog();
                    }
                }

            }
        }
    }
}
