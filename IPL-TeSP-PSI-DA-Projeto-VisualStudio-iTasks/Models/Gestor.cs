using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    internal class Gestor : Utilizador
    {
        public string departamento { get; set; }
        public bool GereUtilizadores { get; set; }

    }
}
