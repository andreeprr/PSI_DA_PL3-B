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
    public partial class frmDetalhesTarefa : Form
    {
        public frmDetalhesTarefa(Utilizador utilizador, Tarefa tarefa)
        {
            InitializeComponent();
            txtId.Text = utilizador.id.ToString();
            if (tarefa != null) 
            {
                txtEstado.Text = tarefa.estadoAtual.ToString();
                txtDataRealini.Text = tarefa.dataRealInicio.ToString("dd/MM/yyyy HH:mm:ss");
                txtdataRealFim.Text = tarefa.dataRealFim.ToString("dd/MM/yyyy HH:mm:ss");
                txtDataCriacao.Text = tarefa.dataCriacao.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else
            {
               txtEstado.Text = EstadoTarefa.ToDo.ToString();
                txtDataRealini.Text = "";
                txtdataRealFim.Text = "";
                txtDataCriacao.Text = "";
            }

            
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDesc.Text) || cbTipoTarefa.SelectedItem == null || cbProgramador.SelectedItem == null 
                || int.Parse(txtOrdem.Text) <= 0 || int.Parse(txtStoryPoints.Text) <= 0 || dtInicio.Value < DateTime.Now || dtInicio.Value < DateTime.Now)
            {
                MessageBox.Show("Por favor, preencha todos os campos corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var tarefa = new Tarefa
            {
                descricao = txtDesc.Text,
                tipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem,
                programador = (Programador)cbProgramador.SelectedItem,
                ordemExecucao = int.Parse(txtOrdem.Text),
                storyPoints = int.Parse(txtStoryPoints.Text),
                dataPrevistaInicio = dtInicio.Value,
                dataPrevistaFim = dtFim.Value,
                dataCriacao = DateTime.Now,
                estadoAtual = EstadoTarefa.ToDo
            };
            var tarefaCriada = TarefasController.AdicionarTarefa(tarefa);
            if (tarefaCriada== true) 
            {

                MessageBox.Show("Tarefa criada com sucesso","Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Erro! Não foi possível criar a tarefa","Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        private void frmDetalhesTarefa_Load(object sender, EventArgs e)
        {
            var tiposTarefas = TipotarefaController.ObterTiposTarefas();
            cbTipoTarefa.DataSource = tiposTarefas;
            cbTipoTarefa.DisplayMember = "nome"; // Exibir o nome do tipo de tarefa
            cbTipoTarefa.ValueMember = "id"; // Usar o ID como valor

            var programadores = UtilizadoresController.ObterProgramadores();
            cbProgramador.DataSource = programadores;
            cbProgramador.DisplayMember = "nome"; // Exibir o nome do programador
            cbProgramador.ValueMember = "id"; // Usar o ID como valor
        }
    }
}
