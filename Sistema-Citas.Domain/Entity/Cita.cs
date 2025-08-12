

using Sistema_Citas.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Citas.Domain.Entity
{
    public class Cita
    {
        [Key]
        public int CitaId { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        [MaxLength(20)]
        public Estado Estado { get; set; } // Pendiente, Confirmado, Rechazado

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]
        public int EstacionId { get; set; }

        [ForeignKey("EstacionId")]
        public Estacion Estacion { get; set; }

        [Required]
        public int CantidadTurnos { get; set; }

        [Required]
        public int DuracionMinutos { get; set; }
    }
}

