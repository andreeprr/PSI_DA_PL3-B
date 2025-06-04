using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks
{
    public class Tarefa
    {
        public int id { get; set; }
        public Gestor gestor { get; set; }
        public Programador programador { get; set; }
        public int ordemExecucao { get; set; }
        public string descricao { get; set; }
        public DateTime dataPrevistaInicio { get; set; }
        public DateTime dataPrevistaFim { get; set; }
        public TipoTarefa tipoTarefa { get; set; }
        public int storyPoints { get; set; }
        public DateTime? dataRealInicio { get; set; }
        public DateTime? dataRealFim { get; set; }
        public DateTime dataCriacao { get; set; }
        public EstadoTarefa estadoAtual { get; set; }

        public override string ToString()
        {
            string texto = this.descricao + " (" + this.ordemExecucao + ")";
            return texto;
        }
    }
    public enum EstadoTarefa
    {
        ToDo,
        Doing,
        Done
    }
}
