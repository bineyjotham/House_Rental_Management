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
    public partial class Rents : Form
    {
        public Rents()
        {
            InitializeComponent();
            GetApart();
            GetTenants();
            ShowRents();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hi.DESKTOP-3TRLI97\Documents\House_Rental_Management.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetApart()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Anum from ApartTb1", Con);
            SqlDataReader Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Anum", typeof(int));
            dt.Load(Rdr);
            ApartCb.ValueMember = "Anum";
            ApartCb.DataSource = dt;
            Con.Close();
        }
        private void GetTenants()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TenID from TenantTb1", Con);
            SqlDataReader Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TenID", typeof(int));
            dt.Load(Rdr);
            TenantsCb.ValueMember = "TenID";
            TenantsCb.DataSource = dt;
            Con.Close();
        }
        private void ShowRents()
        {
            Con.Open();
            string Query = "Select * from RentTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RentsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ResetData()
        { 
            AmountTb.Text = "";
        }
        /*private void GetCosts()
        {
            try
            {


                Con.Open();
                string query = "select * from ApartTb1 where Anum=" + ApartCb.SelectedValue.ToString() + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    AmountTb.Text = dr["ACost"].ToString();
                }
                Con.Close();
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
            }
        }*/
        private void GetCosts()
        {

            string query = "select * from ApartTb1 where Anum=" + ApartCb.SelectedValue.ToString() + "";
            using (var Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hi.DESKTOP-3TRLI97\Documents\House_Rental_Management.mdf;Integrated Security=True;Connect Timeout=30"))
            using (var cmd = new SqlCommand(query, Con))
            {
                Con.Open();
                //string query = "select * from ApartTb1 where Anum=" + ApartCb.SelectedValue.ToString() + "";
                //using (var cmd = new SqlCommand(query, Con))
                //SqlCommand cmd = new SqlCommand(query, Con);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    AmountTb.Text = dr["ACost"].ToString();
                }
            }
                //Con.Close();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ApartmentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ApartCb.Text = RentsDGV.SelectedRows[0].Cells[1].Value.ToString();
            AmountTb.Text = RentsDGV.SelectedRows[0].Cells[2].Value.ToString();
            TenantsCb.Text = RentsDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ApartCb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(RentsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (ApartCb.SelectedIndex == -1 || TenantsCb.SelectedIndex == -1 || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    string Period = RDate.Value.Date.Month+"-"+RDate.Value.Date.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into RentTb1(Apartment, Tenant, Period, Amount)values(@RAp,@RTe,@RPe,@RAm)", Con);
                    cmd.Parameters.AddWithValue(@"RAp", ApartCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"RTe", TenantsCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"RPe", Period);
                    cmd.Parameters.AddWithValue("@RAm", AmountTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Rented!!!");
                    Con.Close();
                    ResetData();
                    ShowRents();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ApartCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCosts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ApartCb.SelectedIndex == -1 || TenantsCb.SelectedIndex == -1 || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    string Period = RDate.Value.Date.Month + "-" + RDate.Value.Date.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update RentTb1 set Apartment = @RAp, Tenant = @RTe, Period = @RPe, Amount = @RAm where RCode = @RKey", Con);
                    cmd.Parameters.AddWithValue(@"RAp", ApartCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"RTe", TenantsCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"RPe", Period);
                    cmd.Parameters.AddWithValue("@RAm", AmountTb.Text);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Updated!!!");
                    Con.Close();
                    ResetData();
                    ShowRents();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("PLease select one of the active rents!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from RentTb1 where RCode=@Rkey", Con);
                    cmd.Parameters.AddWithValue("@Rkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rent terminated!!!");
                    Con.Close();
                    ResetData();
                    ShowRents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
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

        private void label4_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
           // this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
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

        private void label5_Click(object sender, EventArgs e)
        {

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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
