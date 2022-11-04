using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace DisConnectedAchtrEmp
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(constr);


        }
        public DataSet GetAllEmp()
        {
            da = new SqlDataAdapter("select * from Emp", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "emp");// Emp is a table name given to DataTable
            return ds;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["emp"].NewRow();
                row["name"] = txtName.Text;
                row["salary"] = txtSalary.Text;
                ds.Tables["emp"].Rows.Add(row);
                int result = da.Update(ds.Tables["emp"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {

        }
    }
}
