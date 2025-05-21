using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    internal class Gestor : Utilizador
    {
        public Departamento departamento { get; set; }
        public bool GereUtilizadores { get; set; }

    }
    public enum Departamento
    {
        IT,
        Marketing,
        Administração
    }
}
