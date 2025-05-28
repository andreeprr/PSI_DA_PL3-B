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
            if (string.IsNullOrWhiteSpace(txtDesc.Text))
            {
                MessageBox.Show("Por favor, preencha a descrição do tipo de tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var tipoTarefa = new TipoTarefa { nome = txtDesc.Text };

            var success = TipotarefaController.AdicionarTipoTarefa(tipoTarefa);
            if (success == true)
            {
                MessageBox.Show("Tipo de tarefa adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao adicionar o tipo de tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }
    }
}