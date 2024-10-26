using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UspgPOS.Models
{
    public class Marca
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("nombre")]
        [StringLength(255)]
        public string Nombre { get; set; }
    }
}
