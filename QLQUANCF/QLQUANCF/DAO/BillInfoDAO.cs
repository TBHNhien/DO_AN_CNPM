using QLQUANCF.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQUANCF.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance 
        { 
            get { if (instance == null) instance=new BillInfoDAO(); return BillInfoDAO.instance; }
            set { BillInfoDAO.instance = value; } 
        }

        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int id) 
        {
            List<BillInfo> listBillInfo= new List<BillInfo>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from billinfo where idbill = " + id);

            foreach (DataRow item in data.Rows) 
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;    
        }

   

        public string GetName(int id)
        {



            DataTable data = DataProvider.Instance.ExecuteQuery("select * from billinfo where idbill = " + id);
            if (data.Rows.Count > 0)
            {
                BillInfo info = new BillInfo(data.Rows[0]);
                return info.UserName;
            }

            return null;
        }

        public void InsertBillInfo(string name ,int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @TEN , @idBill , @idFood , @count " , new object[] { name , idBill, idFood, count });
        }

        //19

        //xóa bản con trước khi xóa bản cha
        public void DeleteBillInfoByFoodID (int idFood)
        {
            DataProvider.Instance.ExecuteQuery("Delete billinfo where idFood = " + idFood);
        }
    }
}
