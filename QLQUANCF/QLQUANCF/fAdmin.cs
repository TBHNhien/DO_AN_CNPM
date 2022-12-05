using QLQUANCF.DAO;
using QLQUANCF.DTO;
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

namespace QLQUANCF
{
    public partial class fAdmin : Form
    {
        //dùng biding source -> tránh mất data source
        BindingSource foodList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();

            Load2();

            // LoadAccountList();
            //dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery("EXEC USP_GetAccountByUserName @userName", new object[] { "'' OR 1=1--" });// sử dụng STORED PROC sẽ không bị lỗi SQL injection
            //dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Account WHERE UserName= N'' OR 1=1--");//sử dụng select bình thường sẽ bị SQL injection
        }





        #region methods

        void Load2()
        {
            dtgvFood.DataSource = foodList;
            LoadListFood();
            LoadCategoryIntoCombobox(cbCategory);
            AddFoodBinding();
        }

        //kỹ thuật biding -> dữ liệu thay đổi khi thằng này thay đổi thằng kia thay đổi theo
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));//từ txbFoodName -> thay đổi giá trị "text" thay đổi theo "Name" nằm trong dtgvFood.DataSource
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        #endregion





        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void account_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        /*
        void LoadAccountList()
        {
            //string connectionSTR = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

            //string query = "SELECT DisplayName as [Tên Hiển Thị] FROM dbo.Account";

            //string query = "EXEC USP_GetAccountByUserName @userName=N'K9'";

            //sau khi truyền parameter c1
            //string query = "EXEC USP_GetAccountByUserName @userName";
            //c2
            string query = "EXEC USP_GetAccountByUserName @userName ";

            //DataProvider provider = new DataProvider();

            //truyền parameter vào là "K9"c1
            //dtgvAccount.DataSource = provider.ExecuteQuery(query,"K9");
            //c2
            //dtgvAccount.DataSource = provider.ExecuteQuery(query,new object[] { "staff"} );

            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { "staff" });

        }

        void LoadFoodList()
        {
            string query = "select * from food";
            dtgvFood.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
        */

        private void dtgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["IDFoodCategory"].Value;

                Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                cbCategory.SelectedItem = cateogory;

                int index = -1;
                int i = 0;
                foreach (Category item in cbCategory.Items)
                {
                    if (item.ID == cateogory.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }

                cbCategory.SelectedIndex = index;
            }
        }

        //19
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công !!!");
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm món !!!");
            }

        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int idfood = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(idfood, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công !!!");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Lỗi khi sửa món !!!");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {

            int idfood = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(idfood))
            {
                MessageBox.Show("Xóa món thành công !!!");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Lỗi khi sửa món !!!");
            }
        }


        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add  {insertFood += value;}
            remove { insertFood -= value;}
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        string constr = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
        private void getTotalBill()
        {
            //khởi tạo các đối tượng SqlConnection, SqlDataAdapter, DataTable
            SqlConnection conn = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            //lấy chuỗi kết nối từ file App.config
            try
            {
                //mở chuỗi kết nối
                conn.Open();
                //khởi tạo đối tượng SqlCommand
                SqlCommand cmd = new SqlCommand();
                //khai báo đối tượng SqlCommand trong SqlDataAdapter
                da.SelectCommand = cmd;
                //gọi thủ tục từ SQL
                da.SelectCommand.CommandText = "SP_TOTALBILL";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //khai báo các thuộc tính của tham số
                SqlParameter param1 = new SqlParameter
                {
                    ParameterName = "@FROMDATE",
                    SqlDbType = SqlDbType.Date,
                    Value = dtpkFromDate.Text,
                    Direction = ParameterDirection.Input,

                };
                SqlParameter param2 = new SqlParameter
                {
                    ParameterName = "@TODATE",
                    SqlDbType = SqlDbType.Date,
                    Value = dtpkToDate.Text,
                    Direction = ParameterDirection.Input,

                };
                //thêm các tham số vào đối tượng SqlCommand
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                //gán chuỗi kết nối
                da.SelectCommand.Connection = conn;
                //sử dụng phương thức fill để điền dữ liệu từ datatable vào SqlDataAdapter
                da.Fill(dt);
                //gán dữ liệu từ datatable vào datagridview
                dataGridView1.DataSource = dt;
                //đóng chuỗi kết nối
                conn.Close();
                //sử dụng thuộc tính Width và HeaderText để set chiều dài và tiêu đề cho các coloumns
                dataGridView1.Columns[0].Width = 120;
                dataGridView1.Columns[0].HeaderText = "DateCheckIn";
                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[1].HeaderText = "DateCheckOut";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Tổng tiền";
                //xóa cột mặt định bên trái
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getStatisticalStore()
        {
            SqlConnection conn = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                da.SelectCommand = cmd;
                da.SelectCommand.CommandText = "SP_STATISTICALSTORE";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter
                {
                    ParameterName = "@DATEIN",
                    SqlDbType = SqlDbType.Date,
                    Value = dtpkFromDate.Text,
                    Direction = ParameterDirection.Input,

                };
                cmd.Parameters.Add(param1);
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                conn.Close();
                dataGridView2.Columns[0].Width = 150;
                dataGridView2.Columns[0].HeaderText = "Ngày Nhập Hàng";
                dataGridView2.Columns[1].Width = 120;
                dataGridView2.Columns[1].HeaderText = "Tổng tiền";
                dataGridView2.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            getTotalBill();
            getStatisticalStore();
        }


        //19

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }
    }
}
