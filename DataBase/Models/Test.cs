namespace DataBase.Models
{
    public class Test
    {
        public Test()
        {

        }


        //Property

        public int Id { get; set; }
        public string TestGuid { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string TeacherName { get; set; }

        public TimeSpan StartTime { get; set; }

        public double TotalTime { get; set; }

        public bool IsRendomOrder { get; set; }




        public Test(string testguid, string name, string date, string teacherName, string startTime, string totalTime, bool? isRendomOrder)
        {
            TestGuid = testguid;
            Name = name;
            Date = Convert.ToDateTime(date);
            TeacherName = teacherName;
            StartTime = TimeSpan.Parse(startTime);
            TotalTime = Convert.ToDouble(totalTime);
            IsRendomOrder = Convert.ToBoolean(isRendomOrder);
        }

        public Test( string name, string date, string teacherName, string startTime, string totalTime, bool? isRendomOrder)
        {
          
            Name = name;
            Date = Convert.ToDateTime(date);
            TeacherName = teacherName;
            StartTime = TimeSpan.Parse(startTime);
            TotalTime = Convert.ToDouble(totalTime);
            IsRendomOrder = Convert.ToBoolean(isRendomOrder);
        }


        public Test(int id, string testguid, string name, string date, string teacherName, string startTime, string totalTime, bool? isRendomOrder)
        {
            Id = id;
            TestGuid = testguid;
            Name = name;
            Date = Convert.ToDateTime(date);
            TeacherName = teacherName;
            StartTime = TimeSpan.Parse(startTime);
            TotalTime = Convert.ToDouble(totalTime);
            IsRendomOrder = Convert.ToBoolean(isRendomOrder);
        }
        public override string ToString()
        {
            return Id + "-" + Name + "-" + Date.Add(StartTime);

        }
    }
}
