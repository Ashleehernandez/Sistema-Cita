
using Sistema_Citas.Domain.Entity;
using Sistema_Citas.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Sistema_Citas.Domain.DTo.CitasDto
{
    public class GetCitasDto
    {
       
        public int CitaId { get; set; }


        public DateTime FechaInicio { get; set; }

 
        public DateTime FechaFin { get; set; }

        public Estado Estado { get; set; } // Pendiente, Confirmado, Rechazado


     
        public int EstacionId { get; set; }

        [ForeignKey("EstacionId")]
        public Estacion Estacion { get; set; }


        public int CantidadTurnos { get; set; }

 
        public int DuracionMinutos { get; set; }
    }
}

