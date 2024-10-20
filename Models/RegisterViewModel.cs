using System.ComponentModel.DataAnnotations;

namespace UspgPOS.Models
{
	public enum UserRole
	{
		Admin,
		User
	}

	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Nombre de Usuario")]
		public string Usuario { get; set; }

		[Required]
		[Display(Name = "Nombre Completo")]
		public string Nombre { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Contraseña")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirmar Contraseña")]
		[Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[Display(Name = "Rol")]
		public UserRole Rol { get; set; }
	}
}
