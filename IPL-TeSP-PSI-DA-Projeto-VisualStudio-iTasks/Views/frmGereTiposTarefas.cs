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
    public partial class frmGereTiposTarefas : Form
    {
        TipoTarefa tipoTarefaSelecionada;
        public frmGereTiposTarefas()
        {
            InitializeComponent();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDesc.Text)) //verificar se esta vazio
            {
                MessageBox.Show("Por favor, preencha a descrição do tipo de tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int index = lstLista.SelectedIndex;
            if (index >= 0)
            {
                tipoTarefaSelecionada = lstLista.Items[index] as TipoTarefa;
            }
            else
            {
                tipoTarefaSelecionada = null;
            }
            if (tipoTarefaSelecionada == null)
            {
                // Criação do objeto Programador com os dados do formulário
                var tipoTarefa = new TipoTarefa
                {
                    nome = txtDesc.Text
                };

                // Indica se o programador foi adicionado com sucesso
                bool verificaTipoTarefa = TipotarefaController.AdicionarTipoTarefa(tipoTarefa);
                if (verificaTipoTarefa== true)
                {
                    MessageBox.Show("Tipo de tarefa criado com sucesso");
                    // Obtém a lista de tipos de tarefa da base de dados e popula a ListBox
                    List<TipoTarefa> tiposTarefas= TipotarefaController.ObterTiposTarefas();
                    lstLista.DataSource = null;
                    lstLista.DataSource = tiposTarefas;
                    return;
                }
                else
                {
                    MessageBox.Show("Erro! Não foi possível criar tipo de tarefa");
                    return;
                }
            }
            else
            {
                tipoTarefaSelecionada.nome = txtDesc.Text;

                bool tipoTarefaAtualizada = TipotarefaController.AtualizarTipoTarefa(tipoTarefaSelecionada);

                // Verifica se o programador foi atualizado com sucesso
                if (tipoTarefaAtualizada)
                {
                    MessageBox.Show("Tipo de tarefa atualizado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<TipoTarefa> tiposTarefas = TipotarefaController.ObterTiposTarefas();
                    lstLista.DataSource = null;
                    lstLista.DataSource = tiposTarefas;
                    return;
                }
                else
                {
                    MessageBox.Show("Erro! Não foi possível atualizar o tipo de tarefa", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }
        private void btEliminarTipoTarefa_Click(object sender, EventArgs e)
        {
            // Verifica se algum tipo de tarefa está selecionado
            if (lstLista.SelectedItem == null)
            {
                MessageBox.Show("Selecione um tipo de tarefa para eliminar.");
                return;
            }

            // Converte o item selecionado para Gestor
            TipoTarefa tipoTarefa= (TipoTarefa)lstLista.SelectedItem;

            // Elimina o gestor
            bool eliminaTipoTarefa = TipotarefaController.EliminarTipoTarefa(tipoTarefa);
            if (eliminaTipoTarefa)
            {
                MessageBox.Show("Tipo de tarefa eliminado com sucesso.");
                // Atualiza a lista
                lstLista.DataSource = null; // Limpa a lista para evitar problemas de atualização
                lstLista.DataSource = TipotarefaController.ObterTiposTarefas();
            }
            else
            {
                MessageBox.Show("Erro ao eliminar tipo de tarefa.");
            }
        }

        private void frmGereTiposTarefas_Load(object sender, EventArgs e)
        {
            // Carregar a lista de tipos de tarefas ao iniciar o formulário
            List<TipoTarefa> tiposTarefas = TipotarefaController.ObterTiposTarefas(); //obter a lista de tipos de tarefas da base de dados
            lstLista.DataSource = tiposTarefas;

            lstLista.SelectedIndex = -1;
        }

        private void lstLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Verifica se algum item está selecionado na lista antes de preencher os campos
            if (lstLista.SelectedItem != null)
            {
                //Campos preenchidos ao selecionar tipo de tarefa
                TipoTarefa tipoTarefa= (TipoTarefa)lstLista.SelectedItem;
                txtId.Text = tipoTarefa.id.ToString();
                txtDesc.Text = tipoTarefa.nome;
            }
            else
            {
                LimparCampos();
            }
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtDesc.Text = "";
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            lstLista.SelectedIndex = -1; // Limpa a seleção da lista
            LimparCampos(); // Limpa os campos de texto
        }
    }
}