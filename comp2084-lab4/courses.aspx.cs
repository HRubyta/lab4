using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//entity framework reference
using comp2084_lab4.Models;

namespace comp2084_lab4
{
    public partial class courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetCourses();
        }

        protected void GetCourses()
        {
            try
            {
                using (DefaultConnection db = new DefaultConnection())
                {
                    var cours = from d in db.Courses
                                select d;

                    //bind the cours query result to our grid
                    grdCourses.DataSource = cours.ToList();
                    grdCourses.DataBind();
                }
            }
            catch (NullReferenceException e)
            {
                Trace.Write("An error occured during operation with Message:", e.Message);
                Trace.Write("Stack Trace", e.StackTrace);
            }
            catch (Exception e)
            {
                Trace.Write("Database unavailable with Message:", e.Message);
                Trace.Write("Stack Trace", e.StackTrace);
            }


        }

        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            //Handle specific exception
            if (exc is HttpUnhandledException)
            {
                Response.Redirect("error.aspx");
            }

            Server.ClearError();
        }
    }
}