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

namespace House_Rental_Management
{
    public partial class Tenants : Form
    {
        public Tenants()
        {
            InitializeComponent();
            ShowTenants();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hi.DESKTOP-3TRLI97\Documents\House_Rental_Management.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowTenants()
        {
            Con.Open();
            string Query = "Select * from TenantTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TenantsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ResetData()
        {
            PhoneTb.Text = "";
            Gencb.SelectedIndex = -1;
            TnameTb.Text = "";
        }

        /*private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (TnameTb.Text == "" || Gencb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TenantTb1(TenName, TenPhone, TenGen)values(@TN,@TP,@TG)", Con);
                    cmd.Parameters.AddWithValue(@"TN", TnameTb.Text);
                    cmd.Parameters.AddWithValue(@"TP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue(@"TG", Gencb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Added!!!");
                    Con.Close();
                    ResetData();
                    ShowTenants();

                }
                catch (Exception Ex) 
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }*/
        int Key = 0;
        //private void TenantsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    TnameTb.Text = TenantsDGV.SelectedRows[0].Cells[1].Value.ToString();
        //    PhoneTb.Text = TenantsDGV.SelectedRows[0].Cells[2].Value.ToString();
        //    Gencb.Text = TenantsDGV.SelectedRows[0].Cells[3].Value.ToString();
        //   if(TnameTb.Text == "")
        //    {
        //        Key = 0;
        //    }
        //    else
        //    {
        //        Key = Convert.ToInt32(TenantsDGV.SelectedRows[0].Cells[0].Value.ToString());
        //    }
       // }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (TnameTb.Text == "" || Gencb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TenantTb1(TenName, TenPhone, TenGen)values(@TN,@TP,@TG)", Con);
                    cmd.Parameters.AddWithValue(@"TN", TnameTb.Text);
                    cmd.Parameters.AddWithValue(@"TP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue(@"TG", Gencb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Added!!!");
                    Con.Close();
                    ResetData();
                    ShowTenants();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TnameTb.Text == "" || Gencb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update TenantTb1 set TenName = @TN, TenPhone = @TP, TenGen = @TG where TenId = @TKey", Con);
                    cmd.Parameters.AddWithValue(@"TN", TnameTb.Text);
                    cmd.Parameters.AddWithValue(@"TP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue(@"TG", Gencb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Updated!!!");
                    Con.Close();
                    ResetData();
                    ShowTenants();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a tenant!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from TenantTb1 where TenId=@Tkey", Con);
                    cmd.Parameters.AddWithValue("@Tkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Deleted!!!");
                    Con.Close();
                    ResetData();
                    ShowTenants();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void TenantsDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            TnameTb.Text = TenantsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = TenantsDGV.SelectedRows[0].Cells[2].Value.ToString();
            Gencb.Text = TenantsDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (TnameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TenantsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void TnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Apartments Obj = new Apartments();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            //this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Apartments Obj = new Apartments();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
            this.Hide();
        }
    }
}
