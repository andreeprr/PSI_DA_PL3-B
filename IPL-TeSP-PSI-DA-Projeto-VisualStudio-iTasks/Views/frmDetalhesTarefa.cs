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
        private Tarefa tarefa_ { get; set; }
        public frmDetalhesTarefa(Utilizador utilizador, Tarefa tarefa)
        {
            InitializeComponent();
            tarefa = tarefa_;
            txtIdGestor.Text = utilizador.id.ToString();
            if (tarefa != null && utilizador is Gestor) 
            {
                txtId.Text = tarefa.id.ToString();
                txtEstado.Text = tarefa.estadoAtual.ToString();
                if (tarefa.dataRealInicio !=null)
                {
                    txtDataRealini.Text = tarefa.dataRealInicio.Value.ToString("dd/MM/yyyy HH:mm:ss");
                }
                if (tarefa.dataRealFim != null)
                {
                    txtdataRealFim.Text = tarefa.dataRealFim.Value.ToString("dd/MM/yyyy HH:mm:ss");
                }
                txtDataCriacao.Text = tarefa.dataCriacao.ToString("dd/MM/yyyy HH:mm:ss");
                txtDesc.Text = tarefa.descricao;
                cbTipoTarefa.SelectedItem = tarefa.tipoTarefa;
                cbProgramador.SelectedItem = tarefa.programador;
                txtOrdem.Text = tarefa.ordemExecucao.ToString();
                txtStoryPoints.Text = tarefa.storyPoints.ToString();
                dtInicio.Value = tarefa.dataPrevistaInicio;
                dtFim.Value = tarefa.dataPrevistaFim;

            }
            else if (tarefa != null && utilizador is Programador)
            {
                btGravar.Enabled = false;
                txtId.Text = tarefa.id.ToString();
                txtEstado.Text = tarefa.estadoAtual.ToString();
                if (tarefa.dataRealInicio != null)
                {
                    txtDataRealini.Text = tarefa.dataRealInicio.Value.ToString("dd/MM/yyyy HH:mm:ss");
                }
                if (tarefa.dataRealFim != null)
                {
                    txtdataRealFim.Text = tarefa.dataRealFim.Value.ToString("dd/MM/yyyy HH:mm:ss");
                }
                txtDataCriacao.Text = tarefa.dataCriacao.ToString("dd/MM/yyyy HH:mm:ss");
                txtDesc.Text = tarefa.descricao;
                cbTipoTarefa.SelectedItem = tarefa.tipoTarefa;
                cbProgramador.SelectedItem = tarefa.programador;
                txtOrdem.Text = tarefa.ordemExecucao.ToString();
                txtStoryPoints.Text = tarefa.storyPoints.ToString();
                dtInicio.Value = tarefa.dataPrevistaInicio;
                dtFim.Value = tarefa.dataPrevistaFim;


            }
            else
            {
                txtId.Text = "";
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
            TarefasController.AtualizarTarefa(tarefa_);
            if (string.IsNullOrWhiteSpace(txtDesc.Text) || cbTipoTarefa.SelectedItem == null || cbProgramador.SelectedItem == null 
                || int.Parse(txtOrdem.Text) <= 0 || int.Parse(txtStoryPoints.Text) <= 0 || dtInicio.Value < DateTime.Now || dtInicio.Value < DateTime.Now)
            {
                MessageBox.Show("Por favor, preencha todos os campos corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int tarefasAtivas = TarefasController.tarefasAtivas((Programador)cbProgramador.SelectedItem);

            if (tarefasAtivas >= 2)
            {
                MessageBox.Show("Este programador já tem 2 tarefas atribuídas.", "Limite atingido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Não permite atribuir mais tarefas
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

            var programadores = UtilizadoresController.ObterProgramadores();
            cbProgramador.DataSource = programadores;
        }
    }
}
