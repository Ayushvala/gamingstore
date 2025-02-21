using System.Data;
using gamingstore.Models;
using Microsoft.Data.SqlClient;

namespace gamingstore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;

        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //  Login Method
        public bool Login(LoginModel login)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_LoginUser1", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", login.Email);
                cmd.Parameters.AddWithValue("@Password", login.Password);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Check if the 'Id' column exists and is not DBNull
                        if (reader["Id"] != DBNull.Value)
                        {
                            login.Id = Convert.ToInt32(reader["Id"]);
                        }
                        else
                        {
                            return false; // Return false if 'Id' column is missing
                        }

                        // Check if the 'Email' column exists and is not DBNull
                        if (reader["Email"] != DBNull.Value)
                        {
                            login.Email = reader["Email"].ToString();
                        }
                        else
                        {
                            return false; // Return false if 'Email' column is missing
                        }

                        return true;
                    }
                    return false; // No user found
                }
            }
        }


        //  Registration Method
        public int Register(UserModel user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_RegisterUser113", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                SqlParameter returnParam = new SqlParameter("@ReturnVal", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(returnParam);

                conn.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(returnParam.Value);
            }
        }

        //  Get All Games
        public List<ProductModel> GetAllproducts()
        {
            List<ProductModel> product = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Product", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.Add(new ProductModel
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            Photo = reader["Photo"] != DBNull.Value ? reader["Photo"].ToString() : string.Empty
                        });
                    }
                }
            }
            return product;
        }

        //  Delete a Game by ID
        public bool Deleteproduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;  // Returns true if one or more rows are deleted
            }
        }

        //  Get Cart Items by User ID
        public DataTable GetCartItems(int userId)
        {
            DataTable cartTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("sp_GetCartItemsByUserId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(cartTable);
                }
            }
            return cartTable;
        }

        //  Add Game to Cart
        public void AddToCart(int userId, int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Cart (UserId, ProductId) VALUES (@UserId, @ProductId)", connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@ProductId", productId);
                connection.Open();
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new Exception("Failed to add the product to the cart.");
                }
            }
        }

        //  Get User ID from Login Credentials
        public int GetUserId(LoginModel login)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT Id FROM Users WHERE Email = @Email AND Password = @Password", conn))
            {
                cmd.Parameters.AddWithValue("@Email", login.Email);
                cmd.Parameters.AddWithValue("@Password", login.Password);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        //  Get All Products (similar to GetAllproducts method)
        public List<ProductModel> GetAllproduct()
        {
            List<ProductModel> productList = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Product", conn)) // Ensure "Product" is your table name
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productList.Add(new ProductModel
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            Photo = reader["Photo"] != DBNull.Value ? reader["Photo"].ToString() : string.Empty
                        });
                    }
                }
            }
            return productList;
        
        }

        public bool DeleteProductFromCart(int cartId)
        {
            throw new NotImplementedException();
        }
    }
}
