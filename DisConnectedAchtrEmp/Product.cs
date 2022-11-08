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
    public partial class Product : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;

        public Product()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(constr);

        }
        public DataSet GetAllProduct()
        {
            da = new SqlDataAdapter("select * from product", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "product");
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProduct();
                DataRow row = ds.Tables["product"].NewRow();
                row["name"] = txtName.Text;
                row["price"] = txtPrice.Text;
                row["company_Name"] = txtCompany.Text;
                ds.Tables["product"].Rows.Add(row);
                int result = da.Update(ds.Tables["product"]);
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
                ds = GetAllProduct();
                DataRow row = ds.Tables["product"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["price"] = txtPrice.Text;
                    row["company_Name"] = txtCompany.Text;
                    int result = da.Update(ds.Tables["product"]);
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
                ds = GetAllProduct();
                DataRow row = ds.Tables["product"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["product"]);
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
                ds = GetAllProduct();
                DataRow row = ds.Tables["product"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    txtName.Text = row["name"].ToString();
                    txtPrice.Text = row["price"].ToString();
                    txtCompany.Text = row["company_Name"].ToString();
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
                ds = GetAllProduct();
                dataGridView1.DataSource = ds.Tables["product"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
