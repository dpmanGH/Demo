using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Demo.Pages.People
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public class ClientInfo
        {
            public string id;
            public string name;
            public string age;
            public string email;
        }
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress; Initial Catalog = Demo; Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM info order by age";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo info = new ClientInfo();
                                info.id = "" + reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.age = "" + reader.GetInt32(2);
                                info.email = reader.GetString(3);

                                listClients.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
