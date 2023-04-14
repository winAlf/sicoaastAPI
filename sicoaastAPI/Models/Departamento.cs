using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sicoaastAPI.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        [ForeignKey("ccostoId")]
        public int ccostoId { get; set; }
        public Ccosto Ccosto { get; set; }
    }
}
