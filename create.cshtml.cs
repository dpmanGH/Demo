using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Demo.Pages.People.IndexModel;

namespace Demo.Pages.People
{
    public class createModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.age = Request.Form["age"];
            clientInfo.email = Request.Form["email"];

            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 ||
                clientInfo.age.Length == 0 || clientInfo.email.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            // save the new client into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Demo;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO info " +
                        "(id, name, age, email) VALUES " +
                        "(@id, @name, @age, @email)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", clientInfo.id);
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@age", clientInfo.age);
                        command.Parameters.AddWithValue("@email", clientInfo.email);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            clientInfo.id = ""; clientInfo.name = ""; clientInfo.age = ""; clientInfo.email = "";
            successMessage = "New Client Added Correctly";
            Response.Redirect("/People/Index");
        }
    }
}
