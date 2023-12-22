using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fresh_Farm_Market.Pages
{
    [Authorize(Roles="HR")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
