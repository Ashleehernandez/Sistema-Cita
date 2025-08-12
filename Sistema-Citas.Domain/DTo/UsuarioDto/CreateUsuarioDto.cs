using System.ComponentModel.DataAnnotations;

namespace Sistema_Citas.Domain.DTo.UsuarioDto
{
    public class CreateUsuarioDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string ContrasenaHash { get; set; }

        [Required]
        [MaxLength(20)]
        public string Rol { get; set; } = "Usuario"; 

        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
    }
}
