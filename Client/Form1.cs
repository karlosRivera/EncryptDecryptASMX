using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Web.Services.Protocols;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceReference1.AuthenticateHeader oAuth = new ServiceReference1.AuthenticateHeader();
            oAuth.UserName = "Test";
            oAuth.Password = "Test";

            ServiceReference1.Test1SoapClient oClient =new ServiceReference1.Test1SoapClient();
            try
            {
               var o= oClient.Add(oAuth, 2, 3);
            }
            catch(SoapException SoapEx)
            {
                //string strMsg=SoapEx.
            }
            catch (Exception Ex)
            {
                
            }
        }
    }
}
