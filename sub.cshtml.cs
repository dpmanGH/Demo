using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Operation
{
    public class subModel : PageModel
    {
        public bool isok = false;
        public String a = "";
        public String b = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            isok = true;
            a = Request.Form["a"];
            b = Request.Form["b"];
        }
    }
}
