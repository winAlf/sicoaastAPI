﻿using System.ComponentModel.DataAnnotations;

namespace sicoaastAPI.Models.Dtos.Ccosto
{
    public class CrearCcostoDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero de caracteres del nombre, debe ser menor a 60")]
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int organismoId { get; set; }
    }
}
