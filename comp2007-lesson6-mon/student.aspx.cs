using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//model references 
using comp2007_lesson6_mon.Models;
using System.Web.ModelBinding;

namespace comp2007_lesson6_mon
{
    public partial class student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if the page isn't posted back, check the url for an id to see know add or edit
            if (!IsPostBack)
            {
                if (Request.QueryString.Keys.Count > 0)
                {
                    //we have a url parameter if the count is > 0
                    GetStudent();
                }
            }
        }

        protected void GetStudent()
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //get the id from the url paramenter and store in a variable
                Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                var s = (from dep in conn.Students
                         where dep.StudentID == StudentID
                         select dep).FirstOrDefault();

                //populate the formfrom our department object
                txtLastName.Text = s.LastName;
                txtFirstMidName.Text = s.FirstMidName;
                //s.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);
               
                
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //instantiate a new student object in memory
                Student s = new Student();


                //decide if updating or adding, then save
                if (Request.QueryString.Count > 0)
                {
                    Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    s = (from dep in conn.Students
                         where dep.StudentID == StudentID
                         select dep).FirstOrDefault();
                }


                //fill the propreties of our object from the form inputs
                s.LastName = txtLastName.Text;
                s.FirstMidName = txtFirstMidName.Text;
                s.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);


                if (Request.QueryString.Count == 0)
                {
                    conn.Students.Add(s);
                }
                conn.SaveChanges();

                //redirect to updated departments page
                Response.Redirect("students.aspx");
            }
           
        }
    }
}