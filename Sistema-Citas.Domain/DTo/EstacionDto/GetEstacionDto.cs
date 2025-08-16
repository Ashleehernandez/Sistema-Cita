

namespace Sistema_Citas.Domain.DTo.EstacionDto
{
    public class GetEstacionDto
    {
        public int EstacionId { get; set; }

        public int Numero { get; set; }

        public string Nombre { get; set; }

        public bool Disponible { get; set; } = true;


        public string Turno { get; set; }

    }
}
