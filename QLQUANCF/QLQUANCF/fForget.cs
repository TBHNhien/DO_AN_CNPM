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
using Microsoft.SqlServer.Server;
using System.Net.NetworkInformation;
using QLQUANCF.DAO;

namespace QLQUANCF
{
    public partial class fForget : Form
    {
        string constr = "Data Source=.\\MSSQLSERVER2012;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
        //SqlConnection con = new SqlConnection();
        public fForget()
        {
            InitializeComponent();
        }
        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txbPhoneNumberRecover_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        bool insertPass(string userName)
        {
            return AccountDAO.Instance.insertPass(userName);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
            string userName = txbMailRecover.Text;
            if (insertPass(userName))
            {
                if (textBox1.Text == textBox2.Text)
                {
                    SqlConnection con = new SqlConnection(constr);
                    //khỏi tạo instance của class SqlCommand
                    SqlCommand cmd = new SqlCommand();
                    //mở chuỗi kết nối
                    con.Open();
                    //sử dụng thuộc tính CommandText để chỉ định tên Proc
                    cmd.CommandText = "updateAccount";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    //khai báo các thông tin của tham số truyền vào
                    cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = txbMailRecover.Text.Trim();
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = textBox1.Text;
                    //sử dụng ExecuteNonQuery để thực thi
                    cmd.ExecuteNonQuery();
                    //đóng chuỗi kết nối.
                    con.Close();
                    //thông báo thành công
                    MessageBox.Show("Thay đổi mật khẩu thành công!");
                    //đóng form forget password
                    fForget f = new fForget();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đồng bộ!");
                }  
            }
            else
            {
                MessageBox.Show("Email hoặc số điện thoại sai!");
            }
        }


        private void fForget_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
