using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tp01Crud
{
    public class PeopleDB
    {
        private string connectionString =
            "Data Source=OMEN\\SQLEXPRESS;Initial Catalog=CrudWindowsForm;Integrated Security=True";

        public bool Ok()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<People> Get()
        {
            List<People> list = new List<People>();
            string query = "select id,name,age from people";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        People p = new People();
                        p.Id = reader.GetInt32(0);
                        p.Name = reader.GetString(1);
                        p.Age = reader.GetInt32(2);
                        list.Add(p);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception)
                {
                    throw new Exception("Hay un error");
                }
            }
            return list;
        }
        public class People
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
