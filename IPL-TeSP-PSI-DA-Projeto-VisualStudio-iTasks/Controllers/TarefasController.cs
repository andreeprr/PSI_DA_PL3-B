using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks.Models;

namespace iTasks.Controllers
{
    internal class TarefasController
    {
        public static List<Tarefa> ObterTarefasPorUtilizador(Utilizador utilizador)
        {
            // Aqui vamos buscar as tarefas associadas ao utilizador pelo IDs
            using (var db = new iTasksContext())
            {
                return db.Tarefas.Where(tarefa => tarefa.programador.id == utilizador.id).ToList();
            }
        }
    }
}
