using iTasks.Controllers;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmKanban : Form
    {
        Utilizador utilizadorAutenticado = null;
        public frmKanban(Utilizador utilizador)
        {
            utilizadorAutenticado = utilizador;
            InitializeComponent();
            string nome = utilizador.nome;
            label1.Text = $"Bem vindo {nome}!";
            List<Tarefa> tarefas = TarefasController.ObterTarefasPorUtilizador(utilizadorAutenticado);
        }

        private void frmKanban_Load(object sender, EventArgs e)
        {
            // Carregar a lista de tarefas ao iniciar o formulário
            List<Tarefa> tarefas = TarefasController.ObterTarefas(); //obter a lista de tarefas da base de dados

            var tarefasTodo = tarefas.Where(tarefa => tarefa.estadoAtual == EstadoTarefa.ToDo).ToList();
            var tarefasDoing = tarefas.Where(tarefa => tarefa.estadoAtual == EstadoTarefa.Doing).ToList();
            var tarefasDone = tarefas.Where(tarefa => tarefa.estadoAtual == EstadoTarefa.Done).ToList();

            lstTodo.DataSource = null; // Limpar a fonte de dados antes de definir uma nova
            lstDoing.DataSource = null; // Limpar a fonte de dados antes de definir uma nova
            lstDone.DataSource = null; // Limpar a fonte de dados antes de definir uma nova

            lstTodo.DataSource = tarefasTodo; // Definir a fonte de dados para a lista de tarefas "To Do"
            lstDoing.DataSource = tarefasDoing; // Definir a fonte de dados para a lista de tarefas "Doing"
            lstDone.DataSource = tarefasDone; // Definir a fonte de dados para a lista de tarefas "Done"

            lstTodo.DisplayMember = "descricao"; // Exibir a descrição da tarefa na lista "To Do"
            lstDoing.DisplayMember = "descricao"; // Exibir a descrição da tarefa na lista "Doing"
            lstDone.DisplayMember = "descricao"; // Exibir a descrição da tarefa na lista "Done"
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btNova_Click(object sender, EventArgs e)
        {
            Tarefa tarefa = null;
            if (utilizadorAutenticado is Programador)
            {
                MessageBox.Show("Só um gestor pode criar uma nova tarefa.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frmDetalhesTarefa novaTarefa = new frmDetalhesTarefa(utilizadorAutenticado, tarefa);
                novaTarefa.ShowDialog();
            }
        }

        private void btSetDoing_Click(object sender, EventArgs e)
        {
            int index = lstTodo.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Selecione uma tarefa para mudar o estado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string tarefa = lstTodo.Items[index].ToString();
                lstDoing.Items.Add(tarefa);
                lstTodo.Items.RemoveAt(index);
            }
        }

        private void btSetTodo_Click(object sender, EventArgs e)
        {
            int index = lstDoing.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Selecione uma tarefa para mudar o estado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string tarefa = lstDoing.Items[index].ToString();
                lstDoing.Items.RemoveAt(index);
                lstTodo.Items.Add(tarefa);
            }
        }

        private void btSetDone_Click(object sender, EventArgs e)
        {
            int index = lstDoing.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Selecione uma tarefa para mudar o estado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string tarefa = lstDoing.Items[index].ToString();
                lstDoing.Items.RemoveAt(index);
                lstDone.Items.Add(tarefa);
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btPrevisao_Click(object sender, EventArgs e)
        {
            int index = lstDoing.SelectedIndex;
            if (index == -1) 
            {
                MessageBox.Show("Selecione uma tarefa para ver a previsão.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string tarefa = lstDoing.Items[index].ToString();
                // Aqui deve-se calcular a previsão com base na tarefa selecionada
                // e mostrar a previsão em uma nova janela ou mensagem.
            }
        }

        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (utilizadorAutenticado is Programador)
            {
                MessageBox.Show("Só um gestor pode gerir utilizadores.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frmGereUtilizadores gereUtilizadores = new frmGereUtilizadores();
                gereUtilizadores.ShowDialog();
            }
        }

        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (utilizadorAutenticado is Programador)
            {
                MessageBox.Show("Só um gestor pode gerir tarefas.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frmGereTiposTarefas gereTarefas = new frmGereTiposTarefas();
                gereTarefas.ShowDialog();
            }
        }

        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultarTarefasConcluidas verTarefasConcluidas = new frmConsultarTarefasConcluidas();
            verTarefasConcluidas.ShowDialog();
        }

        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaTarefasEmCurso verTarefasEmCurso = new frmConsultaTarefasEmCurso();
            verTarefasEmCurso.ShowDialog();
        }

    }
}
