using QLQUANCF.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLQUANCF.DAO
{
    public class AccountDAO
    {
        //tạo cấu trúc singleton
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        

        private AccountDAO() { }//đảm bảo bên ngoài k lấy được 


        //hàm public để login
        public bool Login (string userName,string passWord)
        {
            //string query = "SELECT * FROM dbo.Account WHERE UserName = N'K9' AND PassWord='1'";//truyền trực tiếp

            //dùng nối chuỗi chuyền tham số vào
            //string query = "SELECT * FROM dbo.Account WHERE UserName = N'" + userName + "' AND PassWord= N'" + passWord + "' ";//có thể bị lỗi SQL injection
            //cách 2 dùng like thay = 
            //string query = "SELECT * FROM dbo.Account WHERE UserName like N'" + userName + "' AND PassWord like N'" + passWord + "' ";

            //khắc phục SQL injection cho câu phía trên
            string query = "USP_Login @userName , @passWord ";


            DataTable result = DataProvider.Instance.ExecuteQuery(query,new object[] {userName , passWord});

            return result.Rows.Count>0;
        }





        //trả về username




        public bool insertPass (string userName)
        {
            string query = "forgetPass @username_input";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });

            return result.Rows.Count > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from account where userName = '" + userName + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public bool UpdateAcccount(string userName , string displayName ,string pass , string newPass)//phải xác định update thành công hay k
        {
            //trả về int result do nó thực hiện câu lệnh update
            int result = DataProvider.Instance.ExecuteNonQuery("EXEC USP_UPDATEACCOUNT @USERNAME , @DISPLAYNAME  , @PASSWORD , @NEWPASS ",new object[] { userName, displayName, pass, newPass });

            return result > 0;//số dòng đc thay đổi > 0
        }

        //20
        public DataTable GetListAccount ()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM dbo.Account");
        }
    }
}
