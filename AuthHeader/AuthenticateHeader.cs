using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace AuthHeader
{
    public class AuthenticateHeader : SoapHeader
    {
        public string UserName;
        public string Password;
    }
}
