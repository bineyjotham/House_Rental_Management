using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace House_Rental_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Reset()
        {
            UsernameTb.Text = "";
            PasswordTb.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(UsernameTb.Text == ""||  PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Both Username and Password!!!");
                Reset();
            }else if(UsernameTb.Text =="Admin" &&  PasswordTb.Text == "Admin")
            {
                Tenants Obj = new Tenants();
                Obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password!!!");
                Reset();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
