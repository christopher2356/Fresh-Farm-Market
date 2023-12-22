using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Fresh_Farm_Market.ViewModels
{
	public class Register
	{
		[Required]
		[DataType(DataType.Text)]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.CreditCard)]
		public string CreditCardNo { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Gender { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		[Phone(ErrorMessage = "Invalid phone number")]
		public string MobileNo { get; set; }

        [Required]
        [DataType(DataType.Text)]
		public string DeliveryAddress { get; set; }

        [Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required, MinLength(12, ErrorMessage = "Password must have min 12 chars"), MaxLength(50)]
		[DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\W]).{3,}$",
			ErrorMessage = "Password must have a combination of lower-case, upper-case, numbers and special characters")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
		public string ConfirmPassword { get; set; }

        [DataType(DataType.Upload)]
		[FileExtensions(Extensions = "jpg")]
		public string? Photo {  get; set; }

        [Required]
		[DataType(DataType.MultilineText)]
		public string AboutMe { get; set; }
    }

}
