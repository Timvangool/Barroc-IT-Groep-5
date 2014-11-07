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
        int[] accesCustSales = new int[16];
        int[] accesCustDevelopment = new int[14];
        int[] accesCustFinance = new int[20];
        string[] columns;
        string account;

        public AccountAcces(string account)
        {
            this.account = account;
            accesCustSales[0] = 1;
            accesCustSales[1] = 2;
            accesCustSales[2] = 3;
            accesCustSales[3] = 4;
            accesCustSales[4] = 5;
            accesCustSales[5] = 6;
            accesCustSales[6] = 7;
            accesCustSales[7] = 8;
            accesCustSales[8] = 9;
            accesCustSales[9] = 10;
            accesCustSales[10] = 11;
            accesCustSales[11] = 12;
            accesCustSales[12] = 13;
            accesCustSales[13] = 14;
            accesCustSales[14] = 21;
            accesCustSales[15] = 22;

            accesCustDevelopment[0] = 1;
            accesCustDevelopment[1] = 2;
            accesCustDevelopment[2] = 3;
            accesCustDevelopment[3] = 4;
            accesCustDevelopment[4] = 5;
            accesCustDevelopment[5] = 6;
            accesCustDevelopment[6] = 7;
            accesCustDevelopment[7] = 8;
            accesCustDevelopment[8] = 9;
            accesCustDevelopment[9] = 10;
            accesCustDevelopment[10] = 11;
            accesCustDevelopment[11] = 12;
            accesCustDevelopment[12] = 13;
            accesCustDevelopment[13] = 14;

            accesCustFinance[0] = 1;
            accesCustFinance[1] = 2;
            accesCustFinance[2] = 3;
            accesCustFinance[3] = 4;
            accesCustFinance[4] = 5;
            accesCustFinance[5] = 6;
            accesCustFinance[6] = 7;
            accesCustFinance[7] = 8;
            accesCustFinance[8] = 9;
            accesCustFinance[9] = 10;
            accesCustFinance[10] = 11;
            accesCustFinance[11] = 12;
            accesCustFinance[12] = 13;
            accesCustFinance[13] = 14;
            accesCustFinance[14] = 15;
            accesCustFinance[15] = 16;
            accesCustFinance[16] = 18;
            accesCustFinance[17] = 19;
            accesCustFinance[18] = 15;
            accesCustFinance[19] = 23;
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
                connection.Close();
            }
            return listacolumnas.ToArray();
        }

        public string[] GetColumnsCustomer()
        {
            columns = getColumnsName();
            return columns;
        }

        public int[] GetCustomerAcces()
        {
            int[] Acces = new int[25];
            Acces[0] = 0;
            if (account == "Sales")
                Acces = accesCustSales;
            else if (account == "Development")
                Acces = accesCustDevelopment;
            else if (account == "Finance")
                Acces = accesCustFinance;
            else if (account == "Admin")
                Acces = accesCustSales;
                

            return Acces;
        }
    }
}
