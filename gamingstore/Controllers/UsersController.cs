using gamingstore.Models;
using gamingstore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace gamingstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // ✅ Login API
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest(new { success = false, message = "Email and password are required!" });
            }

            bool isValidUser = userRepository.Login(login);

            if (isValidUser)
            {
                return Ok(new { success = true, message = "Login successful!", userId = login.Id });
            }

            return Unauthorized(new { success = false, message = "Invalid email or password!" });


        }



        // ✅ Register API
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { success = false, message = "All fields are required!" });
            }

            int result = userRepository.Register(user);

            if (result == -1)
            {
                return BadRequest(new { success = false, message = "Email already exists!" });
            }
            else if (result > 0)
            {
                return Ok(new { success = true, message = "Registration successful!" });
            }

            return BadRequest(new { success = false, message = "Registration failed!" });
        }

        // ✅ Get All Games API
        [HttpGet("products")]
        public ActionResult<List<ProductModel>> Getproducts()
        {
            var products = userRepository.GetAllproduct();

            if (products == null || products.Count == 0)
            {
                return NotFound(new { success = false, message = "No Product found!" });
            }

            return Ok(products);
        }

        // ✅ Delete Game by ID API
        [HttpDelete("deleteproducts/{id}")]
        public IActionResult Delete(int id)
        {
            bool result = userRepository.Deleteproduct(id);

            if (result)
            {
                return Ok(new { success = true, message = "Product deleted successfully!" });
            }

            return NotFound(new { success = false, message = "Product not found!" });
        }

        // ✅ Add Game to Cart API
        [HttpPost("addToCart")]
        public IActionResult AddToCart([FromQuery] int userId, [FromQuery] int productId)
        {
            if (userId <= 0 || productId <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid user ID or game ID!" });
            }

            try
            {
                userRepository.AddToCart(userId, productId);
                return Ok(new { success = true, message = "Product added to cart successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Failed to add Product to cart: {ex.Message}" });
            }
        }

        [HttpGet("getCartItems/{userId}")]
        public IActionResult GetCartItems(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid user ID!" });
            }

            DataTable cartItems = userRepository.GetCartItems(userId);

            if (cartItems.Rows.Count == 0)
            {
                return NotFound(new { success = false, message = "Cart is empty!" });
            }

            // Convert DataTable to a serializable list
            var cartList = new List<Dictionary<string, object>>();
            foreach (DataRow row in cartItems.Rows)
            {
                var item = new Dictionary<string, object>();
                foreach (DataColumn column in cartItems.Columns)
                {
                    item[column.ColumnName] = row[column];
                }
                cartList.Add(item);
            }

            return Ok(cartList); // Return as JSON-friendly format
        }
    }
}
