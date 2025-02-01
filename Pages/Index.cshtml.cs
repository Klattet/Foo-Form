using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Foo_Form.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (Request.Cookies["user_id"] != null)
            {
                return RedirectToPage("/Dashboard");
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }
    }
}
