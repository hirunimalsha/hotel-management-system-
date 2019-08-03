using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class MenuItem
    {
        public string ItemNumber;
        public string ItemName;
        public float ItemPrice;

        public static MenuItem MenuItemByItemNumber(string item_number)
        {
            SqlConnection connection = db.GetConnection();
            SqlDataReader reader = null;
            MenuItem menuItem = new MenuItem();
            try
            {
                string query = $"SELECT * FROM menuItems WHERE item_no = '{item_number}'";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    menuItem.ItemNumber = reader["item_no"].ToString().Trim();
                    menuItem.ItemName = reader["item_name"].ToString().Trim();
                    menuItem.ItemPrice = float.Parse(reader["item_price"].ToString().Trim());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SqlException occured. Timeout.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("InvalidOperationException occured. The connection to database was either not open, or closed during operation.");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (connection != null)
                {
                    connection.Close();
                }
            }

            return menuItem;
        }
    }
}
