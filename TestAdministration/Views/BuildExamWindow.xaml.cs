using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestAdministration.Models;

namespace TestAdministration
{
    public partial class BuildExamWindow : Window
    {
        HttpRequestor httpRequestor;
        private List<Test> testlist;
        private List<Question> questionlist;
        private List<Answer> answerlist;
        public int count = 1;
        public new Test T1;
        public BuildExamWindow()
        {
            InitializeComponent();
            txtId.Text = Guid.NewGuid().ToString();
            httpRequestor = new HttpRequestor();
            testlist = new List<Test>();
            questionlist = new List<Question>();
            answerlist = new List<Answer>();
            SaveTestOnServer_Btn.Click += SaveTestOnServerBtn;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



        }

        public void Button_SaveExamToFile(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(datePicker1.Text) || String.IsNullOrEmpty(txtTeacherName.Text) ||
             String.IsNullOrEmpty(txtHH.Text) || String.IsNullOrEmpty(txtMM.Text) || String.IsNullOrEmpty(txtTotalTime.Text))
            {
                MessageBox.Show("פרטי מבחן לא מלאים, נא מלא את השדות החסרים");
            }

            else
            {
                T1 = new Test(txtId.Text, txtName.Text, datePicker1.Text, txtTeacherName.Text, (txtHH.Text + txtStartTime.Text + txtMM.Text), txtTotalTime.Text, McCheckBox.IsChecked);
                testlist.Add(T1);


                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "שמירת קובץ";
                saveFileDialog.Filter = "JSON Files|*.json";
                if (Directory.Exists("AppData"))
                {
                    saveFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "AppData");  // Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                else
                {
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePathSelected = saveFileDialog.FileName;

                    //Options For Indentation
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    //--Convert List to Json String
                    var testListJson = JsonSerializer.Serialize(testlist, options);
                    //Save JSON Text To File in  BinDebug under AppData Directory
                    File.WriteAllText(filePathSelected, testListJson);
                }
                MessageBox.Show("פרטי מבחן נשמרו בקובץ");
                txtQ.IsEnabled = true;
                txtanswer1.IsEnabled = true;
                txtanswer2.IsEnabled = true;
                txtanswer3.IsEnabled = true;
                txtIndxCrctAns.IsEnabled = true;
                MyCheckBox.IsEnabled = true;
                AddQuestionBtn.IsEnabled = true;
            }
        }

        public void Button_AddQuestion(object sender, RoutedEventArgs e)
        {
            Question Q1 = new Question(T1.TestGuid, txtQ.Text, MyCheckBox.IsChecked);
            questionlist.Add(Q1);
            count++;
            Answer A1 = new Answer(1, Q1.QuestionGuid, txtanswer1.Text, txtIndxCrctAns.Text);
            answerlist.Add(A1);
            Answer A2 = new Answer(2, Q1.QuestionGuid, txtanswer2.Text, txtIndxCrctAns.Text);
            answerlist.Add(A2);
            Answer A3 = new Answer(3, Q1.QuestionGuid, txtanswer3.Text, txtIndxCrctAns.Text);
            answerlist.Add(A3);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "שמירת קובץ";
            saveFileDialog1.Filter = "JSON Files|*.json";
            if (Directory.Exists("AppData"))
            {
                saveFileDialog1.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "AppData");  
            }
            else
            {
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            if (saveFileDialog1.ShowDialog() == true)
            {
                string filePathSelected = saveFileDialog1.FileName;

                //Options For Indentation
                var options1 = new JsonSerializerOptions { WriteIndented = true };
                //Convert List to Json String
                var questionlistJson = JsonSerializer.Serialize(questionlist, options1);
                questionlistJson = questionlistJson.Replace("[\r\n  ", "},\r\n  ");
                questionlistJson = questionlistJson.Replace("\r\n]", ",\r\n  ");
                var answerlistJson = JsonSerializer.Serialize(answerlist, options1);
                answerlistJson = answerlistJson.Replace("[\r\n ", "");
                //Save JSON Text To File in  BinDebug under AppData Directory
                var json = File.ReadAllText(filePathSelected);
                json = json.Replace("}\r\n]", "");
                File.WriteAllText(filePathSelected, json);
                File.AppendAllText(filePathSelected, questionlistJson);
                File.AppendAllText(filePathSelected, answerlistJson);
                MessageBox.Show("פרטי שאלה ותשובות נשמרו בקובץ");

                questionlist.Clear();
                answerlist.Clear();
                txtQ.Clear();
                txtanswer1.Clear();
                txtanswer2.Clear();
                txtanswer3.Clear();
                txtIndxCrctAns.Clear();

            }
        }

        public async void SaveTestOnServerBtn(object sender, RoutedEventArgs e)
        {
            List<Test> JsonTestList = new List<Test>();
            List<Question> JsonQuestionList = new List<Question>();
            List<Answer> JsonAnswerList = new List<Answer>();

            //--Open Dialog
            OpenFileDialog dialog = new OpenFileDialog();
            //--Open Desktop
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //--Open JSON Files Only
            dialog.Filter = "JSON Files|*.json;*.JSON;*.txt";
            if (Directory.Exists("AppData"))
            {
                dialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "AppData");  
            }
            else
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            //Display Dialog
            if (dialog.ShowDialog() == true)
            {
                //--read User Selected File
                string jsonFullPath = dialog.FileName;
                string testsText = File.ReadAllText(jsonFullPath);

                JsonTestList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Test>>(testsText);

                JsonQuestionList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Question>>(testsText);

                JsonAnswerList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Answer>>(testsText);

            }

            if (JsonTestList != null && JsonQuestionList != null && JsonAnswerList != null)
            {
                Models.Test T1 = new Test();
                Models.Question Q1 = new Question();
                Models.Answer A1 = new Answer();
                foreach (Test t in JsonTestList)
                {
                    if (t.Name != null)
                    {

                        T1 = new Models.Test(t.TestGuid, t.Name, t.Date.ToString(), t.TeacherName, t.StartTime.ToString(), t.TotalTime.ToString(), t.IsRendomOrder);
                        httpRequestor.InserTestAsync(T1);

                        foreach (Question q in JsonQuestionList)
                        {
                            if (q.TextQuestion != null)
                            {
                                Q1 = new Models.Question(q.QuestionGuid, q.TestGuid, q.TextQuestion, q.IsRendomAnswerOrder);
                                httpRequestor.InsertQuestionAsync(Q1);


                            }
                        }
                    }

                }

                foreach (Answer a in JsonAnswerList)
                {
                    if (a.TextAnswer != null)
                    {
                        A1 = new Models.Answer(a.QuestionGuid, a.TextAnswer, a.CorrectAnswerIndex.ToString());
                        httpRequestor.InserAnswerAsync(A1);
                    }
                }



                await Task.Run(() => MessageBox.Show("המבחן נשמר בהצלחה"));

            }


            else if (JsonTestList == null)
            {
                MessageBox.Show("לא קיימים פרטי מבחן בקובץ ");
            }
            else
            {
                MessageBox.Show("קיים בקובץ פרט מבחן ללא שאלות ");

            }


        }


        // Validating a Textbox field for only numeric input
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}




