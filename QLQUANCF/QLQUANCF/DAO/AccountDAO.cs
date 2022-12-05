using QLQUANCF.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            //22
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(passWord); // lấy ra mảng kiểu byte từ chuỗi
            byte[] hasData =  new MD5CryptoServiceProvider().ComputeHash(temp);// có đư   ợc mật khẩu đoạn mã hóa băm ra

            string hasPass = "";

            foreach (byte item in hasData)
            {
                hasPass += item;
            }    

            //--end22

            //string query = "SELECT * FROM dbo.Account WHERE UserName = N'K9' AND PassWord='1'";//truyền trực tiếp

            //dùng nối chuỗi chuyền tham số vào
            //string query = "SELECT * FROM dbo.Account WHERE UserName = N'" + userName + "' AND PassWord= N'" + passWord + "' ";//có thể bị lỗi SQL injection
            //cách 2 dùng like thay = 
            //string query = "SELECT * FROM dbo.Account WHERE UserName like N'" + userName + "' AND PassWord like N'" + passWord + "' ";

            //khắc phục SQL injection cho câu phía trên
            string query = "USP_Login @userName , @passWord ";


            DataTable result = DataProvider.Instance.ExecuteQuery(query,new object[] {userName , hasPass });

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

        //21
        public bool InsertAccount(string name, string displayName , int type)
        {
            string query = string.Format("INSERT ACCOUNT (USERNAME , DISPLAYNAME , TYPE , PASSWORD ) VALUES ( N'{0}' , N'{1}' , {2} , '{3}' )", name, displayName, type , "1962026656160185351301320480154111117132155"); //22

            //string query = string.Format("INSERT ACCOUNT (USERNAME , DISPLAYNAME , TYPE ) VALUES ( N'{0}' , N'{1}' , {2} )", name , displayName , type);//*** string.Format
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateAccount( string name, string displayName, int type)
        {
            string query = string.Format("update ACCOUNT SET DISPLAYNAME = N'{1}' , TYPE ={2} WHERE USERNAME = N'{0}' ", name , displayName , type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteAccount(string name)
        {


            string query = string.Format("DELETE ACCOUNT where USERNAME = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        
        public bool ResetPassWord (string name)
        {
            //22 
            string query = string.Format("UPDATE ACCOUNT SET PASSWORD = N'1962026656160185351301320480154111117132155' where USERNAME = N'{0}'", name);

            //string query = string.Format("UPDATE ACCOUNT SET PASSWORD = '0' where USERNAME = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
