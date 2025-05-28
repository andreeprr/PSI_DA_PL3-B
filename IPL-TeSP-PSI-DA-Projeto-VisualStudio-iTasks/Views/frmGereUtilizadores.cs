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

            if (txtNomeGestor.Text == "" || txtUsernameGestor.Text == "" || txtPasswordGestor.Text == "" || cbDepartamento.SelectedItem == null)
            {
                MessageBox.Show("Nome não pode estar vazio");
                return;
            }

            var gestor = new Gestor
            {
                nome = txtNomeGestor.Text,
                username = txtUsernameGestor.Text,
                password = txtPasswordGestor.Text,
                departamento = (Departamento)cbDepartamento.SelectedItem,
                GereUtilizadores = chkGereUtilizadores.Checked
            };
        }

        private void btGravarProg_Click(object sender, EventArgs e)
        {

        }
    }
}
