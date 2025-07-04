﻿using iTasks.Controllers;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace iTasks
{
    public partial class frmKanban : Form
    {
        Utilizador utilizadorAutenticado;
        public frmKanban(Utilizador utilizador)
        {
            utilizadorAutenticado = utilizador; 
            InitializeComponent();
            string nome = utilizador.nome;
            label1.Text = $"Bem vindo {nome}!";
            List<Tarefa> tarefas = TarefasController.ObterTarefasPorUtilizador(utilizadorAutenticado);
            if(utilizadorAutenticado is Programador)
            {
                btNova.Visible = false; // Ocultar o botão "Nova Tarefa" para programadores
                btPrevisao.Visible = false; // Ocultar o botão "Previsão" para programadores
                tarefasEmCursoToolStripMenuItem.Visible = false; // Ocultar o menu de utilizadores para programadores
                utilizadoresToolStripMenuItem.Visible = false; // Ocultar o menu de utilizadores para programadores
                ficheiroToolStripMenuItem.Visible = false; // Ocultar o menu de ficheiro para programadores
            }
        }

        private void frmKanban_Load(object sender, EventArgs e)
        {
            // Carregar a lista de tarefas ao iniciar o formulário
            List<Tarefa> tarefas = TarefasController.ObterTarefasPorUtilizador(utilizadorAutenticado);
            var tarefasTodo = tarefas.Where(tarefa => tarefa.estadoAtual == EstadoTarefa.ToDo).ToList(); // Filtrar tarefas "To Do"
            var tarefasDoing = tarefas.Where(tarefa => tarefa.estadoAtual == EstadoTarefa.Doing).ToList(); // Filtrar tarefas "Doing"
            var tarefasDone = tarefas.Where(tarefa => tarefa.estadoAtual == EstadoTarefa.Done).ToList(); // Filtrar tarefas "Done"

            lstTodo.DataSource = null; // Limpar a fonte de dados antes de definir uma nova
            lstDoing.DataSource = null; // Limpar a fonte de dados antes de definir uma nova
            lstDone.DataSource = null; // Limpar a fonte de dados antes de definir uma nova

            lstTodo.DataSource = tarefasTodo; // Definir a fonte de dados para a lista de tarefas "To Do"
            lstDoing.DataSource = tarefasDoing; // Definir a fonte de dados para a lista de tarefas "Doing"
            lstDone.DataSource = tarefasDone; // Definir a fonte de dados para a lista de tarefas "Done"            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btNova_Click(object sender, EventArgs e)
        {
            Tarefa tarefa = null; 
            if (utilizadorAutenticado is Programador) // verificar se o utilizador autenticado é um programador
            {
                MessageBox.Show("Só um gestor pode criar uma nova tarefa.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frmDetalhesTarefa novaTarefa = new frmDetalhesTarefa(utilizadorAutenticado, tarefa); // criar uma nova tarefa
                novaTarefa.ShowDialog();
            }

            List<Tarefa> tarefas = TarefasController.ObterTarefasPorUtilizador(utilizadorAutenticado); //obter a lista de tarefas da base de dados

            var tarefasTodo = tarefas.Where(tarefa2 => tarefa2.estadoAtual == EstadoTarefa.ToDo).ToList(); // Filtrar tarefas "To Do"
            var tarefasDoing = tarefas.Where(tarefa2 => tarefa2.estadoAtual == EstadoTarefa.Doing).ToList(); // Filtrar tarefas "Doing"
            var tarefasDone = tarefas.Where(tarefa2 => tarefa2.estadoAtual == EstadoTarefa.Done).ToList(); // Filtrar tarefas "Done"

            lstTodo.DataSource = null; // Limpar a fonte de dados antes de definir uma nova
            lstDoing.DataSource = null; // Limpar a fonte de dados antes de definir uma nova
            lstDone.DataSource = null; // Limpar a fonte de dados antes de definir uma nova

            lstTodo.DataSource = tarefasTodo; // Definir a fonte de dados para a lista de tarefas "To Do"
            lstDoing.DataSource = tarefasDoing; // Definir a fonte de dados para a lista de tarefas "Doing"
            lstDone.DataSource = tarefasDone; // Definir a fonte de dados para a lista de tarefas "Done"


        }

        private void btSetDoing_Click(object sender, EventArgs e)
        {
            int index = lstTodo.SelectedIndex; // Verifica o índice selecionado na lista de tarefas "To Do"
            if (index == -1)
            {
                MessageBox.Show("Selecione uma tarefa para mudar o estado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Tarefa tarefaSelecionada = lstTodo.Items[index] as Tarefa; // Obtém a tarefa selecionada na lista "To Do"
            if (tarefaSelecionada == null) 
            {
                MessageBox.Show("Tarefa selecionada inválida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tarefaSelecionada.estadoAtual = EstadoTarefa.Doing;
            tarefaSelecionada.dataRealInicio = DateTime.Now; // Definir a data de início real quando a tarefa é movida para Doing

            TarefasController.AtualizarEstadoTarefa(tarefaSelecionada); // Atualiza o estado da tarefa na base de dados

            frmKanban_Load(null, null); // Recarregar a lista de tarefas para refletir a mudança de estado
        }

        private void btSetTodo_Click(object sender, EventArgs e)
        {
            int index = lstDoing.SelectedIndex; // Verifica o índice selecionado na lista de tarefas "Doing"
            if (index == -1)
            {
                MessageBox.Show("Selecione uma tarefa para mudar o estado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Tarefa tarefaSelecionada = lstDoing.Items[index] as Tarefa; // Obtém a tarefa selecionada na lista "Doing"
            if (tarefaSelecionada == null)
            {
                MessageBox.Show("Tarefa selecionada inválida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tarefaSelecionada.estadoAtual == EstadoTarefa.Done) // Verifica se a tarefa já está concluída
            {
                MessageBox.Show("Tarefas concluídas não podem ser reiniciadas.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tarefaSelecionada.estadoAtual = EstadoTarefa.ToDo;
            tarefaSelecionada.dataRealInicio = null; // Limpar a data de início real quando a tarefa é movida de volta para To Do
            TarefasController.AtualizarEstadoTarefa(tarefaSelecionada);
            frmKanban_Load(null, null); // Recarregar a lista de tarefas para refletir a mudança de estado
        }

        private void btSetDone_Click(object sender, EventArgs e)
        {
            int index = lstDoing.SelectedIndex; // Verifica o índice selecionado na lista de tarefas "Doing"
            if (index == -1)
            {
                MessageBox.Show("Selecione uma tarefa para mudar o estado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Tarefa tarefaSelecionada = lstDoing.Items[index] as Tarefa; // Obtém a tarefa selecionada na lista "Doing"
            if (tarefaSelecionada == null)
            {
                MessageBox.Show("Tarefa selecionada inválida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tarefaSelecionada.estadoAtual = EstadoTarefa.Done;
            tarefaSelecionada.dataRealFim = DateTime.Now; // Definir a data de fim real quando a tarefa é movida para Done
            TarefasController.AtualizarEstadoTarefa(tarefaSelecionada);
            frmKanban_Load(null, null); // Recarregar a lista de tarefas para refletir a mudança de estado
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btPrevisao_Click(object sender, EventArgs e)
        {
            if (lstTodo.Items.Count == 0) 
            {
                MessageBox.Show("Sem tarefas para ver a previsão.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                List<Tarefa> tarefas = new List<Tarefa>();
                foreach (Tarefa tarefa in lstTodo.Items)
                {
                    tarefas.Add(tarefa); // Adiciona as tarefas "To Do" à lista de tarefas
                }
                double previsaoHoras = TarefasController.PrevisaoConclusaoTarefas(tarefas);

                MessageBox.Show($"Tempo previsto para concluir todas as tarefas 'To Do': {previsaoHoras:F1} horas.",
                    "Previsão", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereUtilizadores gereUtilizadores = new frmGereUtilizadores(); // criar uma nova instância do formulário de gestão de utilizadores
            gereUtilizadores.ShowDialog();  // mostrar o formulário de gestão de utilizadores
        }

        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereTiposTarefas gereTarefas = new frmGereTiposTarefas(); // criar uma nova instância do formulário de gestão de tipos de tarefas
            gereTarefas.ShowDialog(); // mostrar o formulário de gestão de tipos de tarefas
        }

        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultarTarefasConcluidas verTarefasConcluidas = new frmConsultarTarefasConcluidas(utilizadorAutenticado); // criar uma nova instância do formulário de consulta de tarefas concluídas
            verTarefasConcluidas.ShowDialog(); // mostrar o formulário de consulta de tarefas concluídas
        }

        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaTarefasEmCurso verTarefasEmCurso = new frmConsultaTarefasEmCurso(utilizadorAutenticado); // criar uma nova instância do formulário de consulta de tarefas em curso
            verTarefasEmCurso.ShowDialog(); // mostrar o formulário de consulta de tarefas em curso
        } 

        private void lstTodo_DoubleClick(object sender, EventArgs e)
        {
            int index = lstTodo.SelectedIndex; // Verifica o índice selecionado na lista de tarefas "To Do"
            Tarefa tarefaSelecionada = lstTodo.Items[index] as Tarefa; // Obtém a tarefa selecionada na lista "To Do"
            frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorAutenticado, tarefaSelecionada);
            detalhesTarefa.ShowDialog();
        }
        private void lstDoing_DoubleClick(object sender, EventArgs e)
        {
            int index = lstDoing.SelectedIndex; // Verifica o índice selecionado na lista de tarefas "Doing"
            Tarefa tarefaSelecionada = lstDoing.Items[index] as Tarefa; // Obtém a tarefa selecionada na lista "Doing"
            frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorAutenticado, tarefaSelecionada);
            detalhesTarefa.ShowDialog();
        }
        private void lstDone_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Tarefas concluídas não podem ser editadas.",
                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void utilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listagensToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ficheiroToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.Title = "Exportar tarefas concluídas";
                sfd.FileName = "tarefas_concluidas.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bool sucesso = TarefasController.ExportarTarefasConcluidasParaCSV(utilizadorAutenticado, sfd.FileName);
                    if (sucesso)
                        MessageBox.Show("Exportação concluída com sucesso!", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
