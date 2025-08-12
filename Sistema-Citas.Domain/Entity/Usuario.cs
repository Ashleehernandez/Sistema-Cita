

using System.ComponentModel.DataAnnotations;

namespace Sistema_Citas.Domain.Entity
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ContrasenaHash { get; set; }

        [Required]
        [MaxLength(20)]
        public string Rol { get; set; } = "Usuario"; // Valor por defecto

        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
    }
}
