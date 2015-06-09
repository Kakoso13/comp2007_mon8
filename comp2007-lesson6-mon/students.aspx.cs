using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference the models
using comp2007_lesson6_mon.Models;
using System.Web.ModelBinding;



namespace comp2007_lesson6_mon
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //fill the grid
            if (!IsPostBack)
            {
                GetStudents();
            }
        }

        protected void GetStudents()
        {

            //connect using our connection string from web.config and EF context class
            using (DefaultConnection conn = new DefaultConnection())
            {

                //use link to query the Student model
                var deps = from s in conn.Students
                           select s;

                //bind the query result to the gridview
                grdStudents.DataSource = deps.ToList();
                grdStudents.DataBind();

            }
        }
        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //get the selected StudentID
                Int32 StudentID = Convert.ToInt32(grdStudents.DataKeys[e.RowIndex].Values["StudentID"]);

                var s = (from dep in conn.Students
                         where dep.StudentID == StudentID
                         select dep).FirstOrDefault();

                //process the delete
                conn.Students.Remove(s);
                conn.SaveChanges();

                //update the grip
                GetStudents();
            }
        }
    }
}