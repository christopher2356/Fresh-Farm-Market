using Fresh_Farm_Market.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using System.Web;

namespace Fresh_Farm_Market.Pages
{
	public class LoginModel : PageModel
    {
		[BindProperty]
		public Login LModel { get; set; }
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<RegisterModel> logger;
        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger)
		{
			this.signInManager = signInManager;
            this.logger = logger;
        }
		public void OnGet()
        {
        }
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
                var identityResult = await signInManager
					.PasswordSignInAsync(HttpUtility.HtmlEncode(LModel.Email), HttpUtility.HtmlEncode(LModel.Password), isPersistent: false, lockoutOnFailure: true);

				string guid = Guid.NewGuid().ToString();
				HttpContext.Session.SetString("MyCookieAuth", guid);
				Response.Cookies.Append("MyCookieAuth", guid);

				string guid2 = Guid.NewGuid().ToString();
				HttpContext.Session.SetString("MySessionCookie", guid2);
				Response.Cookies.Append("MySessionCookie", guid2);

				if (identityResult.Succeeded)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, "c@c.com"),
						new Claim(ClaimTypes.Email, "c@c.com"),
					};

					var i = new ClaimsIdentity(claims, "MyCookieAuth");
					ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(i);
					await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    HttpContext.Session.SetString("Email", LModel.Email);
                    logger.LogInformation("Successful user login");

                    return RedirectToPage("Index");
				}

				ModelState.AddModelError("", "Username or Password incorrect");
                logger.LogInformation("Unsuccessful user login");

                if (identityResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "User account locked out");
                    logger.LogInformation("User account locked out");
                }
            }
			return Page();
		}
	}
}
