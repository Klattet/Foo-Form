
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Foo_Form.Models
{
    public class RecordDTO
    {
        public required string Name { get; set; }

        public required DateTime DateJoined { get; set; }
        public required string Email { get; set; }
    };
}
