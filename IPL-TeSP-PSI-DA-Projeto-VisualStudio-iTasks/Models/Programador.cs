using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class Programador : Utilizador
    {
        public Gestor gestor { get; set; }
        public NivelExperiencia NivelExperiencia { get; set; }

    }
    public enum NivelExperiencia
    {
        Junior,
        Senior
    }
}
