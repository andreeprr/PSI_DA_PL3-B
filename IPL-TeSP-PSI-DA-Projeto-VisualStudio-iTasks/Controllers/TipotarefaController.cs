using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Controllers
{
    internal class TipotarefaController
    {

        //adiciona um tipo de tarefa à base de dados
        public static bool AdicionarTipoTarefa(TipoTarefa tipoTarefa)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    db.TipoTarefas.Add(tipoTarefa);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
        //vai buscar a lista de tipos de tarefas na base de dados
        public static List<TipoTarefa> ObterTiposTarefas()
        {
            using (var db = new iTasksContext())
            {
                return db.TipoTarefas.ToList();
            }
        }
    }
}
