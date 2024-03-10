using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestAdministration
{
    public class HttpRequestor
    {
        readonly HttpClient client;

        public HttpRequestor() : this("https://localhost:7178")
        {
        }


        public HttpRequestor(string apiurl)
        {
            //01 Create HttpClient instance with base address
            client = new HttpClient();
            client.BaseAddress = new Uri(apiurl);
        }


		public async Task<Models.User> RequestLoginAsync(Models.UserLogin credentials)
        {
            try
            {
                //3.1) Convert credentials Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.UserLogin>(credentials);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3.2 Get response 
                using HttpResponseMessage response =
                    await client.PostAsync("api/Users/login", loginContent);

                response.EnsureSuccessStatusCode();//201

                //3.3 Get Json Data From Server Result
                Models.User userResponse =
                    await response.Content.ReadFromJsonAsync<Models.User>();

                return userResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> RequestRegisterAsync(Models.User credentials)
        {

            try
            {
                //1) Convert credentials Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.User>(credentials);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //2 Get response 
                using HttpResponseMessage response =
                    await client.PostAsync("api/Users/FindtUser", loginContent);

                response.EnsureSuccessStatusCode();//201

                //3 Get Json Data From Server Result and  parsing the string data to a boolean.

                var data = await response.Content.ReadAsStringAsync();

                return Boolean.Parse(data);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> InsertUserAsync(Models.User newuser)
        {
            try
            {
                //3.1) Convert credentials Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.User>(newuser);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3.2 Get response 
                using HttpResponseMessage response =
                    await client.PostAsync("api/Users/Insert", loginContent);

                response.EnsureSuccessStatusCode();//201
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task InserTestAsync(Models.Test newtest)
        {
            try
            {
                //3.1) Convert  Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.Test>(newtest);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3.2 Get response 
                using HttpResponseMessage response =
                    await client.PostAsync("api/Tests/Insert", loginContent);

                response.EnsureSuccessStatusCode();//201


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public async Task<bool> InsertQuestionAsync(Models.Question newqestion)
        {
            try
            {
                //3.1) Convert credentials Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.Question>(newqestion);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3.2 Get response 
                using HttpResponseMessage response =
                    await client.PostAsync("api/Questions/Insert", loginContent);

                response.EnsureSuccessStatusCode();//201
                return true;   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;   
            }
        }

        public async Task<bool> InserAnswerAsync(Models.Answer newAnswer)
        {
            newAnswer.Id = 0;
            try
            {
                //3.1) Convert credentials Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.Answer>(newAnswer);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3.2 Get response 
                using HttpResponseMessage response =
                    await client.PostAsync("api/Answers/Insert", loginContent);

                response.EnsureSuccessStatusCode();//201
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> InserGradeAsync(Models.Grade newGrade)
        {
            newGrade.Id = 0;
            try
            {
                //3.1) Convert credentials Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.Grade>(newGrade);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3.2 Get response 
                using HttpResponseMessage response =
                    await client.PostAsync("api/Grades/Insert", loginContent);

                response.EnsureSuccessStatusCode();//201
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> InsertErrorAsync(List<Models.Error> ErrorsList)
        {
            
            try
            {
                //3.1) Convert credentials Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<List<Models.Error>>(ErrorsList);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3.2 Get response 
                using HttpResponseMessage response =
                    await client.PostAsync("api/Errors/Insert", loginContent);

                response.EnsureSuccessStatusCode();//201
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<List<Models.User>> GetAllUsersByType(string UserType)
        {
            List<Models.User> Users = new List<Models.User>();
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //2 Get server  Resource Data
                using HttpResponseMessage response = await client.GetAsync("api/Users/" + UserType);
                //3 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //4 Get the actual  data string  Content
                jsonResponse = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<Models.User>>(jsonResponse);

            }
            catch (Exception ex)
            {
                
            }

            return Users;
        }

        public async Task<List<Models.Test>> GetAllTestsAsync()
        {
            //02 Call asynchronous network methods in a try/catch block to handle exceptions.
            string jsonResponse = string.Empty;
            List<Models.Test> tests = new List<Models.Test>();
            try
            {
                //03 Get server  Resource Data
                using HttpResponseMessage response = await client.GetAsync("api/Tests/");
                // 04 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //05 Get the actual  data string  Content
                jsonResponse = await response.Content.ReadAsStringAsync();
                tests  = JsonConvert.DeserializeObject<List<Models.Test>>(jsonResponse);


            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message);
            }

            return tests;
           

        }

        public async Task<List<Models.Grade>> GetAllGradesAsync()
        {
            //02 Call asynchronous network methods in a try/catch block to handle exceptions.
            string jsonResponse = string.Empty;
            List<Models.Grade> Grades = new List<Models.Grade>();
            try
            {
                //03 Get server  Resource Data
                using HttpResponseMessage response = await client.GetAsync("api/Grades");
                // 04 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //05 Get the actual  data string  Content
                jsonResponse = await response.Content.ReadAsStringAsync();
                Grades = JsonConvert.DeserializeObject<List<Models.Grade>>(jsonResponse);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Grades;


        }

        public async Task<List<Models.Test>> GetAllTestsByTestGuidOrName(string TestGuidOrName)
        {
            List<Models.Test> tests = new List<Models.Test>();
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //2 Get server  Resource Data
                using HttpResponseMessage response = await client.GetAsync("api/Tests/GetAllTestsByTestGuidOrName?TestGuidOrName=" + TestGuidOrName);
                //3 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //4 Get the actual  data string  Content
                jsonResponse = await response.Content.ReadAsStringAsync();
                tests = JsonConvert.DeserializeObject<List<Models.Test>>(jsonResponse);

            }
            catch (Exception ex)
            {
               
            }

            return tests;
        }

        public async Task<List<Models.Question>> GetAllQuestionsByTestGuid(string TestGuid)
        {
            List<Models.Question> Questions = new List<Models.Question>();
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //2 Get server  Resource Data
                using HttpResponseMessage response = await client.GetAsync("api/Questions/GetAllQuestionsByTestGuid/" + TestGuid);
                //3 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //4 Get the actual  data string  Content
                jsonResponse = await response.Content.ReadAsStringAsync();
                Questions = JsonConvert.DeserializeObject<List<Models.Question>>(jsonResponse);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Questions;
        }

        public async Task<List<Models.Answer>> GetAllAnswersByQusGuid( string QusGuid)
        {
            List<Models.Answer> Answers = new List<Models.Answer>();
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //2 Get server  Resource Data
                using HttpResponseMessage response = await client.GetAsync("api/Answers/GetAllAnswersByQusGuid/" + QusGuid);
                //3 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //4 Get the actual  data string  Content
                jsonResponse = await response.Content.ReadAsStringAsync();
                Answers = JsonConvert.DeserializeObject<List<Models.Answer>>(jsonResponse);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Answers;
        }

        public async Task<List<Models.Error>> GetAllMyErrorsInTest( int StudentId,int ExamId)
        {  
            List<Models.Error> Errors = new List<Models.Error>();
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //2 Get server  Resource Data
                using HttpResponseMessage response = await client.GetAsync("api/Errors/GetAllMyErrorsInTest?StudentId=" + StudentId + "&ExamId=" + ExamId);
                //3 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //4 Get the actual  data string  Content
                jsonResponse = await response.Content.ReadAsStringAsync();
                Errors = JsonConvert.DeserializeObject<List<Models.Error>>(jsonResponse);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Errors;
        }

        public async Task<List<Models.Grade>> GetAllGradesById(int Studentid)
        {
            List<Models.Grade> Grades = new List<Models.Grade>();
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //2 Get server  Resource Data
                using HttpResponseMessage response = await client.GetAsync("api/Grades/GetAllGradesById/" + Studentid);
                //3 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //4 Get the actual  data string  Content
                jsonResponse = await response.Content.ReadAsStringAsync();
                Grades = JsonConvert.DeserializeObject<List<Models.Grade>>(jsonResponse);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Grades;
        }

        public async Task<bool> UpdateTestAsync(Models.Test testToUpdate)
        {
            // 1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //2) Convert  Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.Test>(testToUpdate);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3 Get response 
                using HttpResponseMessage response =
                    await client.PutAsync("api/Tests/Update", loginContent);

                response.EnsureSuccessStatusCode();//201

                //4 Get Json Data From Server Result and  parsing the string data to a boolean.
                var data = await response.Content.ReadAsStringAsync();

                return Boolean.Parse(data);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateQuestionAsync(Models.Question questionToUpdate)
        {
            // 1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //2) Convert  Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.Question>(questionToUpdate);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3 Get response 
                using HttpResponseMessage response =
                    await client.PutAsync("api/Questions/Update", loginContent);

                response.EnsureSuccessStatusCode();//201

                //4 Get Json Data From Server Result and  parsing the string data to a boolean.
                var data = await response.Content.ReadAsStringAsync();

                return Boolean.Parse(data);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAnserAsync(Models.Answer answerToUpdate)
        {
            // 1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //2) Convert  Object to JSON
                string jsonLoginData = System.Text.Json.JsonSerializer.Serialize<Models.Answer>(answerToUpdate);
                using StringContent loginContent = new StringContent(jsonLoginData, Encoding.UTF8, @"application/json");

                //3 Get response 
                using HttpResponseMessage response =
                    await client.PutAsync("api/Answers/Update", loginContent);

                response.EnsureSuccessStatusCode();//201

                //4 Get Json Data From Server Result and  parsing the string data to a boolean.
                var data = await response.Content.ReadAsStringAsync();

                return Boolean.Parse(data);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteTestAsync(int id)
        {
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            { //2 Get server  Resource Data
                using HttpResponseMessage response = await client.DeleteAsync("api/Tests/Delete"+ @"/" + id);
                //3 Ensure that  the operation is return as 200 (Success)
                 response.EnsureSuccessStatusCode();
                //4 Get Json Data From Server Result and  parsing the string data to a boolean.
                 var data = await response.Content.ReadAsStringAsync();

                return Boolean.Parse(data);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteQuestionAsync(string qusGuid)
        {
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            { //2 Get server  Resource Data
                using HttpResponseMessage response = await client.DeleteAsync("api/Questions/Delete" + @"/" + qusGuid);
                //3 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //4 Get Json Data From Server Result and  parsing the string data to a boolean.
                var data = await response.Content.ReadAsStringAsync();

                return Boolean.Parse(data);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAllQuestionsAsync(string TestGuid)
        {
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            { //2 Get server  Resource Data
                using HttpResponseMessage response = await client.DeleteAsync("api/Questions/DeleteAllQuestionsByTestGuid" + @"/" + TestGuid);
                //3 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //4 Get Json Data From Server Result and  parsing the string data to a boolean.
                var data = await response.Content.ReadAsStringAsync();

                return Boolean.Parse(data);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAllAnswersAsync(string QusGuid)
        {
            string jsonResponse = string.Empty;
            //1 Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            { //2 Get server  Resource Data
                using HttpResponseMessage response = await client.DeleteAsync("api/Answers/DeleteAllAnswersByQusGuid" + @"/" + QusGuid);
                //3 Ensure that  the operation is return as 200 (Success)
                response.EnsureSuccessStatusCode();
                //4 Get Json Data From Server Result and  parsing the string data to a boolean.
                var data = await response.Content.ReadAsStringAsync();

                return Boolean.Parse(data);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }


    }
}




