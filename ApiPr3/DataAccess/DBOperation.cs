using ApiPr3.Interface;
using ApiPr3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ApiPr3.DataAccess
{
    public class DBOperation : IDBOperation
    {
        private readonly IConfiguration _configuration;

        public DBOperation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Book> Select(string sqlStatement)
        {
            var books = new List<Book>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
            {
                connection.Open();

                string query = sqlStatement;

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            books.Add(new Book()
                            {
                                ID = Convert.ToInt32(reader["ID"]),

                                PublisherID = Convert.ToInt32(reader["PublisherID"]),

                                Title = reader["Title"].ToString(),

                                Pages = Convert.ToInt32(reader["Pages"]),

                                Weight = Convert.ToInt32(reader["Weight"]),

                                Subject = reader["Subject"].ToString()
                            });
                        }catch(InvalidCastException) {

                            books.Add(new Book()
                            {
                                ID = Convert.ToInt32(reader["ID"]),

                                PublisherID = Convert.ToInt32(reader["PublisherID"]),

                                Title = reader["Title"].ToString(),

                                Pages = Convert.ToInt32(reader["Pages"]),

                                Weight = null,

                                Subject = reader["Subject"].ToString()
                            });
                        }

                    }
                }
            }

            return books;
        }

        public int Insert(string sqlStatement)
        {
            string query = sqlStatement;

            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception) { return 0; }
        }

        public int Delete(string sqlStatement)
        {
            string query = sqlStatement;

            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    return command.ExecuteNonQuery();
                }
            }catch(Exception) { return 0; }

            
        }

        public int Update(string sqlStatement)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    return command.ExecuteNonQuery();
                }
            }catch (Exception) { return 0; }
        }
    }
}
