﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//add a rference so can use EF(entity framework) for our database
using comp2084_lab4.Models;



namespace comp2084_lab4
{
    public partial class department_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if have id look up the selected record
                if (!String.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
                {
                    GetDepartment();
                }
            }

        }

        protected void GetDepartment()
        {
            try
            {
                //look up the selected department and fill the form
                using (DefaultConnection db = new DefaultConnection())
                {
                    Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    //look up department
                    Department dep = (from d in db.Departments
                                      where d.DepartmentID == DepartmentID
                                      select d).FirstOrDefault();

                    //pre-populate the form fields
                    txtName.Text = dep.Name;
                    txtBudget.Text = dep.Budget.ToString();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //create new department in memory
                Department dep = new Department();

                //check url
                if (!String.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
                {
                    Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    dep = (from d in db.Departments
                           where d.DepartmentID == DepartmentID
                           select d).FirstOrDefault();


                }
                //fill new properties of the new department
                dep.Name = txtName.Text;
                dep.Budget = Convert.ToDecimal(txtBudget.Text);

                //save the new department
                if (String.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
                {
                    db.Departments.Add(dep);
                }
                db.Departments.Add(dep);
                db.SaveChanges();

                //redirect to department list page
                Response.Redirect("departments.aspx");
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