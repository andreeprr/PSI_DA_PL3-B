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
            //Verifica se algum item está selecionado na lista antes de preencher os campos do gestor
            if (lstListaGestores.SelectedItem != null)
            {
                //Campos preenchidos ao selecionar gestor
                Gestor gestor = (Gestor)lstListaGestores.SelectedItem;
                txtIdGestor.Text = gestor.id.ToString();
                txtNomeGestor.Text = gestor.nome;
                txtUsernameGestor.Text = gestor.username;
                txtPasswordGestor.Text = gestor.password;
                cbDepartamento.SelectedItem = gestor.departamento;
                chkGereUtilizadores.Checked = gestor.GereUtilizadores;
            }else 
            {
                LimparCamposGestor();
            }
        }

        private void lstListaProgramadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Verifica se algum item está selecionado na lista antes de preencher os campos do gestor
            if (lstListaProgramadores.SelectedItem != null)
            {
                //Campos preenchidos ao selecionar programador
                Programador programador = (Programador)lstListaProgramadores.SelectedItem;
                txtIdProg.Text = programador.id.ToString();
                txtNomeProg.Text = programador.nome;
                txtUsernameProg.Text = programador.username;
                txtPasswordProg.Text = programador.password;
                cbNivelProg.SelectedItem = programador.NivelExperiencia;
                cbGestorProg.SelectedItem = programador.gestor;
            }else
            {
                LimparCamposProgramador();
            }
        }

        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            //Validação simples para garantir que os campos obrigatórios não estejam vazios
            if (txtNomeGestor.Text == "" || txtUsernameGestor.Text == "" || txtPasswordGestor.Text == "" || cbDepartamento.SelectedItem == null)
            {
                MessageBox.Show("Campo não pode estar vazio");
                return;
            }

            //Criação do objeto Programador com os dados do formulário
            var gestor = new Gestor
            {
                nome = txtNomeGestor.Text,
                username = txtUsernameGestor.Text,
                password = txtPasswordGestor.Text,
                departamento = (Departamento)cbDepartamento.SelectedItem,
                GereUtilizadores = chkGereUtilizadores.Checked
            };

            //Indica se o gestor foi adicionado com sucesso (true) ou não (false), por isso usamos o bool
            bool verificaGestor = UtilizadoresController.AdicionarGestor(gestor);
            if (verificaGestor == true)
            {
                MessageBox.Show("Gestor criado com sucesso");
                //Obtém a lista de gestores da base de dados e popula a ListBox
                List<Utilizador> gestores = UtilizadoresController.ObterGestores();
                lstListaGestores.DataSource = gestores;
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
            //Validação simples para garantir que os campos obrigatórios não estejam vazios
            if (txtNomeGestor.Text == "" || txtUsernameGestor.Text == "" || txtPasswordGestor.Text == "" || cbDepartamento.SelectedItem == null)
            {
                MessageBox.Show("Campo não pode estar vazio");
                return;
            }

            //Criação do objeto Gestor com os dados do formulário
            var gestor = new Gestor
            {
                nome = txtNomeGestor.Text,
                username = txtUsernameGestor.Text,
                password = txtPasswordGestor.Text,
                departamento = (Departamento)cbDepartamento.SelectedItem,
                GereUtilizadores = chkGereUtilizadores.Checked
            };

            //Indica se o gestor foi adicionado com sucesso (true) ou não (false), por isso usamos o bool para verificar o resultado
            bool verificaGestor = UtilizadoresController.AdicionarGestor(gestor);
            if (verificaGestor == true)
            {
                MessageBox.Show("Gestor criado com sucesso");
                //Obtém a lista de programadores da base de dados e popula a ListBox
                List<Utilizador> gestores = UtilizadoresController.ObterGestores();
                lstListaGestores.DataSource = gestores;
                return;
            }
            else
            {
                MessageBox.Show("Erro! Não foi possível criar o gestor");
                return;
            }
        }

        private void frmGereUtilizadores_Load(object sender, EventArgs e)
        {
            //Carregar as listas de gestores e programadores ao iniciar o formulário
            List<Utilizador> programadores= UtilizadoresController.ObterProgramadores(); //obter a lista de tipos de tarefas da base de dados
            lstListaProgramadores.DataSource = programadores;

            List<Utilizador> gestores = UtilizadoresController.ObterGestores(); //obter a lista de tipos de tarefas da base de dados
            List<Utilizador> gestoresCombobox = UtilizadoresController.ObterGestores();
            lstListaGestores.DataSource = gestores;

            //Popular a ComboBox de gestores para programadores
            cbGestorProg.DataSource = gestoresCombobox;

            //Define o índice selecionado da lista como -1 para garantir que nenhum item esteja selecionado ao iniciar o programa
            lstListaGestores.SelectedIndex = -1;
            lstListaProgramadores.SelectedIndex = -1;
        }
        private void LimparCamposGestor()
        {
            txtIdGestor.Text = "";
            txtNomeGestor.Text = "";
            txtUsernameGestor.Text = "";
            txtPasswordGestor.Text = "";
            cbDepartamento.SelectedIndex = -1;
            chkGereUtilizadores.Checked = false;
        }
        private void LimparCamposProgramador()
        {
            txtIdProg.Text = "";
            txtNomeProg.Text = "";
            txtUsernameProg.Text = "";
            txtPasswordProg.Text = "";
            cbNivelProg.SelectedIndex = -1;
            cbGestorProg.SelectedIndex = -1;
        }
    }
}
