using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TestAdministration.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        HttpRequestor httpRequestor;

        public Register()
        {
            InitializeComponent();
            httpRequestor = new HttpRequestor();
            Register_Btn.Click += RegisterBtn;
        }

        private async void RegisterBtn(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(userName.Text) || string.IsNullOrEmpty(userId.Text)  || string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(usertypeCmb.Text))
            {
                MessageBox.Show("מלא את כל השדות");
            }
            else
            {
                Models.User user = new Models.User(usertypeCmb.Text, userName.Text, Int32.Parse(userId.Text), int.Parse(password.Text));
   
                if (await httpRequestor.RequestRegisterAsync(user) == true)
                {
                    MessageBox.Show("משתמש כבר קיים במערכת ");
                    usertypeCmb.Text = "";
                    userName.Text = "";
                    userId.Text = "";
                    password.Text = "";
                }
                else
                {
                    if (await httpRequestor.InsertUserAsync(user) == true)
                    { 
                        MessageBox.Show("משתמש נרשם בהצלחה!");
                       this.Close();
                    }
                    else
                    {
                        MessageBox.Show(" הרשמה נכשלה");
                    }
                }
                

            }

           
        }


        //regular expression matches the text input only if it is not a number, and then it is not enter into the textboX
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
