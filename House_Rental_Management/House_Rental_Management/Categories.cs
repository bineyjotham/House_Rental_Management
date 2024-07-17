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
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
            ShowCategories();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hi.DESKTOP-3TRLI97\Documents\House_Rental_Management.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowCategories()
        {
            Con.Open();
            string Query = "Select * from CategoryTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CategoriesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            CategorieTb.Text = "";
            RemarksTb.Text = "";
            DataSet ds = new DataSet();
        }
        private void Categories_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(CategorieTb.Text == "" || RemarksTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CategoryTb1(Category,Remarks)values(@Cat,@Rem)", Con);
                    cmd.Parameters.AddWithValue("@Cat", CategorieTb.Text);
                    cmd.Parameters.AddWithValue("@Rem", RemarksTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Added!!!");
                    Con.Close();
                    ResetData();
                    ShowCategories();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
