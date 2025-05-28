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
    public partial class frmGereUtilizadores : Form
    {
        public frmGereUtilizadores()
        {
            InitializeComponent();
        }

        private void lstListaGestores_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            string nome = txtNomeGestor.Text;
            if (nome == "")
            {
                MessageBox.Show("Nome não pode estar vazio");
                return;
            }
            
        }

        private void btGravarProg_Click(object sender, EventArgs e)
        {
            string nome = txtNomeProg.Text;
            if (nome == "")
            {
                MessageBox.Show("Nome não pode estar vazio");
                return;
            }
            
        }
    }
}
