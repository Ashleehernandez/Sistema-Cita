

using Sistema_Citas.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Citas.Domain.DTo.CitasDto
{
    public class UpdateCitasDto
    {
        
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public Estado Estado { get; set; } // Pendiente, Confirmado, Rechazado

      
        public int UsuarioId { get; set; }

        public int CantidadTurnos { get; set; }

        public int DuracionMinutos { get; set; }
    }
}
