using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.SqlServerCe;


namespace Barroc_IT_Programma
{
    class AccountAcces
    {
        DatabaseHandler dhb = new DatabaseHandler();
        int[] accesCustSales = new int[15];
        int[] accesCustDevelopment = new int[10];
        int[] accesCustFinance = new int[10];
        string[] columns;

        public AccountAcces()
        {
            accesCustSales[0] = 1;
            accesCustSales[1] = 2;
            accesCustSales[2] = 3;
            accesCustSales[3] = 4;
            accesCustSales[4] = 5;
            accesCustSales[5] = 6;
            accesCustSales[6] = 7;
            accesCustSales[7] = 8;
            accesCustSales[8] = 9;
            accesCustSales[9] = 11;
            accesCustSales[10] = 12;
            accesCustSales[11] = 13;
            accesCustSales[12] = 14;
            accesCustSales[13] = 22;
            accesCustSales[14] = 23;
        }

        private string[] getColumnsName()
        {
            List<string> listacolumnas = new List<string>();
            using (SqlConnection connection = dhb.Getcon())
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = 'Tbl_Customer' and t.type = 'U'";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listacolumnas.Add(reader.GetString(0));
                    }
                }
            }
            return listacolumnas.ToArray();
        }

        public string[] GetColumnsCustomer()
        {
            columns = getColumnsName();
            return columns;
        }

        public int[] GetSalesCustomerAcces()
        {
            return accesCustSales;
        }
    }
}
