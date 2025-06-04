using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks
{
    public class TipoTarefa
    {
        public int id { get; set; }
        public string nome { get; set; }
        public override string ToString()
        {
            string texto = this.nome + " (" + this.id + ")";
            return texto;
        }
    }
}
