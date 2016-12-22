using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Services.Protocols;
using AuthHeader;

namespace AuthExtension
{
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
            var AfterDeserialize = "";
            bool flag = false;
            if (message.Stage == SoapMessageStage.AfterDeserialize)
            {
                AfterDeserialize = "AfterDeserialize";
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
                            flag = true;
                        //return; // Allow call to execute
                        break;
                    }
                }

                // Fail the call if we get to here. Either the header
                // isn't there or it contains invalid credentials.
            }

            //if (AfterDeserialize == "AfterDeserialize")
            //{
            //    if (!flag)
            //    {
            //        throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            //        //return;
            //    }
            //}
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
