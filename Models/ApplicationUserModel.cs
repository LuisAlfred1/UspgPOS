using Microsoft.AspNetCore.Identity;

namespace UspgPOS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Rol { get; set; }
    }
}
