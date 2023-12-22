using Fresh_Farm_Market.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;

namespace Fresh_Farm_Market.Pages
{
    public class LogoutModel : PageModel
    {
		private readonly SignInManager<IdentityUser> signInManager;
		public LogoutModel(SignInManager<IdentityUser> signInManager)
		{
			this.signInManager = signInManager;
		}
		public void OnGet()
        {
        }
		public async Task<IActionResult> OnPostLogoutAsync()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("MyCookieAuth");
			HttpContext.Session.Remove("MySessionCookie");

			string guid = Guid.NewGuid().ToString();
			HttpContext.Session.SetString("MyCookieAuth", guid);
			Response.Cookies.Append("MyCookieAuth", guid);

			string guid2 = Guid.NewGuid().ToString();
			HttpContext.Session.SetString("MySessionCookie", guid2);
			Response.Cookies.Append("MySessionCookie", guid2);

			await signInManager.SignOutAsync();
			
			return RedirectToPage("Login");
		}
		public async Task<IActionResult> OnPostDontLogoutAsync()
		{
			return RedirectToPage("Index");
		}
	}
}
