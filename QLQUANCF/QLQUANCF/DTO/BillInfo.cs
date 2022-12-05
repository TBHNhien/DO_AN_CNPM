using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQUANCF.DTO
{
    public class BillInfo
    {
        public BillInfo(string userName, int idBillInfo, int idBill, int idFood, int count)
        {
            this.userName = userName;
            this.idBillInfo = idBillInfo;
            this.idBill = idBill;   
            this.idFood = idFood;
            this.count = count; 
        }

        public BillInfo(DataRow row) 
        {
            this.userName = row["userName"].ToString();
            this.idBillInfo = (int)row["idBillInfo"];
            this.idBill = (int)row["idBill"];
            this.idFood = (int)row["idFood"];
            this.count = (int)row["count"];
        }

        private int count;

        private int idFood;

        private string userName;

        private int idBill;

        private int idBillInfo;

        public int IdBillInfo 
        { 
            get { return idBillInfo; }
            set { idBillInfo = value; } 
        }

        public int IdFood 
        { 
            get { return idFood; } 
            set { idFood= value; }
        }

        public int Count 
        { 
            get { return count; }
            set { count = value; }
        }

        public int IdBill 
        { 
            get { return idBill;  }
            set { idBill = value; }
        }

        public string UserName
        { 
            get { return userName; } 
            set { userName = value; }
        }
    }
}
