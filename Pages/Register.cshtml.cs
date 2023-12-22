using Fresh_Farm_Market.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;

namespace Fresh_Farm_Market.Pages
{
    public class RegisterModel : PageModel
    {
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
		private readonly ILogger<RegisterModel> logger;

        [BindProperty]
		public Register RModel { get; set; }
		public RegisterModel(UserManager<IdentityUser> userManager,
		SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<RegisterModel> logger)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
			this.logger = logger;
		}
		public void OnGet()
        {
        }
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var user = new IdentityUser()
				{
					UserName = RModel.Email,
					Email = RModel.Email,
				};

                IdentityRole role = await roleManager.FindByIdAsync("Admin");
                if (role == null)
                {
                    IdentityResult result3 = await roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!result3.Succeeded)
                    {
                        ModelState.AddModelError("", "Create role admin failed");
                    }
                }
                IdentityRole role2 = await roleManager.FindByIdAsync("HR");
                if (role2 == null)
                {
                    IdentityResult result4 = await roleManager.CreateAsync(new IdentityRole("HR"));
                    if (!result4.Succeeded)
                    {
                        ModelState.AddModelError("", "Create role hr failed");
                    }
                }

                //Create user
                var result = await userManager.CreateAsync(user, HttpUtility.HtmlEncode(RModel.Password));
				List<String> rolesList = new List<String>();
                rolesList.Add("Admin");
                rolesList.Add("HR");
				String[] roles = rolesList.ToArray();
				if (result.Succeeded)
				{
					//result = await userManager.AddToRolesAsync(user, roles);

                    await signInManager.SignInAsync(user, false);
					logger.LogInformation("Successful user registered");
					return RedirectToPage("Index");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
                    logger.LogInformation("Unsuccessful user registered");
                }
			}
			return Page();
		}
	}
}
