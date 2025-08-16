

using System.ComponentModel.DataAnnotations;

namespace Sistema_Citas.Domain.Entity
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }


        [MaxLength(100)]
        public string Nombre { get; set; }

   
        [MaxLength(100)]
        public string Apellido { get; set; }

        
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }


        public string ContrasenaHash { get; set; }


        [MaxLength(20)]
        public string Rol { get; set; } = "Usuario"; // Valor por defecto

        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
    }
}
