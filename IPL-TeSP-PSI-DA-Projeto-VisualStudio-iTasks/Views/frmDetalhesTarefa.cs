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
        private Utilizador utilizador_ { get; set; }
        public frmDetalhesTarefa(Utilizador utilizador, Tarefa tarefa)
        {
            InitializeComponent();
            tarefa_ = tarefa;
            utilizador_ = utilizador;
            
            if(tarefa == null)
            {
                txtIdGestor.Text = utilizador.id.ToString();
                var tarefas = TarefasController.ObterTarefas();
                int maxId;
                if (tarefas.Any()) 
                {
                    maxId = tarefas.Max(t => t.id) + 1; // Incrementa o ID máximo encontrado
                    txtId.Text = maxId.ToString(); // Define o ID da nova tarefa
                }
                else
                {
                    maxId = 1; // Se não houver tarefas, começa com ID 1
                    txtId.Text = maxId.ToString(); // Define o ID da nova tarefa
                }
                txtEstado.Text = EstadoTarefa.ToDo.ToString(); // Define o estado inicial da tarefa
                txtDataCriacao.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); // Define a data de criação da tarefa
            }
            else if (tarefa != null && utilizador_ is Gestor) 
            {
                txtIdGestor.Text = tarefa.gestor.id.ToString();
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
            else if (tarefa != null && utilizador_ is Programador)
            {
                txtIdGestor.Text = tarefa.gestor.id.ToString();
                btGravar.Visible = false;
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

            //Verifica se existe alguma tarefa para saber se atualiza ou se cria uma nova
            if(tarefa_ != null)
            {
                tarefa_.descricao = txtDesc.Text;
                tarefa_.programador = (Programador)cbProgramador.SelectedItem;
                tarefa_.tipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;
                tarefa_.ordemExecucao = int.Parse(txtOrdem.Text);
                tarefa_.storyPoints = int.Parse(txtStoryPoints.Text);
                tarefa_.dataPrevistaInicio = dtInicio.Value;
                tarefa_.dataPrevistaFim = dtFim.Value;
                bool tarefaAtualizada = TarefasController.AtualizarTarefa(tarefa_);
                if (tarefaAtualizada == true)
                {

                    MessageBox.Show("Tarefa atualizada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Erro! Não foi possível atualizar a tarefa", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            } 
            else
            {
                var tarefa = new Tarefa
                {
                    descricao = txtDesc.Text,
                    gestor = utilizador_ as Gestor, // Certifica-se de que o gestor está anexado à tarefa 
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
                if (tarefaCriada == true)
                {

                    MessageBox.Show("Tarefa criada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Erro! Não foi possível criar a tarefa", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }
        private void frmDetalhesTarefa_Load(object sender, EventArgs e)
        {
            var tiposTarefas = TipotarefaController.ObterTiposTarefas();
            cbTipoTarefa.DataSource = tiposTarefas;

            var programadores = UtilizadoresController.ObterProgramadoresPorGestor(utilizador_);
            cbProgramador.DataSource = programadores;

            // Seleciona automaticamente o tipoTarefa e o programador associados à tarefa
            if (tarefa_ != null)
            {
                if (tarefa_.tipoTarefa != null)
                    cbTipoTarefa.SelectedItem = tiposTarefas.FirstOrDefault(t => t.id == tarefa_.tipoTarefa.id);

                if (tarefa_.programador != null)
                    cbProgramador.SelectedItem = programadores.FirstOrDefault(p => p.id == tarefa_.programador.id);
            }
        }
    }
}
