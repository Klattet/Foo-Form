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

        public IActionResult OnPostAdd() {

            var user_id_string = Request.Cookies["user_id"];

            if (user_id_string != null)
            {
                var user_id_int = int.Parse(user_id_string);

                var name = Request.Form["name"];
                var date = Request.Form["date"];
                var email = Request.Form["email"];

                var bad_data_flag = false;

                if (String.IsNullOrEmpty(name))
                {
                    ModelState.AddModelError("RecordDTO.Name", "Name is a required field!");
                    bad_data_flag = true;
                }
                if (String.IsNullOrEmpty(date))
                {
                    ModelState.AddModelError("RecordDTO.DateJoined", "Date is a required field!");
                    bad_data_flag = true;
                }
                if (String.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("RecordDTO.Email", "Email is a required field!");
                    bad_data_flag = true;
                }

                if (bad_data_flag)
                {
                    this.Records = Context.Records.Where(rec => rec.OwnerId.ToString() == Request.Cookies["user_id"]).ToList();
                    return Page();
                }
                
                Context.Records.Add(
                    new Record
                    {
                        Name = name,
                        DateJoined = DateOnly.Parse(date),
                        Email = email,
                        OwnerId = user_id_int
                    }
                );
                Context.SaveChanges();
                ModelState.Clear();
            }
            return OnGet();
        }

        public IActionResult OnPostRemove(int id)
        {
            var record = Context.Records.FirstOrDefault(rec => rec.Id == id);
            if (record != null)
            {
                Context.Remove(record);
                Context.SaveChanges();
            }
            return OnGet();
        }

        public RecordDTO RecordDTO { get; set; }
        public DateTime DateTime { get; set; }
    }
}
