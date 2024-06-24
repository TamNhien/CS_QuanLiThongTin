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

namespace CS_QuanLiThongTin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAB1-MAY15\MISASME2022;Initial Catalog=QuanLiThongTin;Integrated Security=True;Encrypt=False");
        private void openCon()
        {
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
        }

        private void closeCon()
        {
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private Boolean Exe(string cmd)
        {
            openCon();
            Boolean check;
            try
            {
                SqlCommand sc = new SqlCommand(cmd, con);
                sc.ExecuteNonQuery();
                check = true;
            }
            catch (Exception)
            {
                check = false;
                throw;
            }
            closeCon();
            return check;
        }

        private DataTable Red(string cmd)
        {
            openCon();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand sc = new SqlCommand(cmd, con);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(dt);
            }
            catch (Exception)
            {
                dt = null;
                throw;
            }
            closeCon();
            return dt;
        }

        private void load()
        {
            DataTable dt = Red("SELECT * FROM QUANLITHONGTIN");
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            load();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            maTen.ResetText();
            hoTen.ResetText();
            namSinh.ResetText();
            queQuan.ResetText();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Exe("INSERT INTO QUANLITHONGTIN(MATT, HOTEN, NAMSINH, QUEQUAN) VALUES (N'" + maTen.Text + "', N'" + hoTen.Text + "', N'" + namSinh.Text + "', N'" + queQuan.Text + "') ");
            //Exe("INSERT INTO QUANLITHONGTIN(MATT, HOTEN, NAMSINH, QUEQUAN) VALUES (N'ma',N'ten',N'nam',N'que') ");
            load();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            load();
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            Exe("UPDATE QUANLITHONGTIN SET HOTEN = N'" + hoTen.Text + "', NAMSINH =  N'" + namSinh.Text + "', QUEQUAN = N'" + queQuan.Text + "' WHERE MATT = '" + maTen.Text + "' ");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            maTen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            hoTen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            namSinh.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            queQuan.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Exe("DELETE FROM QUANLITHONGTIN WHERE MATT = '" + maTen.Text + "'");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = Red("SELECT * FROM QUANLITHONGTIN WHERE MATT = '" + tentimkiem.Text + "' ");
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }
    }
}
