using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

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
        public AuthenticateHeader Credentials;

        [AuthExtension]
        [SoapHeader("credentials", Required = true)]
        [WebMethod]
        public string Add(int x, int y)
        {
            return (x + y).ToString();
        }
    }

    public class AuthenticateHeader : SoapHeader
    {
        public string UserName;
        public string Password;
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class AuthExtensionAttribute : SoapExtensionAttribute
    {
        int _priority = 1;

        public override int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        public override Type ExtensionType
        {
            get { return typeof(AuthExtension); }
        }
    }

    public class AuthExtension : SoapExtension
    {
        public override void ProcessMessage(SoapMessage message)
        {
            if (message.Stage == SoapMessageStage.AfterDeserialize)
            {
                //Check for an AuthHeader containing valid
                //credentials
                foreach (SoapHeader header in message.Headers)
                {
                    if (header is AuthenticateHeader)
                    {
                        AuthenticateHeader credentials = (AuthenticateHeader)header;
                        if (credentials.UserName.ToLower() ==
                            "jeff" &&
                            credentials.Password.ToLower() ==
                            "imbatman")
                            return; // Allow call to execute
                        break;
                    }
                }

                // Fail the call if we get to here. Either the header
                // isn't there or it contains invalid credentials.
                throw new SoapException("Unauthorized",
                    SoapException.ClientFaultCode);
            }
        }

        public override Object GetInitializer(Type type)
        {
            return GetType();
        }

        public override Object GetInitializer(LogicalMethodInfo info,
            SoapExtensionAttribute attribute)
        {
            return null;
        }

        public override void Initialize(Object initializer)
        {
        }
    }
}
