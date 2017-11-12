using System;

namespace OnlineShop.ViewModels.Identity
{
	public class RegistrationViewModel
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string SecondName { get; set; }

		public string Confirm { get; set; }

		public bool Gender { get; set; }

		public DateTime BirthDt { get; set; }
	}

	public class LoginViewModel
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}