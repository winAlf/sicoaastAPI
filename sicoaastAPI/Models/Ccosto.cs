using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sicoaastAPI.Models
{
    public class Ccosto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        [ForeignKey("organismoId")]
        public int organismoId { get; set; }
        public Organismo Organismo { get; set; }
    }
}
