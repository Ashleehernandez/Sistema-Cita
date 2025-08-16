using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Citas.Domain.DTo.EstacionDto
{
    public class UpdateEstacionDto
    {
      

     
        public int Numero { get; set; }

        public string Nombre { get; set; } = "Mañana";

        public bool Disponible { get; set; } = true;


        public string Turno { get; set; }

 
        public DateTime? UpdateTime { get; set; }
    }
}
