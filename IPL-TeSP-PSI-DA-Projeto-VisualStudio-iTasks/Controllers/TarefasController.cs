using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public static int tarefasAtivas(Programador programador)
        {
            using (var db = new iTasksContext())
            {
               return db.Tarefas.Count(t => t.programador.id == programador.id && (t.estadoAtual == EstadoTarefa.ToDo || t.estadoAtual == EstadoTarefa.Doing));
            }
        }

        public static bool AtualizarEstadoTarefa(Tarefa tarefa)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    var tarefaDb = db.Tarefas.FirstOrDefault(t => t.id == tarefa.id);
                    if (tarefaDb == null)
                        return false;

                    tarefaDb.estadoAtual = tarefa.estadoAtual;
                    tarefaDb.dataRealInicio = tarefa.dataRealInicio;
                    tarefaDb.dataRealFim = tarefa.dataRealFim;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar estado da tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool AtualizarTarefa(Tarefa tarefa)
        {
            try
            {
                using(var db = new iTasksContext())
                {
                    var tarefaDb = db.Tarefas.FirstOrDefault(t => t.id == tarefa.id);
                    if (tarefaDb == null)
                        return false;

                    tarefaDb.descricao = tarefa.descricao;
                    tarefaDb.ordemExecucao = tarefa.ordemExecucao;
                    tarefaDb.programador = tarefa.programador; // Certifica-se de que o programador está anexado à tarefa
                    tarefaDb.tipoTarefa = tarefa.tipoTarefa;
                    tarefaDb.storyPoints = tarefa.storyPoints;
                    tarefaDb.dataPrevistaInicio = tarefa.dataPrevistaInicio;
                    tarefaDb.dataPrevistaFim = tarefa.dataPrevistaFim;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar a tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool AdicionarTarefa(Tarefa tarefa)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    db.Programadores.Attach(tarefa.programador); // Certifica-se de que o programador está anexado à tarefa
                    db.TipoTarefas.Attach(tarefa.tipoTarefa); // Certifica-se de que o tipo de tarefa está anexado à tarefa
                    db.Tarefas.Add(tarefa);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static List<Tarefa> ObterTarefas()
        {
            using (var db = new iTasksContext())
            {
                return db.Tarefas.ToList();
            }
        }
    }
}
