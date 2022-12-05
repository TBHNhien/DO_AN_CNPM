using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//luu tru thong tin va thuoc tinh cua table
namespace QLQUANCF.DTO
{
     public class Table
    {


        private int idTableFood;
        public int IdTableFood
        {
            get { return idTableFood; }
            set { idTableFood = value; }
        }

        private string name;
        public String Name 
        {
            get { return name; }
            set { name = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        //tao hang dung
        public Table (int idTableFood,string name,string status)
        {
            this.idTableFood= idTableFood;
            this.name= name;
            this.status= status;    
        }

        //chuyen tung row thanh list trong table DTO
        public Table (DataRow row)
        {
            this.IdTableFood = (int)row["idTableFood"];
            this.Name= row["Name"].ToString();
            this.Status = row["status"].ToString();
        }
    }
}
