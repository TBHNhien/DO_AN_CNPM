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

        //
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
    }
}
