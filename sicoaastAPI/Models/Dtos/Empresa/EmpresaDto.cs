using System.ComponentModel.DataAnnotations;

namespace sicoaastAPI.Models.Dtos.Empresa
{
    public class EmpresaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage="El numero de caracteres del nombre, debe ser menor a 60")]
        public string Name { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
