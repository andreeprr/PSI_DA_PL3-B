﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                if (utilizador is Gestor)
                {
                    // Se o utilizador for um gestor, vamos buscar as tarefas que ele gere
                    return db.Tarefas.Include("Gestor").Include("Programador").Include("TipoTarefa").Where(tarefa => tarefa.gestor.id == utilizador.id).ToList();
                }
                else
                {
                    return db.Tarefas.Include("Gestor").Include("Programador").Include("TipoTarefa").Where(tarefa => tarefa.programador.id == utilizador.id).ToList();
                }
            }
        }

        public static List<Tarefa> ObterTarefasEmCurso(Utilizador utilizador)
        {
            // Aqui vamos buscar as tarefas em curso e ToDo
            using (var db = new iTasksContext())
            {
                return db.Tarefas.Include("Gestor").Include("Programador").Include("TipoTarefa").Where(tarefa => 
                tarefa.estadoAtual == EstadoTarefa.Doing &&
                tarefa.estadoAtual == EstadoTarefa.ToDo &&
                tarefa.gestor.id == utilizador.id).ToList();
            }
        }

        public static List<Tarefa> ObterTarefasConcluidas(Utilizador utilizador)
        {
            // Aqui vamos buscar as tarefas concluidas
            using (var db = new iTasksContext())
            {
                if (utilizador is Gestor)
                {
                    return db.Tarefas.Include("Gestor").Include("Programador").Include("TipoTarefa").Where(tarefa =>
                        tarefa.estadoAtual == EstadoTarefa.Done &&
                        tarefa.gestor.id == utilizador.id).ToList();
                }
                else
                {
                    return db.Tarefas.Include("Gestor").Include("Programador").Include("TipoTarefa").Where(tarefa =>
                        tarefa.estadoAtual == EstadoTarefa.Done &&
                        tarefa.programador.id == utilizador.id).ToList();
                }
            }
        }

        public static List<Tarefa> ObterTarefasConcluidasPorProgramador(Utilizador utilizador)
        {
            // Aqui vamos buscar as tarefas concluidas
            using (var db = new iTasksContext())
            {
                return db.Tarefas
                    .Include("Gestor")
                    .Include("Programador")
                    .Include("TipoTarefa")
                    .Where(tarefa => 
                    tarefa.estadoAtual == EstadoTarefa.Done &&
                    tarefa.programador.id == utilizador.id)
                    .ToList();
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

                    tarefaDb.descricao = tarefa.descricao; // Certifica-se de que a descrição está anexada à tarefa
                    tarefaDb.ordemExecucao = tarefa.ordemExecucao; // Certifica-se de que a ordem de execução está anexada à tarefa
                    tarefaDb.storyPoints = tarefa.storyPoints; // Certifica-se de que os story points estão anexados à tarefa
                    tarefaDb.dataPrevistaInicio = tarefa.dataPrevistaInicio;    // Certifica-se de que a data prevista de início está anexada à tarefa
                    tarefaDb.dataPrevistaFim = tarefa.dataPrevistaFim; // Certifica-se de que a data prevista de fim está anexada à tarefa

                    tarefaDb.programador = db.Programadores.Find(tarefa.programador.id); // Certifica-se de que o programador está anexado à tarefa
                    tarefaDb.tipoTarefa = db.TipoTarefas.Find(tarefa.tipoTarefa.id); // Certifica-se de que o tipo de tarefa está anexado à tarefa

                    db.SaveChanges(); // Certifica-se de que as alterações são salvas no banco de dados
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
                    db.Gestores.Attach(tarefa.gestor); // Certifica-se de que o gestor está anexado à tarefa
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
                return db.Tarefas.Include("gestor").Include("tipoTarefa").Include("programador").ToList(); // Devolve a lista de tarefas
            }
        }

        public static bool ExportarTarefasConcluidasParaCSV(Utilizador utilizador, string caminhoArquivo)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    // Busca tarefas concluídas do gestor
                    var tarefas = db.Tarefas
                        .Include("gestor")
                        .Include("programador")
                        .Include("tipoTarefa")
                        .Where(t => t.estadoAtual == EstadoTarefa.Done && t.gestor.id == utilizador.id)
                        .ToList();

                    using (var writer = new StreamWriter(caminhoArquivo, false, Encoding.UTF8))
                    {
                        // Cabeçalho
                        writer.WriteLine("Programador;Descricao;DataPrevisaoInicio;DataPrevisaoFim;DataPrevista;TipoTarefa;DataRealInicio;DataRealFim");

                        foreach (var t in tarefas)
                        {
                            string linha = $"{t.programador.nome};{t.descricao};" +
                                $"{t.dataPrevistaInicio.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)};" +
                                $"{t.dataPrevistaFim.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)};" +
                                $"{t.dataCriacao.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)};" +
                                $"{t.tipoTarefa};" +
                                $"{t.dataRealInicio?.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)};" +
                                $"{t.dataRealFim?.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)}";
                            writer.WriteLine(linha);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar CSV: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static double PrevisaoConclusaoTarefas(List<Tarefa> todasTarefas)
        {
            var concluidasPorStoryPts = todasTarefas
                .Where(t=> t.estadoAtual == EstadoTarefa.Done && t.dataRealInicio.HasValue && t.dataRealFim.HasValue)
                .GroupBy(t => t.storyPoints)
                .ToDictionary(
                g=> g.Key,
                g => g.Average(t => (t.dataRealFim.Value - t.dataRealInicio.Value).TotalHours));

            var tarefasToDo = todasTarefas
                .Where(t => t.estadoAtual == EstadoTarefa.ToDo).ToList();

            double somaPrevisao = 0;

            foreach (var tarefa in tarefasToDo)
            {
                if (concluidasPorStoryPts.ContainsKey(tarefa.storyPoints))
                {
                    somaPrevisao += concluidasPorStoryPts[tarefa.storyPoints];
                }
                else if (concluidasPorStoryPts.Count > 0)
                {
                    // Procura a média do StoryPoints mais próximo
                    var spMaisProximo = concluidasPorStoryPts.Keys.OrderBy(sp => Math.Abs(sp - tarefa.storyPoints)).First();
                    somaPrevisao += concluidasPorStoryPts[spMaisProximo];
                }
            }

            return somaPrevisao;
        }
        
    }
}
