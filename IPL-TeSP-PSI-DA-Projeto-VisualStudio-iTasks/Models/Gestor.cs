using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class Gestor : Utilizador
    {
        public Departamento departamento { get; set; }
        public bool GereUtilizadores { get; set; }
        public override string ToString()
        {
            string texto = this.nome + "(" + this.id + ")";
            return texto;
        }

    }
    public enum Departamento
    {
        IT,
        Marketing,
        Administração
    }
}
