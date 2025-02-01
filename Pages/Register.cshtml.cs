using System.Text.RegularExpressions;
using Foo_Form.Data;
using Foo_Form.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Foo_Form.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly FooFormDbContext _db;
        private readonly Regex EmailRegex;

        public RegisterModel(FooFormDbContext dbContext)
        {
            this._db = dbContext;
            this.EmailRegex = new Regex("^.+@.+\\..+$");
        }

        public IActionResult OnGet()
        {
            if (Request.Cookies["user_id"] != null)
            {
                return RedirectToPage("/Dashboard");
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_db.Accounts.FirstOrDefault(acc => acc.UserName == RegisterDTO.UserName) != null)
            {
                ModelState.AddModelError("RegisterDTO.Username", "This username is not available!");
                return Page();
            }
            else if (_db.Accounts.FirstOrDefault(acc => acc.Email == RegisterDTO.Email) != null)
            {
                ModelState.AddModelError("RegisterDTO.Email", "This e-mail is already tied to an account!");
                return Page();
            }
            else if (!EmailRegex.IsMatch(RegisterDTO.Email))
            {
                ModelState.AddModelError("RegisterDTO.Email", "Invalid e-mail format!");
                return Page();
            } else {
                _db.Accounts.Add(
                    new Account
                    {
                        UserName = RegisterDTO.UserName,
                        Email = RegisterDTO.Email,
                        Password = RegisterDTO.Password
                    }
                );
                _db.SaveChanges();

                var saved_account = _db.Accounts.FirstOrDefault(acc => acc.UserName == RegisterDTO.UserName);

                if (saved_account != null) {
                    Response.Cookies.Append("user_id", saved_account.Id.ToString());
                }
                return RedirectToPage("/Dashboard");
            }
        }

        [BindProperty]
        public RegisterDTO RegisterDTO { get; set; }
    }

}
