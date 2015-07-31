using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//add a rference so can use EF(entity framework) for our database
using comp2084_lab4.Models;


namespace comp2084_lab4
{
    public partial class departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //call the GetDepartments function to populate grid
            if (!IsPostBack)
            {
                GetDepartments();
            }

        }

        protected void GetDepartments()
        {
            try
            {
                //use entity framework to connect and get the list of departments
                using (DefaultConnection db = new DefaultConnection())
                {
                    //old query that show all department
                    //var deps = from d in db.Departments
                    //          select d;
                    //new query filtered for logged in user only
                    Int32 DepartmentID = Convert.ToInt32(Session["DepartmentID"]);

                    var deps = from d in db.Departments
                               where d.DepartmentID == DepartmentID
                               select d;

                    //bind the deps query result to our grid
                    grdDepartments.DataSource = deps.ToList();
                    grdDepartments.DataBind();
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

        protected void grdDepartments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the new page index and repopulate the grid
            grdDepartments.PageIndex = e.NewPageIndex;
            GetDepartments();

        }

        protected void grdDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //identify the departmentID to be deleted from the row user selected
            Int32 DepartmentID = Convert.ToInt32(grdDepartments.DataKeys[e.RowIndex].Values["DepartmentID"]);

            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                Department dep = (from d in db.Departments
                                  where d.DepartmentID == DepartmentID
                                  select d).FirstOrDefault();

                //delete
                db.Departments.Remove(dep);
                db.SaveChanges();

                //refresh grid
                GetDepartments();
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