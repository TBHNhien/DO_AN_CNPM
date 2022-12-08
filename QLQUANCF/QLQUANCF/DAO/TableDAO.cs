using QLQUANCF.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQUANCF.DAO
{
    public class TableDAO
    {
        //tao singleton
        private static TableDAO instance;

        public static TableDAO Instance 
        { 
            get { if (instance == null) instance= new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }
        public static int TableWidth = 80;
        public static int TableHeight = 80;
        private TableDAO() { }

        //
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table> ();

            //lay table len tu query
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            //tu datatable chuyen thanh list<table>
            //chuyen tung row thanh list trong table DTO

            foreach (DataRow item in data.Rows) 
            {
            Table table= new Table(item);
            tableList.Add(table);
            }

             return tableList;   

        }
        public void SwitchTable(int id1,int id2)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_SWITCHTABLE @idtabl1 , @idtable2",new object[] {id1,id2});
        }

        public DataTable GetLoadTableList()
        {
            return DataProvider.Instance.ExecuteQuery("select idTableFood , name , status from tablefood");
        }
    }
}
