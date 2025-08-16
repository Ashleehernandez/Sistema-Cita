

using System.ComponentModel.DataAnnotations;

namespace Sistema_Citas.Domain.Entity
{
    public class Estacion
    {
        [Key]
        public int EstacionId { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = "Mañana";

        public bool Disponible { get; set; } = true;

        [MaxLength(30)]
        public string Turno { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
    }
}

