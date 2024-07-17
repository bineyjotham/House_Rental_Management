using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace House_Rental_Management
{
    public partial class Apartments : Form
    {
        public Apartments()
        {
            InitializeComponent();
            GetCategories();
            GetOwner();
            ShowAparts();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hi.DESKTOP-3TRLI97\Documents\House_Rental_Management.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetCategories()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CNum from CategoryTb1", Con);
            SqlDataReader Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CNum", typeof(int));
            dt.Load(Rdr);
            TypeCb.ValueMember = "CNum";
            TypeCb.DataSource = dt;
            Con.Close();
        }
        private void GetOwner()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select LLId from LandLordTb1", Con);
            SqlDataReader Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("LLId", typeof(int));
            dt.Load(Rdr);
            LLcb.ValueMember = "LLId";
            LLcb.DataSource = dt;
            Con.Close();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void ShowAparts()
        {
            Con.Open();
            string Query = "Select * from ApartTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ApartmentsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ResetData()
        {
            ApNameTb.Text = "";
            LLcb.SelectedIndex = -1;
            CostsTb.Text = "";
            TypeCb.SelectedIndex = -1;
            AddressTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || TypeCb.SelectedIndex == -1 || CostsTb.Text == "" || LLcb.SelectedIndex == -1 || AddressTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ApartTb1(AName, AAddress, AType, ACost, Owner)values(@ANa,@AAd,@ATy,@ACo,@AOw)", Con);
                    cmd.Parameters.AddWithValue(@"ANa", ApNameTb.Text);
                    cmd.Parameters.AddWithValue(@"AAd", AddressTb.Text);
                    cmd.Parameters.AddWithValue(@"ATy", TypeCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AOw", LLcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ACo", CostsTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Added!!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            // this.Hide();
        }
        int Key = 0;
        private void ApartmentsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ApNameTb.Text = ApartmentsDGV.SelectedRows[0].Cells[1].Value.ToString();
            AddressTb.Text = ApartmentsDGV.SelectedRows[0].Cells[2].Value.ToString();
            TypeCb.Text = ApartmentsDGV.SelectedRows[0].Cells[3].Value.ToString();
            CostsTb.Text = ApartmentsDGV.SelectedRows[0].Cells[4].Value.ToString();
            LLcb.Text = ApartmentsDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (ApNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ApartmentsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || TypeCb.SelectedIndex == -1 || CostsTb.Text == "" || LLcb.SelectedIndex == -1 || AddressTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ApartTb1 set AName = @ANa, AAddress = @AAd, AType = @ATy, ACOst = @ACo, Owner = @AOw where Anum = @AKey", Con);
                    cmd.Parameters.AddWithValue(@"ANa", ApNameTb.Text);
                    cmd.Parameters.AddWithValue(@"AAd", AddressTb.Text);
                    cmd.Parameters.AddWithValue(@"ATy", TypeCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AOw", LLcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ACo", CostsTb.Text);
                    cmd.Parameters.AddWithValue(@"AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Updated!!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();

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
                MessageBox.Show("Select an Apartment!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ApartTb1 where Anum=@Akey", Con);
                    cmd.Parameters.AddWithValue("@Akey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Deleted!!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
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

        private void label3_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
            this.Hide();
        }

        private void TypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
