using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Foo_Form.Models;
using Foo_Form.Data;

namespace Foo_Form.Pages
{
    public class DashboardModel : PageModel
    {
        public required List<Record> Records { get; set; }

        public readonly FooFormDbContext Context;

        public DashboardModel(FooFormDbContext dbContext)
        {
            this.Context = dbContext;
            this.Records = [];
        }

        public IActionResult OnGet()
        {
            if (Request.Cookies["user_id"] != null)
            {
                this.Records = Context.Records.Where(rec => rec.OwnerId.ToString() == Request.Cookies["user_id"]).ToList();
                return Page();
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid)
            {

                return Page();
            }

            var user_id_string = Request.Cookies["user_id"];

            if (user_id_string != null)
            {
                var user_id_int = int.Parse(user_id_string);
                Console.
                Context.Records.Add(
                    new Record
                    {
                        Name = RecordDTO.Name,
                        DateJoined = DateOnly.FromDateTime(RecordDTO.DateJoined),
                        Email = RecordDTO.Email,
                        OwnerId = user_id_int
                    }
                );
            }
            return Page();
        }

        public RecordDTO RecordDTO { get; set; }
    }
}
