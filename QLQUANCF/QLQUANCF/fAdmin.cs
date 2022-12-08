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

        //20
        BindingSource accountList = new BindingSource();
        //-- end 20

        //foodcategoryList
        BindingSource foodcategoryList = new BindingSource();

        //TableList
        BindingSource TableList = new BindingSource();

        //truyền account hiện tại để không đc xóa //21
        public Account loginAccount;
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
            dtgvAccount.DataSource = accountList;//20
            dtgvCategory.DataSource = foodcategoryList;
            dtgvTable.DataSource = TableList;
            LoadListFood();
            LoadAccount();
            LoadFoodCategory();
            LoadTableList();
            LoadCategoryIntoCombobox(cbCategory);
            AddFoodBinding();
            AddAccountBinding();
            AddFoodCategory();
            AddTableList();
        }

        //20
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));

        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void AddFoodCategory()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "idfoodcategory", true, DataSourceUpdateMode.Never));
            txbCategory.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "name", true, DataSourceUpdateMode.Never));
        }

        void LoadFoodCategory()
        {
            foodcategoryList.DataSource = CategoryDAO.Instance.GetListFoodCategory();
        }

        //TableList
        void AddTableList()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "idTableFood", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "name", true, DataSourceUpdateMode.Never));
            cbbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "status", true, DataSourceUpdateMode.Never));
        }

        void LoadTableList()
        {
            TableList.DataSource = TableDAO.Instance.GetLoadTableList();
        }

        //kỹ thuật biding -> dữ liệu thay đổi khi thằng này thay đổi thằng kia thay đổi theo
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));//từ txbFoodName -> thay đổi giá trị "text" thay đổi theo "Name" nằm trong dtgvFood.DataSource
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)//sửa chữa ngay thay hàm ComboBox (System.Windows.Forms.ComboBox)
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
            add { insertFood += value; }
            remove { insertFood -= value; }
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

        string constr = "Data Source=.\\MSSQLSERVER2012;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
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


        //20
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void txbUserName_TextChanged(object sender, EventArgs e)
        {

        }


        //21

        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công !!!");
            }
            else
            {
                MessageBox.Show("ADD ACOUNT FAILSE !!!");
            }

            LoadAccount();
        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show(" Sửa tài khoản thành công !!!");
            }
            else
            {
                MessageBox.Show("EDIT ACCOUNT FAILSE !!!");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Tài khoản đang được sử dụng !!! DELETE FAIL");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show(" Xóa tài khoản thành công !!!");
            }
            else
            {
                MessageBox.Show("DELETE ACCOUNT FAILSE !!!");
            }

            LoadAccount();
        }

        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassWord(userName))
            {
                MessageBox.Show(" Đặt lại MK mặc định thành công !!!");
            }
            else
            {
                MessageBox.Show("Reset Password ACCOUNT FAILSE !!!");
            }
        }



        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmAccountType.Value;

            AddAccount(userName, displayName, type);

        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmAccountType.Value;

            EditAccount(userName, displayName, type);

        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);

        }



        private void button2_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            ResetPass(userName);
        }

        private void panel29_Paint(object sender, PaintEventArgs e)
        {
            string userName = Properties.Settings.Default.username;
            txbStoreUserName.Text = userName;
        }

        private void tbStore_Click(object sender, EventArgs e)
        {

        }

        private void addDeleteEditStore(string query)
        {
            SqlConnection conn = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                da.SelectCommand = cmd;
                da.SelectCommand.CommandText = query;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter
                {
                    ParameterName = "@USERNAME",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = txbStoreUserName.Text,
                    Direction = ParameterDirection.Input,

                };
                SqlParameter param2 = new SqlParameter
                {
                    ParameterName = "@MATERIAL",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = txbMaterial.Text,
                    Direction = ParameterDirection.Input,

                };
                SqlParameter param3 = new SqlParameter
                {
                    ParameterName = "@DATEIN",
                    SqlDbType = SqlDbType.Date,
                    Value = dtpStoreDateIn.Text,
                    Direction = ParameterDirection.Input,

                };
                SqlParameter param4 = new SqlParameter
                {
                    ParameterName = "@DATEEXPRIRED",
                    SqlDbType = SqlDbType.Date,
                    Value = dtpDateExprired.Text,
                    Direction = ParameterDirection.Input,

                };

                SqlParameter param5 = new SqlParameter
                {
                    ParameterName = "@PRICEIN",
                    SqlDbType = SqlDbType.Float,
                    Value = txbStorePriceIn.Text,
                    Direction = ParameterDirection.Input,

                };
                SqlParameter param6 = new SqlParameter
                {
                    ParameterName = "@AMOUNT",
                    SqlDbType = SqlDbType.Int,
                    Value = nmStoreAmount.Text,
                    Direction = ParameterDirection.Input,

                };
                SqlParameter param7 = new SqlParameter
                {
                    ParameterName = "@CATEGORY",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = txbStoreCategory.Text,
                    Direction = ParameterDirection.Input,

                };
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);
                cmd.Parameters.Add(param4);
                cmd.Parameters.Add(param5);
                cmd.Parameters.Add(param6);
                cmd.Parameters.Add(param7);
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                dtgvStore.DataSource = dt;
                conn.Close();
                dtgvStore.Columns[0].Width = 150;
                dtgvStore.Columns[0].HeaderText = "Tên Người Nhập";
                dtgvStore.Columns[1].Width = 120;
                dtgvStore.Columns[1].HeaderText = "Tên Nguyên Liệu";
                dtgvStore.Columns[2].Width = 120;
                dtgvStore.Columns[2].HeaderText = "Ngày Nhập";
                dtgvStore.Columns[3].Width = 120;
                dtgvStore.Columns[3].HeaderText = "Ngày Hết Hạn";
                dtgvStore.Columns[4].Width = 120;
                dtgvStore.Columns[4].HeaderText = "Giá Thành";
                dtgvStore.Columns[5].Width = 120;
                dtgvStore.Columns[5].HeaderText = "Số Lượng";
                dtgvStore.Columns[6].Width = 120;
                dtgvStore.Columns[6].HeaderText = "Thông tin";

                dtgvStore.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddStore_Click(object sender, EventArgs e)
        {
            string query = "USP_ADDSTORE";
            addDeleteEditStore(query);
        }

        private void btnDeleteStore_Click(object sender, EventArgs e)
        {
            string query = "USP_DELETESTORE";
            addDeleteEditStore(query);
        }

        private void btnEditStore_Click(object sender, EventArgs e)
        {
            string query = "USP_EDITSTORE";
            addDeleteEditStore(query);
        }

        private void showStore()
        {
            SqlConnection conn = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                da.SelectCommand = cmd;
                da.SelectCommand.CommandText = "USP_SHOWSTORE";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                dtgvStore.DataSource = dt;
                conn.Close();
                dtgvStore.Columns[0].Width = 150;
                dtgvStore.Columns[0].HeaderText = "Tên Người Nhập";
                dtgvStore.Columns[1].Width = 120;
                dtgvStore.Columns[1].HeaderText = "Tên Nguyên Liệu";
                dtgvStore.Columns[2].Width = 120;
                dtgvStore.Columns[2].HeaderText = "Ngày Nhập";
                dtgvStore.Columns[3].Width = 120;
                dtgvStore.Columns[3].HeaderText = "Ngày Hết Hạn";
                dtgvStore.Columns[4].Width = 120;
                dtgvStore.Columns[4].HeaderText = "Giá Thành";
                dtgvStore.Columns[5].Width = 120;
                dtgvStore.Columns[5].HeaderText = "Số Lượng";
                dtgvStore.Columns[6].Width = 120;
                dtgvStore.Columns[6].HeaderText = "Thông tin";

                dtgvStore.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /*
        void addStoreList()
        {
            txbStoreUserName.DataBindings.Add(new Binding("Text", dtgvStore.DataSource, "UserName", true, DataSourceUpdateMode.Never));//từ txbFoodName -> thay đổi giá trị "text" thay đổi theo "Name" nằm trong dtgvFood.DataSource
            txbMaterial.DataBindings.Add(new Binding("Text", dtgvStore.DataSource, "Material", true, DataSourceUpdateMode.Never));
            dtpStoreDateIn.DataBindings.Add(new Binding("Date", dtgvStore.DataSource, "DateIn", true, DataSourceUpdateMode.Never));
            dtpDateExprired.DataBindings.Add(new Binding("Date", dtgvStore.DataSource, "Dateeprired", true, DataSourceUpdateMode.Never));
            txbStorePriceIn.DataBindings.Add(new Binding("Float", dtgvStore.DataSource, "priceIn", true, DataSourceUpdateMode.Never));
            nmStoreAmount.DataBindings.Add(new Binding("int", dtgvStore.DataSource, "amount", true, DataSourceUpdateMode.Never));
            txbStoreCategory.DataBindings.Add(new Binding("Text", dtgvStore.DataSource, "category", true, DataSourceUpdateMode.Never));
        }
        */
        private void btnShowStore_Click(object sender, EventArgs e)
        {
            showStore();
        }

        private void dtgvStore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txbStoreUserName.Text = dtgvStore.CurrentRow.Cells["UserName"].Value.ToString();
            txbMaterial.Text = dtgvStore.CurrentRow.Cells["Material"].Value.ToString();
            dtpStoreDateIn.Text = dtgvStore.CurrentRow.Cells["DateIn"].Value.ToString();
            dtpDateExprired.Text = dtgvStore.CurrentRow.Cells["Dateexpired"].Value.ToString();
            txbStorePriceIn.Text = dtgvStore.CurrentRow.Cells["priceIn"].Value.ToString();
            nmStoreAmount.Text = dtgvStore.CurrentRow.Cells["amount"].Value.ToString();
            txbStoreCategory.Text = dtgvStore.CurrentRow.Cells["category"].Value.ToString();
        }


        //foodcategoryList

        void AddFoodCategory(string name)
        {
            if (CategoryDAO.Instance.InsertFoodCategory(name))
            {
                MessageBox.Show("Thêm danh sách món thành công !!!");
            }
            else
            {
                MessageBox.Show("ADD FOODCATEGORY FAILSE !!!");
            }

            LoadFoodCategory();
        }
        void UpdateCategory(int idFoodCategory, string name)
        {
            if (CategoryDAO.Instance.UpdateFoodCategory(idFoodCategory, name))
            {
                MessageBox.Show("Sửa danh sách món thành công !!!");
            }
            else
            {
                MessageBox.Show("EDIT FOODCATEGORY FAILSE !!!");
            }

            LoadFoodCategory();
        }

        void DeleteCategory(int id)
        {
            if (CategoryDAO.Instance.DeleteFoodCategory(id))
            {
                MessageBox.Show("Xóa danh sách món thành công !!!");
            }
            else
            {
                MessageBox.Show("DELETE FOODCATEGORY FAILSE !!!");
            }

            LoadFoodCategory();
        }

    //
        private void btnAddCategory_Click_1(object sender, EventArgs e)
        {
            string name = txbCategory.Text;
            AddFoodCategory(name);
        }

        private void btnEditCategory_Click_1(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);
            string name = txbCategory.Text;
            UpdateCategory(id, name);
        }

        private void btnDeleteCategory_Click_1(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);
            DeleteCategory(id);
        }
    }
}