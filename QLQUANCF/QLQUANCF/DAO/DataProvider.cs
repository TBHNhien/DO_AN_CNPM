using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQUANCF.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;//tạo 1 đối tượng static //lấy ra duy nhất//đóng gói ctr+R+E

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set => instance = value;//chỉ nội bộ (private) mới lấy được dữ liệu
        }
        private DataProvider() { }//đảm bảo bên ngoài ko tác động được , chỉ lấy ra thôi

        private string connectionSTR = "Data Source=.\\MSSQLSERVER2012;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";


        //truyền vào parameter là stringid c1 ,đưa 1 mảng parameter vào c2
        //public DataTable ExecuteQuery(string query, string id) c1
        //c2 lưu ý null phải để cuối 


        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //đưa parameter vào c1 
                //command.Parameters.AddWithValue("@userName", id);

                //c2 đưa n Parameters vào
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;
                    //cho mỗi item trong listPara
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))// nếu item có chứa (Contains) @ 
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }   

        
            //trả về số dòng thành công
            public int ExecuteNonQuery(string query, object[] parameter = null)
            {
            int data = 0;

                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    //c2 đưa n Parameters vào
                    if (parameter != null)
                    {
                        string[] listPara = query.Split(' ');

                        int i = 0;
                        //cho mỗi item trong listPara
                        foreach (string item in listPara)
                        {
                            if (item.Contains('@'))// nếu item có chứa (Contains) @ 
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }
                    data = command.ExecuteNonQuery();
                    connection.Close();
                }
                return data;
            }
        

        //trả về số lượng (vd:count(*))
        public object ExecuteScalar(string query, object[] parameter = null)//trả về 1 object
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //c2 đưa n Parameters vào
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;
                    //cho mỗi item trong listPara
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))// nếu item có chứa (Contains) @ 
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
                connection.Close();
            }
            return data;
        }
    }
}
