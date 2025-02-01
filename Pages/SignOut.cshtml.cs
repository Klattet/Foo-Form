using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Foo_Form.Pages
{
    public class SignOutModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (Request.Cookies["user_id"] != null)
            {
                Response.Cookies.Delete("user_id");
            }
            return RedirectToPage("/Login");
        }


    }
}
