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
            if (txtNomeGestor.Text == "" || txtUsernameGestor.Text == "" || txtPasswordGestor.Text == "" || 
                cbDepartamento.SelectedItem == null)
            {
                MessageBox.Show("Campo não pode estar vazio");
                return;
            };

            //Criação do objeto Gestor com os dados do formulário
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
                AtualizarComboGestores();
                return;
            }
            else
            {
                MessageBox.Show("Erro ! Não foi possível criar o gestor");
                return;
            }

        }
        private void AtualizarComboGestores()
        {
            cbGestorProg.DataSource = null;
            cbGestorProg.DataSource = UtilizadoresController.ObterGestores();
            cbGestorProg.DisplayMember = "nome"; // Ajuste para o campo que você quiser mostrar
        }

        private void btGravarProg_Click(object sender, EventArgs e)
        {
            // Validação simples para garantir que os campos obrigatórios não estejam vazios
            if (txtNomeProg.Text == "" || txtUsernameProg.Text == "" || txtPasswordProg.Text == "" ||
                cbNivelProg.SelectedItem == null || cbGestorProg.SelectedItem == null)
            {
                MessageBox.Show("Campo não pode estar vazio");
                return;
            }

            // Criação do objeto Programador com os dados do formulário
            var programador = new Programador
            {
                nome = txtNomeProg.Text,
                username = txtUsernameProg.Text,
                password = txtPasswordProg.Text,
                NivelExperiencia = (NivelExperiencia)cbNivelProg.SelectedItem,
                gestor = (Gestor)cbGestorProg.SelectedItem
            };

            // Indica se o programador foi adicionado com sucesso
            bool verificaProg = UtilizadoresController.AdicionarProgramador(programador);
            if (verificaProg == true)
            {
                MessageBox.Show("Programador criado com sucesso");
                // Obtém a lista de programadores da base de dados e popula a ListBox
                List<Utilizador> programadores = UtilizadoresController.ObterProgramadores();
                lstListaProgramadores.DataSource = programadores;
                return;
            }
            else
            {
                MessageBox.Show("Erro! Não foi possível criar o programador");
                return;
            }
        }

        private void frmGereUtilizadores_Load(object sender, EventArgs e)
        // Carrega e mostra os dados iniciais de gestores e programadores nas listas e na ComboBox ao abrir o formulário
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

        private void btNovoGestor_Click(object sender, EventArgs e)
        {
            lstListaGestores.SelectedIndex = -1; // Desseleciona
            LimparCamposGestor();
        }

        private void btEliminarGestor_Click(object sender, EventArgs e)
        {
            // Verifica se algum gestor está selecionado
            if (lstListaGestores.SelectedItem == null)
            {
                MessageBox.Show("Selecione um gestor para eliminar.");
                return;
            }

            // Confirmação com o utilizador
            DialogResult confirm = MessageBox.Show(
                "Tem certeza que deseja eliminar este gestor?\nOs programadores associados também serão eliminados.",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
                return;

            // Converte o item selecionado para Gestor
            Gestor gestor = (Gestor)lstListaGestores.SelectedItem;

            // Elimina o gestor
            bool eliminaGestor = UtilizadoresController.EliminarGestor(gestor);
            if (eliminaGestor)
            {
                MessageBox.Show("Gestor eliminado com sucesso.");
                // Atualiza a lista
                lstListaGestores.DataSource = UtilizadoresController.ObterGestores();
                lstListaProgramadores.DataSource = UtilizadoresController.ObterProgramadores(); // atualiza também os programadores
                AtualizarComboGestores();
            }
            else
            {
                MessageBox.Show("Erro ao eliminar o gestor.");
            }
        }

        private void btLimparProg_Click(object sender, EventArgs e)
        {
            lstListaProgramadores.SelectedIndex = -1; // Desseleciona
            LimparCamposProgramador();
        }

        private void btEliminarProg_Click(object sender, EventArgs e)
        {
            // Verifica se algum programador está selecionado
            if (lstListaProgramadores.SelectedItem == null)
            {
                MessageBox.Show("Selecione um programador para eliminar.");
                return;
            }

            // Confirmação com o utilizador
            DialogResult confirm = MessageBox.Show(
                "Tem certeza que deseja eliminar este programador?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
                return;

            // Converte o item selecionado para Programador
            Programador programador = (Programador)lstListaProgramadores.SelectedItem;

            // Elimina o programador
            bool eliminarProg = UtilizadoresController.EliminarProgramador(programador);
            if (eliminarProg)
            {
                MessageBox.Show("Programador eliminado com sucesso.");
                // Atualiza a lista de programadores
                lstListaProgramadores.DataSource = UtilizadoresController.ObterProgramadores();
            }
            else
            {
                MessageBox.Show("Erro ao eliminar o programador.");
            }
        }

        private void txtIdGestor_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIdProg_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
