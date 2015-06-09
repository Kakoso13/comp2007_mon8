<%@ Page Title="" Language="C#" MasterPageFile="~/monday.Master" AutoEventWireup="true" CodeBehind="student.aspx.cs" Inherits="comp2007_lesson6_mon.student" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Student Details</h1>

    <h5>All fields are required</h5>

     <div class="form-group">
        <label for="txtLastName">Last Name:</label>
        <asp:TextBox ID="txtLastName" runat="server" required MaxLength="50" />
    </div>
    <div class="form-group">
        <label for="txtFirstMidName">First Name:</label>
        <asp:TextBox ID="txtFirstMidName" runat="server" required MaxLength="50" />
   </div>
    <div class="form-group">
        <label for="txtEnrollmentDate">Enrollment Date:</label>
        <asp:TextBox ID="txtEnrollmentDate" runat="server" required TextMode="Date" />
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Must be a Valid Date"
            ControlToValidate="txtEnrollmentDate" CssClass="alert alert-danger"
            Type="Date" MinimumValue="2000-01-01" MaximumValue="12/31/2999"></asp:RangeValidator>
    </div>

    <div>
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
             OnClick="btnSave_Click" />
    </div>
</asp:Content>
