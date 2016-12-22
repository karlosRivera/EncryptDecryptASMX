using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Services;
using System.Web.Services.Protocols;
using AuthHeader;
using System.Xml;
using EncryptClass;

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

        private Stream inwardStream;
        private Stream outwardStream;
        public override Stream ChainStream(Stream stream)
        {
            outwardStream = stream;
            inwardStream = new MemoryStream();
            return inwardStream;
        }

        public override void ProcessMessage(SoapMessage message)
        {
            var AfterDeserialize = "";
            bool flag = false;

            string soapMsg1;
            StreamReader readStr;
            StreamWriter writeStr;
            XmlDocument xDoc = new XmlDocument();

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
            else if (message.Stage == SoapMessageStage.BeforeDeserialize)
            {
                readStr = new StreamReader(outwardStream);
                writeStr = new StreamWriter(inwardStream);
                soapMsg1 = readStr.ReadToEnd();
                //soapMsg2 = writeStr.ReadToEnd();
                if (message is System.Web.Services.Protocols.SoapClientMessage)
                {
                    // this is called at client side
                    xDoc.LoadXml(soapMsg1);

                    //						XmlNodeList xSiteID = xDoc.GetElementsByTagName("siteID");
                    //						xSiteID[0].InnerXml = decrypt(xSiteID[0].InnerXml);
                    //
                    //						XmlNodeList xSitePwd = xDoc.GetElementsByTagName("sitePwd");
                    //						xSitePwd[0].InnerXml = decrypt(xSitePwd[0].InnerXml);

                    XmlNodeList xResult = xDoc.GetElementsByTagName("AuthenticateResult");
                    xResult[0].InnerXml = decrypt(xResult[0].InnerXml);

                }
                else if (message is System.Web.Services.Protocols.SoapServerMessage)
                {
                    // this is called at server side
                    xDoc.LoadXml(soapMsg1);

                    XmlNodeList xSiteID = xDoc.GetElementsByTagName("siteID");
                    xSiteID[0].InnerXml = decrypt(xSiteID[0].InnerXml);

                    XmlNodeList xSitePwd = xDoc.GetElementsByTagName("sitePwd");
                    xSitePwd[0].InnerXml = decrypt(xSitePwd[0].InnerXml);

                    XmlNodeList xUserID = xDoc.GetElementsByTagName("UserID");
                    xUserID[0].InnerXml = decrypt(xUserID[0].InnerXml);

                    XmlNodeList xPwd = xDoc.GetElementsByTagName("Password");
                    xPwd[0].InnerXml = decrypt(xPwd[0].InnerXml);
                }

                soapMsg1 = xDoc.InnerXml;
                writeStr.Write(soapMsg1);
                writeStr.Flush();
                inwardStream.Position = 0;
            }
            else if (message.Stage == SoapMessageStage.AfterSerialize)
            {
                inwardStream.Position = 0;
                readStr = new StreamReader(inwardStream);
                writeStr = new StreamWriter(outwardStream);
                soapMsg1 = readStr.ReadToEnd();
                //soapMsg2 = writeStr.ReadToEnd();
                if (message is System.Web.Services.Protocols.SoapClientMessage)
                {
                    // this is called at client side
                    xDoc.LoadXml(soapMsg1);

                    XmlNodeList xSiteID = xDoc.GetElementsByTagName("siteID");
                    xSiteID[0].InnerXml = encrypt(xSiteID[0].InnerXml);

                    XmlNodeList xSitePwd = xDoc.GetElementsByTagName("sitePwd");
                    xSitePwd[0].InnerXml = encrypt(xSitePwd[0].InnerXml);

                    XmlNodeList xUserID = xDoc.GetElementsByTagName("UserID");
                    xUserID[0].InnerXml = encrypt(xUserID[0].InnerXml);

                    XmlNodeList xPwd = xDoc.GetElementsByTagName("Password");
                    xPwd[0].InnerXml = encrypt(xPwd[0].InnerXml);

                }
                else if (message is System.Web.Services.Protocols.SoapServerMessage)
                {
                    // this is called at server side
                    xDoc.LoadXml(soapMsg1);

                    //						XmlNodeList xSiteID = xDoc.GetElementsByTagName("siteID");
                    //						xSiteID[0].InnerXml = encrypt(xSiteID[0].InnerXml);
                    //
                    //						XmlNodeList xSitePwd = xDoc.GetElementsByTagName("sitePwd");
                    //						xSitePwd[0].InnerXml = encrypt(xSitePwd[0].InnerXml);

                    XmlNodeList xResult = xDoc.GetElementsByTagName("AuthenticateResult");
                    xResult[0].InnerXml = encrypt(xResult[0].InnerXml);
                }

                soapMsg1 = xDoc.InnerXml;
                writeStr.Write(soapMsg1);
                writeStr.Flush();
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

        public override object GetInitializer(Type serviceType)
        {
            return null;
        }

        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return null;
        }

        public override void Initialize(object initializer)
        {
            return;
        }

        private string encrypt(string message)
        {
            EncryptClass.EncryptClass ec = new EncryptClass.EncryptClass();
            string encryStr = ec.custEncrypt(message);
            return encryStr;
        }

        private string decrypt(string message)
        {
            EncryptClass.EncryptClass ec = new EncryptClass.EncryptClass();
            string decryptStr = message;
            return ec.custDecrypt(decryptStr);
        }
    }
}
