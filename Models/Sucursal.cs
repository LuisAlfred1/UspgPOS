using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UspgPOS.Models
{
    public class Sucursal
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("nombre")]
        [StringLength(255)]
        [Required]
        public string Nombre { get; set; }

        [Column("area")]
        [StringLength(255)]
        public string Area { get; set; }

        [Column("ciudad")]
        [StringLength(255)]
        public string Ciudad { get; set; }
    }
}
