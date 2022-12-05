using QLQUANCF.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQUANCF.DAO
{

    public class FoodDAO
    {
        //TẠO SINGLETON
        private static FoodDAO instance;

        public static FoodDAO Instance 
        {
            get { if(instance==null)instance = new FoodDAO(); return instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        //
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();

            string query = "select * from food where idFoodCategory = " + id ;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows) 
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

        //HIỂN THỊ DS THỨC ĂN FADMIN

        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();

            string query = "select * from food ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

        //19
        public bool InsertFood (string name ,int idfoodcategory , float price)
        {
            string query = string.Format ("INSERT FOOD (NAME , IDFOODCATEGORY , PRICE ) VALUES (N'{0}' , {1} , {2} )" , name , idfoodcategory , price) ;//*** string.Format
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateFood( int idFood , string name, int idfoodcategory, float price)
        {
            string query = string.Format("update dbo.Food SET name = N'{0} ' , idFoodCategory = {1} , price ={2} WHERE idFood = {3} ", name, idfoodcategory , price , idFood );
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteFood(int idFood)
        {
            //trước khi DeleteFood được phải xóa đc bản con
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);

            string query = string.Format("DELETE Food where idFood = {0}" , idFood );
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        //19
        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();

            string query = string.Format("SELECT * FROM Food WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }


        //FoodCategory
        public void DeleteFoodByIDFoodCategory(int idFoodCategory)
        {
            DataProvider.Instance.ExecuteQuery("Delete food where idfoodcategory = " + idFoodCategory);
        }
    }
}
