

namespace Sistema_Citas.Domain.DTo.EstacionDto
{
    public class CreateEstacionDto
    {
        
        public int EstacionId { get; set; }

    
        public int Numero { get; set; }

        public string Nombre { get; set; }

        public bool Disponible { get; set; } = true;

        public string Turno { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        }
    }
