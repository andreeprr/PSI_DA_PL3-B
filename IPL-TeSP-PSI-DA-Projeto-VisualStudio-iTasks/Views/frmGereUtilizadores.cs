using iTasks.Controllers;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            cbDepartamento.DataSource = Enum.GetValues(typeof(Departamento));
            cbNivelProg.DataSource = Enum.GetValues(typeof(NivelExperiencia));

        }

        private void lstListaGestores_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btGravarGestor_Click(object sender, EventArgs e)
        {

            if (txtNomeGestor.Text == "" || txtUsernameGestor.Text == "" || txtPasswordGestor.Text == "" || cbDepartamento.SelectedItem == null)
            {
                MessageBox.Show("Campo não pode estar vazio");
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

            bool verificaGestor = UtilizadoresController.AdicionarGestor(gestor);
            if (verificaGestor == true)
            {
                MessageBox.Show("Gestor criado com sucesso");
                return;
            }
            else
            {
                MessageBox.Show("Erro ! Não foi possível criar o gestor");
                return;
            }
        }

        private void btGravarProg_Click(object sender, EventArgs e)
        {
            if (txtNomeProg.Text == "" || txtUsernameProg.Text == "" || txtPasswordProg.Text == "" || cbNivelProg.SelectedItem == null || cbGestorProg == null)
            {
                MessageBox.Show("Campo não pode estar vazio");
                return;
            }

            var programador = new Programador
            {
                nome = txtNomeProg.Text,
                username = txtUsernameProg.Text,
                password = txtPasswordProg.Text,
                NivelExperiencia = (NivelExperiencia)cbNivelProg.SelectedItem,
                gestor = (Gestor)cbGestorProg.SelectedItem
            };

            bool verificaProgramador = UtilizadoresController.AdicionarProgramador(programador);
            if (verificaProgramador == true)
            {
                MessageBox.Show("Programador criado com sucesso");
                return;
            }
            else
            {
                MessageBox.Show("Erro ! Não foi possível criar o programador");
                return;
            }
        }
    }
}
