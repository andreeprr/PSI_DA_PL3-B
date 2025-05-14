using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    internal class Programador : Utilizador
    {
        public int idGestor { get; set; }
        public int NivelExperiencia { get; set; }
    }
}
