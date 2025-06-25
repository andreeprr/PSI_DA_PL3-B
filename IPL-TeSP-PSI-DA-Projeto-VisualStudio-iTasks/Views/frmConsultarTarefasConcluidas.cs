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
    public partial class frmConsultarTarefasConcluidas : Form
    {
        Utilizador utilizadorLogado;
        public frmConsultarTarefasConcluidas(Utilizador utilizador)
        {
            InitializeComponent();
            utilizadorLogado = utilizador;
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConsultarTarefasConcluidas_Load(object sender, EventArgs e)
        {
            List<Tarefa> tarefasConcluidas = TarefasController.ObterTarefasConcluidas(utilizadorLogado);
            gvTarefasConcluidas.DataSource = tarefasConcluidas;
        }
    }
}
