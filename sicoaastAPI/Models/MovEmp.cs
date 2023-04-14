using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sicoaastAPI.Models
{
    public class MovEmp
    {
        public int Id { get; set; }
        [Required]
        public int activo { get; set; }
        [Required]
        public int numEmp { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(100)]
        public string Apaterno { get;set; }
        [MaxLength(100)]
        public string Amaterno { get; set; }
        public string NombreCompleto { get; set; }
        [Required]
        public int Folio { get; set; }
        public int? Nip { get; set;}
        [MaxLength(60)]
        public string? RutaImagen { get; set;}
        public enum TipoGenero { Masculino, Femenino }
        public TipoGenero? Genero { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public DateTime? FechaReactivacion { get; set; }
        public DateTime? FechaVigencia { get; set; }

        [ForeignKey("empresaId")]
        public int empresaId { get; set; }
        public Empresa Empresa { get; set; }

        [ForeignKey("organismoId")]
        public int organismoId { get; set; }
        public Organismo Organismo { get; set; }

        [ForeignKey("ccostoId")]
        public int ccostoId { get; set; }
        public Ccosto Ccosto { get; set; }

        [ForeignKey("departamentoId")]
        public int departamentoId { get; set; }
        public Departamento Departamento { get; set; }

    }
}
