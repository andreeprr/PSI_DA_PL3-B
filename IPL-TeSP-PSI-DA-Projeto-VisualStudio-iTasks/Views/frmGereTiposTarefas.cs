using iTasks.Controllers;
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

            var tipoTarefa = new TipoTarefa { nome = txtDesc.Text }; //criar um novo tipo de tarefa com a descrição preenchida

            var success = TipotarefaController.AdicionarTipoTarefa(tipoTarefa); //verificar se foi adicionado a base de dados
            if (success == true)
            {
                MessageBox.Show("Tipo de tarefa adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao adicionar o tipo de tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtDesc.Clear(); //limpar o campo de texto

            //atualizar lista com a base de dados
            List<TipoTarefa> tiposTarefas = TipotarefaController.ObterTiposTarefas(); //obter a lista de tipos de tarefas da base de dados
            lstLista.DataSource = null; //limpar a lista
            lstLista.DataSource = tiposTarefas; //atualizar a lista com a nova lista de tipos de tarefas
        }

        private void frmGereTiposTarefas_Load(object sender, EventArgs e)
        {
            // Carregar a lista de tipos de tarefas ao iniciar o formulário
            List<TipoTarefa> tiposTarefas = TipotarefaController.ObterTiposTarefas(); //obter a lista de tipos de tarefas da base de dados
            lstLista.DataSource = tiposTarefas;
        }
    }
}