using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UspgPOS.Models
{
    public class Cliente
    {
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        [Column("nombre")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        [Column("nit")]
        public string Nit {  get; set; }
        [StringLength(255)]
        [Column("correo")]
        public string Correo { get; set; }

        public ICollection<Venta> Ventas { get; set; } // Relacion de uno a muchos

    }
}
