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
    public partial class frmKanban : Form
    {
        Utilizador utilizadorAutenticado = null;
        public frmKanban(Utilizador utilizador)
        {
            utilizadorAutenticado = utilizador;
            InitializeComponent();
            string username = utilizador.username;
            label1.Text = $"Bem vindo {username}";
        }

        private void frmKanban_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
