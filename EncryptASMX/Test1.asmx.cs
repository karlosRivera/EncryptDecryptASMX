using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using AuthHeader;
using AuthExtension;

namespace EncryptASMX
{
    /// <summary>
    /// Summary description for Test1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Test1 : System.Web.Services.WebService
    {
        public AuthenticateHeader CredentialsAuth;

        [AuthExtension]
        [SoapHeader("CredentialsAuth", Required = true)]
        [WebMethod]
        public string Add(int x, int y)
        {
            string strValue = "";
            if (CredentialsAuth.UserName == "Test" && CredentialsAuth.Password == "Test")
            {
                strValue = (x + y).ToString();
            }
            else
            {
                //throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
                Context.Response.Status = "403 Forbidden";
                //the next line is untested - thanks to strider for this line
                Context.Response.StatusCode = 403;
                //the next line can result in a ThreadAbortException
                //Context.Response.End(); 
                Context.ApplicationInstance.CompleteRequest();
                return null;
            }
            return strValue;
        }
    }




}
