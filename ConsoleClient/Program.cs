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
            ServiceReference1.AuthenticateHeader oAuth = new ServiceReference1.AuthenticateHeader();
            oAuth.UserName = "Test";
            oAuth.Password = "Test";

            ServiceReference1.Test1SoapClient oClient = new ServiceReference1.Test1SoapClient();
            var o = oClient.CustomAdd(oAuth, 1, 2);
        }
    }
}
