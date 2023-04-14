using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sicoaastAPI.Models.Dtos.MovEmp
{
    public class CrearMovEmpDto
    {
        public int numEmp { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El numero de caracteres del nombre, debe ser menor a 100")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [MaxLength(100, ErrorMessage = "El numero de caracteres del apellido paterno, debe ser menor a 100")]
        public string Apaterno { get; set; }
        [MaxLength(100, ErrorMessage = "El numero de caracteres del apellido materno, debe ser menor a 100")]
        public string Amaterno { get; set; }
        public int Folio { get; set; }
        public int Nip { get; set; }
        public string RutaImagen { get; set; }
        public enum TipoGenero { Masculino, Femenino }
        public TipoGenero Genero { get; set; }
        public DateTime FechaVigencia { get; set; }
        public int empresaId { get; set; }
        public int organismoId { get; set; }
        public int ccostoId { get; set; }
        public int departamentoId { get; set; }
    }
}
