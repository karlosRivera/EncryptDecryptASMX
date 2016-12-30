using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AsmxService.AuthenticateHeader oAuth = new AsmxService.AuthenticateHeader();
            oAuth.UserName = "RTest";
            oAuth.Password = "Test";

            AsmxService.Test1 oClient = new AsmxService.Test1();
            oClient.AuthenticateHeaderValue = oAuth;

            try
            {
                var o = oClient.Add(1, 2);
                Console.WriteLine(o);
            }
             catch (SoapException SoapEx)
            {
                Console.WriteLine(SoapEx.Message);
            }
            Console.ReadLine();
        }
    }
}
