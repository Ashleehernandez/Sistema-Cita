namespace Sistema_Citas.Domain.DTo.UsuarioDto
{
    public class UpdateUsuarioDto
    {
        public string ContrasenaHash { get; set; }

        public string Nombre { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
