using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PetShop
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            DisplayCustomer();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-1D77MCM;Initial Catalog=PetShopDb;Persist Security Info=True;User ID=sa;Password=123");
        private void DisplayCustomer()
        {
            try
            {
                conn.Open();
                string sql = "select * from CustomerTbl";
                SqlDataAdapter Sda = new SqlDataAdapter(sql, conn);
                SqlCommandBuilder CmdBuilder = new SqlCommandBuilder(Sda);
                var Ds = new DataSet();
                Sda.Fill(Ds);
                CusGridView.DataSource = Ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                DisplayCustomer();
                ClearCus();
            }
        }

      
        private void label1_Click(object sender, EventArgs e)
        {
            Products ProObj = new Products();
            ProObj.Show();
            this.Hide();
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees EmpObj = new Employees();
            EmpObj.Show();
            this.Hide();
        }

        private void ClearCus()
        {
            CusNameTb.Text = "";
            CusAddTb.Text = "";
            CusphoneTb.Text = "";

        }

        private void SaveBtnCus_Click(object sender, EventArgs e)
        {

            if (CusNameTb.Text == "" || CusAddTb.Text == "" || CusphoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl(CusName,CusAdd,CusPhone) values(@CN,@CA,@CP)", conn);
                    cmd.Parameters.AddWithValue("@CN", CusNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CusAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CusphoneTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Customer Added");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    conn.Close();

                    DisplayCustomer();
                    ClearCus();

                }

            }
        }
        int key = 0;
        private void CusGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            CusNameTb.Text = CusGridView.SelectedRows[0].Cells[1].Value.ToString();
            CusphoneTb.Text = CusGridView.SelectedRows[0].Cells[2].Value.ToString();
            CusAddTb.Text = CusGridView.SelectedRows[0].Cells[3].Value.ToString();



            if (CusNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CusGridView.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtnCus_Click(object sender, EventArgs e)
        {
            if (CusNameTb.Text == "" || CusAddTb.Text == "" || CusphoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTbl set (CusName=@CN,CusAdd=@EA,,CusPhone=@CP where CusId=@Ckey", conn);
                    cmd.Parameters.AddWithValue("@CN", CusNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CusAddTb.Text);

                    cmd.Parameters.AddWithValue("@CP", CusphoneTb.Text);

                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Update!!!");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    conn.Close();
                    DisplayCustomer();
                    ClearCus();
                }

            }
        }

        private void DeleteBtnCus_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select An Customer");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CusId = @Cuskey", conn);
                    cmd.Parameters.AddWithValue("@Cuskey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted!!!");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    conn.Close();
                    DisplayCustomer();
                    ClearCus();
                }
            }
        }
    }
}
