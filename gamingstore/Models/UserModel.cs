namespace gamingstore.Models
{
    public class UserModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

    }

    public class LoginModel
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
