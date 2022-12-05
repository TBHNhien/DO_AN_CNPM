using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQUANCF.DTO
{
     public class Menu
    {
        public Menu (string foodnName,int count,float price ,float totalPrice = 0)
        {
            this.foodName = foodName;
            this.count = count;
            this.price = price;
            this.totalPrice = totalPrice;
        }

        //lấy cả List<Menu>
        public Menu(DataRow row)
        {
            this.foodName =row["NAME"].ToString();
            this.count = (int)row["count"];
            this.price = (float)Convert.ToDouble(row["price"].ToString());//chuyển qua string xong qua double xong qua float
            this.totalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }

        private float totalPrice;

        private float price;

        private int count;

        private string foodName;

        public string FoodName 
        { 
            get { return foodName; }
            set { foodName = value; } 
        }

        public int Count 
        { 
            get { return count; }
            set { count = value; } 
        }

        public float Price 
        {
            get { return price; }
            set { price = value; }
        }

        public float TotalPrice 
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
    }
}
