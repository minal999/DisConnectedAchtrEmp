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
    public partial class Student : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Student()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public DataSet GetAllStudent()
        {
            da = new SqlDataAdapter("select * from student", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "student");
            return ds;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                DataRow row = ds.Tables["student"].NewRow();
                row["name"] = txtName.Text;
                row["Sub1"] = txtSub1.Text;
                row["Sub2"] = txtSub2.Text;
                row["Sub3"] = txtSub3.Text;
                row["Percentage"] = txtPer.Text;
                ds.Tables["student"].Rows.Add(row);
                int result = da.Update(ds.Tables["student"]);
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
            try
            {
                ds = GetAllStudent();
                DataRow row = ds.Tables["student"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["Sub1"] = txtSub1.Text;
                    row["Sub2"] = txtSub2.Text;
                    row["Sub3"] = txtSub3.Text;
                    row["Percentage"] = txtPer.Text;
                    int result = da.Update(ds.Tables["student"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                DataRow row = ds.Tables["student"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["student"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                DataRow row = ds.Tables["student"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    txtName.Text = row["name"].ToString();
                    txtSub1.Text = row["Sub1"].ToString();
                    txtSub2.Text = row["Sub2"].ToString();
                    txtSub3.Text = row["Sub3"].ToString();
                    txtPer.Text = row["Percentage"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                dataGridView1.DataSource = ds.Tables["student"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
