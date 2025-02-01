using Foo_Form.Data;
using Foo_Form.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Foo_Form.Pages
{
    public class LoginModel : PageModel
    {

        private readonly FooFormDbContext Context;

        public LoginModel(FooFormDbContext dbContext)
        {
            this.Context = dbContext;
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
            if (!ModelState.IsValid) {
                return Page();
            }

            var account = Context.Accounts.FirstOrDefault(acc => acc.UserName == LoginDTO.UserName);
            if (account == null)
            {
                ModelState.AddModelError("LoginDTO.Username", "Could not find any accounts with that username!");
                return Page();
            }
            else if (account.Password != LoginDTO.Password)
            {
                ModelState.AddModelError("LoginDTO.Password", "Incorrect password!");
                return Page();
            }
            else
            {
                if (Request.Cookies["user_id"] == null)
                {
                    Response.Cookies.Append("user_id", account.Id.ToString());
                }
                return RedirectToPage("/Dashboard");
            }
        }

        [BindProperty]
        public LoginDTO LoginDTO { get; set; }
    }
}