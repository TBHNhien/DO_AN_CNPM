using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQUANCF.DTO
{
    public class Food
    {
        public Food(int id,string name , int idFoodCategory,float price)
        { 
            this.ID= id; 
            this.Name= name;
            this.IDFoodCategory= idFoodCategory;
            this.Price= price;
        } 

        public Food(DataRow row) 
        {
            this.ID = (int)row["idFood"];
            this.Name = row["name"].ToString();
            this.IDFoodCategory = (int)row["idFoodCategory"];
            this.Price =(float)Convert.ToDouble(row ["price"].ToString());
        }

        private float price;

        private int iDFoodCategory;

        private string name;

        private int iD;

        public int ID 
        {
            get { return iD; }
            set { iD = value; }
        }

        public string Name { get => name; set => name = value; }
        public int IDFoodCategory { get => iDFoodCategory; set => iDFoodCategory = value; }
        public float Price { get => price; set => price = value; }
    }
}
