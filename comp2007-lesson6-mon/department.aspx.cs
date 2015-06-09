using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference models
using comp2007_lesson6_mon.Models;
using System.Web.ModelBinding;

namespace comp2007_lesson6_mon
{
    public partial class department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if the page isn't posted back, check the url for an id to see know add or edit
            if (!IsPostBack)
            {
                if (Request.QueryString.Keys.Count > 0)
                {
                    //we have a url parameter if the count is > 0
                    GetDepartment();
                }
            }
        }

        protected void GetDepartment()
        {
            //connect
            using(DefaultConnection conn = new DefaultConnection())
            {
                //get the id from the url paramenter and store in a variable
                Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                var d = (from dep in conn.Departments
                         where dep.DepartmentID == DepartmentID
                         select dep).FirstOrDefault();

                //populate the formfrom our department object
                txtName.Text = d.Name;
                txtBudget.Text = d.Budget.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using(DefaultConnection conn = new DefaultConnection())
            {
                //instantiate a new department object in memory
                Department d = new Department();

                
                //decide if updating or adding, then save
                if (Request.QueryString.Count > 0)
                {
                    Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    d = (from dep in conn.Departments
                         where dep.DepartmentID == DepartmentID
                         select dep).FirstOrDefault();
                }


               //fill the propreties of our object from the form inputs
                d.Name = txtName.Text;
                d.Budget = Convert.ToDecimal(txtBudget.Text);


                if (Request.QueryString.Count == 0)
                {
                    conn.Departments.Add(d);
                }
                conn.SaveChanges();

                //redirect to updated departments page
                Response.Redirect("departments.aspx");
            }
            
        }
    }
}