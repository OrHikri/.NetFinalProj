using System;
using System.Threading.Tasks;

namespace TestAdministration
{


    public class Program
    {
         public static async Task Main()
        {
            HttpRequestor httpRequestor = new HttpRequestor();
            //await httpRequestor.PostItemToServerAsync("api/tests","ABABAB");

            

            //await httpRequestor.GetServerDataAsync("api/tests", 5);

            Console.ReadKey();

        }


    }
}