using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UspgPOS.Models
{
    public class Clasificacion
    {
        [Column("id")]
        public long id {  get; set; }
        [Column("nombre")]
        [StringLength(255)]
        public string Nombre { get; set; }
    }
}
