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
    public partial class Landlords : Form
    {
        public Landlords()
        {
            InitializeComponent();
            ShowLandlord();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hi.DESKTOP-3TRLI97\Documents\House_Rental_Management.mdf;Integrated Security=True;Connect Timeout=30");

        private void GenCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ShowLandlord()
        {
            Con.Open();
            string Query = "Select * from LandLordTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            LandlordDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ResetData()
        {
            PhoneTb.Text = "";
            GenCb.SelectedIndex = -1;
            LLNameTb.Text = "";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (LLNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LandlordTb1(LLName, LLPhone, LLGen)values(@LLN,@LLP,@LLG)", Con);
                    cmd.Parameters.AddWithValue(@"LLN", LLNameTb.Text);
                    cmd.Parameters.AddWithValue(@"LLP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue(@"LLG", GenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landlord Added!!!");
                    Con.Close();
                    ResetData();
                    ShowLandlord();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void LandlordDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LLNameTb.Text = LandlordDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = LandlordDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenCb.Text = LandlordDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (LLNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(LandlordDGV.SelectedRows[0].Cells[0].Value.ToString());
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
                    SqlCommand cmd = new SqlCommand("delete from LandLordTb1 where LLId=@Tkey", Con);
                    cmd.Parameters.AddWithValue("@Tkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landlord Deleted!!!");
                    Con.Close();
                    ResetData();
                    ShowLandlord();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LLNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update LandlordTb1 set LLName = @LLN, LLPhone = @LLP, LLGen = @LLG where LLId = @LLKey", Con);
                    cmd.Parameters.AddWithValue(@"LLN", LLNameTb.Text);
                    cmd.Parameters.AddWithValue(@"LLP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue(@"LLG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"LLKey", Key); 
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landlord Updated!!!");
                    Con.Close();
                    ResetData();
                    ShowLandlord();

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
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Apartments Obj = new Apartments();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
    }

