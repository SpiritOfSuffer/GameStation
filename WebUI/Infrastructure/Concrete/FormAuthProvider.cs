using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool IsAuthenticated = FormsAuthentication.Authenticate(username, password);

            if (IsAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            } 

            return IsAuthenticated;
        }
    }
}