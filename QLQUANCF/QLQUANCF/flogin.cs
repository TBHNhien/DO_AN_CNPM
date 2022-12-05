using QLQUANCF.DAO;
using QLQUANCF.DTO;

namespace QLQUANCF
{
    public partial class flogin : Form
    {
        public flogin()
        {
            InitializeComponent();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /* bai 3
        private void btnLogin_Click(object sender, EventArgs e)
        {
            fTableManager f = new fTableManager();
            //f.Show();//cách 1 : vẫn hiện bản login cũ
            //cách 2 :
            this.Hide();//ẩn login hiện tại
            f.ShowDialog();
            this.Show();
        }*/
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string PassWord = txbPassWord.Text;

            Properties.Settings.Default.username= userName;
            Properties.Settings.Default.Save();

            if (Login(userName , PassWord))
            {
             Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
             fTableManager f = new fTableManager(loginAccount);//khai báo cửa sổ tiếp theo sau khi đăng nhập
             this.Hide();
             f.ShowDialog();
             this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
            } 
                
        } 
        
        bool Login (string userName , string PassWord)
        {
            return AccountDAO.Instance.Login(userName , PassWord); 
        }


            private void flogin_Load(object sender, EventArgs e)
        {

        }

        private void flogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát ???","Thông báo",MessageBoxButtons.OKCancel)!=System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }    
        }

        private void txbPassWord_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbUserName_TextChanged(object sender, EventArgs e)
        {

        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnForgetPass_Click(object sender, EventArgs e)
        {
            fForget f = new fForget();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}