namespace Sistema_Citas.Domain.DTo.UsuarioDto
{
    public class UpdateUsuarioDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string ContrasenaHash { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
