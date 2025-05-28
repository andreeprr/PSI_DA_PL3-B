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
            }
            else
            {
               txtEstado.Text = EstadoTarefa.ToDo.ToString();
            }
            txtDataRealini.Text
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            string descricao = txtDesc.Text;
            string tipoTarefa = cbTipoTarefa.SelectedItem.ToString();
            string programador = cbProgramador.SelectedItem.ToString();
            string ordem = txtOrdem.Text;
            int storyPoints = int.Parse(txtStoryPoints.Text);
            DateTime dataInicio = dtInicio.Value;
            DateTime dataFim = dtFim.Value;


        }

        private void frmDetalhesTarefa_Load(object sender, EventArgs e)
        {

        }
    }
}
