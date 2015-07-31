using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace comp2084_lab4
{
    public partial class _404_error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //IIS error
            Response.TrySkipIisCustomErrors = true;

            //set status code and message
            Response.StatusCode = 404;
            Response.StatusDescription = "Page not found";
        }
    }
}