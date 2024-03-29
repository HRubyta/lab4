﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using comp2084_lab4.Models;
using System.Security.Cryptography;

namespace comp2084_lab4
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                //create instructor name
                Instructor objI = new Instructor();

                //first get salt value
                String username = txtUsername.Text;

                objI = (from i in db.Instructors
                        where i.Username == username
                        select i).FirstOrDefault();

                //did we find the username
                if (objI != null)
                {
                    String salt = objI.Salt;

                    String password = txtPassword.Text;
                    String pass_and_salt = password + salt;

                    // Create a new instance of the hash crypto service provider.
                    HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
                    // Convert the data to hash to an array of Bytes.
                    byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(pass_and_salt);
                    // Compute the Hash. This returns an array of Bytes.
                    byte[] bytHash = hashAlg.ComputeHash(bytValue);
                    // Optionally, represent the hash value as a base64-encoded string, 
                    // For example, if you need to display the value or transmit it over a network.
                    string base64 = Convert.ToBase64String(bytHash);

                    //check if the password we just salted and hashed matches the password in db
                    if (objI.Password == base64)
                    {
                        //test with label
                        //lblError.Text = "valid login";
                        //store the identity in the session object
                        Session["InstructorID"] = objI.InstructorID;
                        Session["InstructorName"] = objI.FirstName + objI.LastName;
                        Session["DepartmentID"] = objI.DepartmentID;

                        //redirect to department page
                        Response.Redirect("departments.aspx");
                    }
                    else
                    {
                        lblError.Text = "invalid login";
                    }
                }
                else
                {
                    lblError.Text = "Invalid login";
                }
            }
        }
    }
}