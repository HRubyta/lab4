using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace comp2084_lab4
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                // Pass the error on to the error page.
                Server.Transfer("error.aspx?handler=Application_Error%20-%20Global.asax", true);
            }

            var serverError = Server.GetLastError() as HttpException;

            if (null != serverError)
            {
                int errorCode = serverError.GetHttpCode();

                if(404 == errorCode)
                {
                    Server.ClearError();
                    Server.Transfer("404-error.aspx");
                }
            }
        }
    }
}