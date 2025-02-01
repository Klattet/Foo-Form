
namespace Foo_Form.Models
{
    public class Record
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateOnly DateJoined { get; set; }
        public required string Email { get; set; }

        public required int OwnerId { get; set; }
    };
}
