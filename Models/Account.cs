
namespace Foo_Form.Models
{
    public class Account
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    };
}
