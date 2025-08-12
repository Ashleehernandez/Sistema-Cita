
using Sistema_Citas.Domain.Entity;
using Sistema_Citas.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Sistema_Citas.Domain.DTo.CitasDto
{
    public class CreateCitaDto
    {
      
        public DateTime FechaInicio { get; set; }

       
        public DateTime FechaFin { get; set; }

       
        public int UsuarioId { get; set; }

        public int EstacionId { get; set; }
       
        public int CantidadTurnos { get; set; }

       
        public int DuracionMinutos { get; set; }
    }
}
