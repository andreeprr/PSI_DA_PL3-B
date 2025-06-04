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
        public override string ToString()
        {
            string texto = this.nome + " (" + this.id + ")";
            return texto;
        }

    }
    public enum NivelExperiencia
    {
        Junior,
        Senior
    }
}
