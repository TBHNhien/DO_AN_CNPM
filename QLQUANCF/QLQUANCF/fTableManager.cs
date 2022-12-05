using QLQUANCF.DAO;
using QLQUANCF.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QLQUANCF.fAccountProfile;

namespace QLQUANCF
{
    public partial class fTableManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount 
        {
            get { return loginAccount; }
            set { loginAccount = value; ChageAccount(loginAccount.Type); }
         }

       

        public fTableManager(Account acc)
        {
            InitializeComponent();

            this.LoginAccount= acc;


            LoadTable();

            LoadCategory();

            LoadComboboxTable(cbSwitchTable);
        }


        #region Method

        void ChageAccount (int type)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        void LoadTable ()
        {
            flpTable.Controls.Clear(); // xóa tránh bị thêm bàn

          List<Table> tableList = TableDAO.Instance.LoadTableList ();

          foreach (Table item in tableList)
            {
                Button btn = new Button() { Width=TableDAO.TableWidth,Height= TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor=Color.Silver; 
                        break;
                    default:
                        btn.BackColor=Color.Red;
                        break;  

                }

                flpTable.Controls.Add(btn);

            }
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cb1Category.DataSource = listCategory;

            //cần chỉ cho nó biết trường nào để lấy tên
            cb1Category.DisplayMember= "Name";
        }

        void LoadFoodListByCategoryID ( int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;

            //cần chỉ cho nó biết trường nào để lấy tên
            cbFood.DisplayMember= "Name";
        }

        void ShowBill(int id)
        {
            //clear bàn
            lsvBill.Items.Clear();

            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);

            float totalPrice = 0;

            foreach (Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                //NHỮNG thằng sau đều là sub item

                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");//ĐẢM bảo luôn tiếng việt

            Thread.CurrentThread.CurrentCulture = culture;//culture c1

            txbTotalPrice.Text = totalPrice.ToString("c");

            //txbTotalPrice.Text = totalPrice.ToString("c",culture);//culter c2
        }
        //B13
        void LoadComboboxTable (ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }


        #endregion








        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).IdTableFood;

            //sử dụng lsv Tag => mỗi khi click sẽ có Tag bàn đó
            lsvBill.Tag = (sender as Button).Tag;

            ShowBill(tableID);
        }
        private void cb1Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            //lấy id
            ComboBox cb = sender as ComboBox;

            //TH chưa có datasource
            if (cb.SelectedItem == null)
                return;

            //TH đã có datasource
            Category selected = cb.SelectedItem as Category;

            id = selected.ID;

            LoadFoodListByCategoryID(id);
        }

        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //xác định bill hiện tại là thằng nào
            Table table = lsvBill.Tag as Table;//lấy được table

            //lấy idbill
            int idbill = BillDAO.Instance.GetUncheckBillIDByTableID(table.IdTableFood);
            int discount = (int)nmDisCount.Value;
            float totalPrice = (float)Convert.ToDouble(txbTotalPrice.Text.Split(' ')[0]);
            float finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idbill != -1) //bill này có
            {
                if ( MessageBox.Show(string.Format("Bạn muốn thanh toán bàn {0}\n Tổng tiền - (Tổng tiền / 100) x Giảm giá \n => {1} - ({1} /100 ) x {2} = {3}" ,table.Name , totalPrice , discount ,finalTotalPrice) ,"Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idbill, discount);
                    ShowBill(table.IdTableFood);
                    LoadTable();
                } 
            } 
                
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            fAccountProfile f = new fAccountProfile(loginAccount);

            f.UpdateAccount += f_UpdateAccount;

            //f.Show();//cách 1 : vẫn hiện bản login cũ
            //cách 2 :
            //this.Hide();//ẩn login hiện tại
            f.ShowDialog();
            //this.Show();
        }

        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }

        private void đăngXuâtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            fAdmin f = new fAdmin();
            
            //lấy thông tin account truyền qua fAdmin //21
            f.loginAccount= loginAccount;

            f.InsertFood += F_InsertFood;
            f.DeleteFood += F_DeleteFood;
            f.UpdateFood += F_UpdateFood;
            f.ShowDialog();
        }

        private void F_UpdateFood(object? sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cb1Category.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).IdTableFood);
        }

        private void F_DeleteFood(object? sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cb1Category.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).IdTableFood);
            LoadTable();
        }

        private void F_InsertFood(object? sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cb1Category.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).IdTableFood);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            //lấy table hiện tại
            Table table = lsvBill.Tag as Table;

            if (table == null) 
            {
                MessageBox.Show("Hãy Chọn Bàn");
                return;
            }

            //lấy idbill hiện tại
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.IdTableFood);
            int idFood = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;//kiểu decimal => ép kiểu int

            string ten = Properties.Settings.Default.username;



            //nếu idbill ==-1 ko có bill nào hết => thêm bill mới
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.IdTableFood);
                BillInfoDAO.Instance.InsertBillInfo(ten, BillDAO.Instance.GetMaxIDBill(), idFood, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(ten, idBill, idFood, count);
            }
            ShowBill(table.IdTableFood);
            LoadTable();
        }

        private void btnTranferTable_Click(object sender, EventArgs e)
        {    

            int id1 = (lsvBill.Tag as Table).IdTableFood;

            int id2 = (cbSwitchTable.SelectedItem as Table).IdTableFood;

            if (MessageBox.Show(String.Format("Bạn có muốn chuyển bàn {0} qua bàn {1}", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thong bao" ,MessageBoxButtons.OKCancel)==System.Windows.Forms.DialogResult.OK);

            TableDAO.Instance.SwitchTable(id1, id2);

            LoadTable();
        }

        #endregion

        private void fTableManager_Load(object sender, EventArgs e)
        {
            if (loginAccount.Type== 0) 
            {
                adminToolStripMenuItem.Enabled = false;
            }
            
        }
        

        //23 gọi thẳng event của nút thanh toán và thêm món
        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click (this , new EventArgs());

        }

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }
    }
}
