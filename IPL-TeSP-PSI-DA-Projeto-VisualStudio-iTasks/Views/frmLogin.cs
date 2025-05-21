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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btLogin; // Define o botão de login como o botão padrão
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            Utilizador utilizador = LoginController.Login(username, password);
            if(utilizador == null)
            {
                MessageBox.Show("Nome de utilizador ou palavra-passe incorretos.");
                return;
            }
            frmKanban kanban = new frmKanban(utilizador);
            this.Hide();
            kanban.ShowDialog();
            this.Close();

        }
    }
}
