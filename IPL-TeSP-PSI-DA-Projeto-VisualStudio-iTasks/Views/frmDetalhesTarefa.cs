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
                txtDataRealini.Text = tarefa.dataRealInicio.ToString("dd/MM/yyyy");
                txtdataRealFim.Text = tarefa.dataRealFim.ToString("dd/MM/yyyy");
                txtDataCriacao.Text = tarefa.dataCriacao.ToString("dd/MM/yyyy");
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
            Tarefa tarefa = new Tarefa();
            tarefa.descricao = txtDesc.Text;
            tarefa.tipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;
            tarefa.programador = (Programador)cbProgramador.SelectedItem;
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
