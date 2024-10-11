using System.ComponentModel.DataAnnotations;

namespace UspgPOS.Models
{
	public class LoginViewModel
	{
		[Required]
		public string Usuario { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool Recordar { get; set; }

		[Required]
		public long SucursalId { get; set; }
	}
}
