using QLQUANCF.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQUANCF.DTO
{
    public class Bill
    {

        public Bill(int idBill, DateTime? dateCheckIn, DateTime? dateCheckOut, int status, int discount)
        {
            this.IdBill = idBill;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;
            
        }

        public Bill(DataRow row)
        {
            this.IdBill = (int)row["idBill"];
            this.DateCheckIn = (DateTime?)row["dateCheckIn"];

            var dateCheckOutTemp = row["dateCheckOut"];
            if (dateCheckOutTemp.ToString()!="")
            this.DateCheckOut = (DateTime?)row["dateCheckOutTemp"];
            this.Status = (int)row["status"];
            this.Discount = (int)row["discount"];
        }

        private int idBill;

        private DateTime? dateCheckIn;

        private DateTime? dateCheckOut;

        private int status;

        public int IdBill
        {
            get { return idBill; }
            set { idBill = value; }
        }

        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }

        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

       

        //Bài 13 : thêm discount 
        private int discount;

        public int Discount { get => discount; set => discount = value; }
    }
}
