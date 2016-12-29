using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthExtension;

namespace ConsoleClient.ServiceReference1
{
    public partial class Test1SoapClient : System.ServiceModel.ClientBase<ConsoleClient.ServiceReference1.Test1Soap>, ConsoleClient.ServiceReference1.Test1Soap 
    {
        [AuthExtension]
        public string CustomAdd(ConsoleClient.ServiceReference1.AuthenticateHeader AuthenticateHeader, int x, int y)
        {
            ConsoleClient.ServiceReference1.AddRequest inValue = new ConsoleClient.ServiceReference1.AddRequest();
            inValue.AuthenticateHeader = AuthenticateHeader;
            inValue.x = x;
            inValue.y = y;
            ConsoleClient.ServiceReference1.AddResponse retVal = ((ConsoleClient.ServiceReference1.Test1Soap)(this)).Add(inValue);
            return retVal.AddResult;
        }
    }
}
