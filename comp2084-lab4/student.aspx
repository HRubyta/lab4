﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="student.aspx.cs" Inherits="comp2084_lab4.student" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <h1>Student Details</h1>

    <h5>All fields are required</h5>
    <div class="form-group">
        <label for="txtFirstName" class="col-sm-2">First name: </label>
        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" required />
    </div>
    <div class="form-group">
        <label for="txtLastName" class="col-sm-2">Last name: </label>
        <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" required />
    </div>
    <div class="form-group">
        <label for="txtEnrollDate" class="col-sm-2">Enrollment date: </label>
        <asp:TextBox ID="txtEnrollDate" runat="server" required />
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Must be between 2014-09-01 and 2004-09-01"
              CssClass="alert alert-danger" 2014-09-01" MinimumValue="2004-09-01" ControlToValidate="txtEnrollDate"
             Type="Date" />

    </div>
    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
            onclick="btnSave_Click" />
    </div>
    <asp:Panel ID="pnlCourses" runat="server" Visible="false">
        <h2>Courses</h2>
        <asp:GridView ID="grdCourses" runat="server" DataKeyNames="EnrollmentID" 
            AutoGenerateColumns="false" CssClass="table table-striped table-hover"
            OnRowDeleting="grdCourses_RowDeleting">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Department" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="CourseID"
                 DataNavigateUrlFormatString="students.aspx?CourseID={0}" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
