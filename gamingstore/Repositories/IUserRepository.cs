using System.Data;
using gamingstore.Models;

namespace gamingstore.Repositories
{
    public interface IUserRepository
    {
        int Register(UserModel user);
        bool Login(LoginModel user);  // ✅ Return type changed to bool
        List<ProductModel> GetAllproduct();
        bool Deleteproduct(int id);
        void AddToCart(int userId, int gameId);
        DataTable GetCartItems(int userId);
        int GetUserId(LoginModel login);
        bool DeleteProductFromCart(int cartId);
    }
}
