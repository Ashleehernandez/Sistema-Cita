

using System.ComponentModel.DataAnnotations;

namespace Sistema_Citas.Domain.DTo.EstacionDto
{
    public class CreateEstacionDto
    {
        [Required]
        public int Numero { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        public bool Disponible { get; set; } = true;

        [MaxLength(30)]
        public string? Turno { get; set; } = "Mañana";


    }
}
