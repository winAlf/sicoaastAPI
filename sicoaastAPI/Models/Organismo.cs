using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sicoaastAPI.Models
{
    public class Organismo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        [ForeignKey("empresaId")]
        public int empresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
