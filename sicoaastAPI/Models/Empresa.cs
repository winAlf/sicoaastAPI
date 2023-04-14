using System.ComponentModel.DataAnnotations;

namespace sicoaastAPI.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
