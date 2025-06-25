using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                MessageBox.Show($"Erro ao adicionar tipo de tarefa: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool EliminarTipoTarefa(TipoTarefa tipoTarefa)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    // Anexa o tipo de tarefa ao contexto, caso não esteja
                    db.TipoTarefas.Attach(tipoTarefa);
                    // Procura as tarefas associados a este tipo de tarefa
                    var tarefasAssociadas = db.Tarefas.Where(t => t.tipoTarefa.id == tipoTarefa.id).ToList();
                    // Remove as tarefas associadas
                    db.Tarefas.RemoveRange(tarefasAssociadas);
                    //Remove o tipo de tarefa
                    db.TipoTarefas.Remove(tipoTarefa);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao eliminar tipo de tarefa: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool AtualizarTipoTarefa(TipoTarefa tipoTarefa)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    var tipoTarefaDb = db.TipoTarefas.FirstOrDefault(t => t.id == tipoTarefa.id);
                    if (tipoTarefaDb == null)
                        return false;

                    tipoTarefaDb.nome = tipoTarefa.nome; // Certifica-se de que o nome é atualizado

                    db.SaveChanges(); // Certifica-se de que as alterações são salvas na base de dados
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar tipo de tarefa: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
