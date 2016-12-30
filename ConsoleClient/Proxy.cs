using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthExtension;

namespace ConsoleClient.AsmxService
{
    public partial class Test1 : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        [AuthExtension]
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenticateHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Add", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CustAdd(int x, int y)
        {
            object[] results = this.Invoke("Add", new object[] {
                        x,
                        y});
            return ((string)(results[0]));
        }
    }
}
