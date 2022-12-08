using QLQUANCF.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQUANCF.DAO
{
    public class CategoryDAO
    {
        //tạo sigleton

        private static CategoryDAO instance;

        public static CategoryDAO Instance 
        { 
            get { if(instance == null) instance= new CategoryDAO(); return instance; }
            private set { CategoryDAO.instance= value; } 
        }

        private CategoryDAO() { }

        
        public List<Category> GetListCategory() 
        {
            List<Category> list = new List<Category>();

            string query = "select * from FoodCategory";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows) 
            {
                Category category = new Category(item);
                list.Add(category);
            }

            return list;
        }

        //17 
        //lấy category đúng

        public Category GetCategoryByID (int id)
        {
            Category category = null;

            List<Category> list = new List<Category>();

            string query = "select * from FoodCategory where idFoodCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }

            return category;
        }

        //20
        public DataTable GetListFoodCategory()
        {
            return DataProvider.Instance.ExecuteQuery("select idfoodcategory , name  from foodcategory");
        }

        //21
       
        public bool InsertFoodCategory(string name)
        {
            string query = string.Format("INSERT foodcategory ( name ) VALUES ( N'{0}')" , name ); //22

            //string query = string.Format("INSERT ACCOUNT (USERNAME , DISPLAYNAME , TYPE ) VALUES ( N'{0}' , N'{1}' , {2} )", name , displayName , type);//*** string.Format
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateFoodCategory(int idFoodCategory, string name)
        {
            string query = string.Format("update foodcategory SET name = N'{1}' WHERE idFoodCategory = N'{0}' ", idFoodCategory , name );
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteFoodCategory(int id)
        {
            BillInfoDAO.Instance.DeleteBillInfoByIDFoodCategory(id);
            FoodDAO.Instance.DeleteFoodByIDFoodCategory(id);

            string query = string.Format("DELETE foodcategory where idfoodcategory = '{0}'", id );
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
       
    }
}
