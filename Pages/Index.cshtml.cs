using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace Fresh_Farm_Market.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
        private readonly IHttpContextAccessor contxt;
        public IndexModel(IHttpContextAccessor httpContextAccessor)
        {
            contxt = httpContextAccessor;
        }
        public void OnGet()
		{
            var Email = HttpContext.Session.GetString("Email");
        }
	}
}