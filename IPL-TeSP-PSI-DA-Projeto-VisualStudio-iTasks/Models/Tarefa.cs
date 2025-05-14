using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks
{
    internal class Tarefa
    {
        public int id { get; set; }
        public int idGestor { get; set; }
        public int idProgramador { get; set; }
        public int ordemExecucao { get; set; }
        public string descricao { get; set; }
        public DateTime dataPrevistaInicio { get; set; }
        public DateTime dataPrevistaFim { get; set; }
        public TipoTarefa tipoTarefa { get; set; }
        public int storyPoints { get; set; }
        public DateTime dataRealInicio { get; set; }
        public DateTime dataRealFim { get; set; }  
        public DateTime dataCriacao { get; set; }
        public string estadoAtual { get; set; }

    }
}
