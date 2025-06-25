using System;
using iTasks.Models;
using iTasks.Controllers;
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
    public partial class frmConsultaTarefasEmCurso : Form
    {
        Utilizador utilizadorLogado;
        public frmConsultaTarefasEmCurso(Utilizador utilizador)
        {
            InitializeComponent();
            utilizadorLogado = utilizador;
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConsultaTarefasEmCurso_Load(object sender, EventArgs e)
        {
            List<Tarefa> tarefasEmCurso = TarefasController.ObterTarefasEmCurso(utilizadorLogado);
            gvTarefasEmCurso.DataSource = tarefasEmCurso;
        }

        private void gvTarefasEmCurso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
