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

        private void btNova_Click(object sender, EventArgs e)
        {
            if (utilizadorAutenticado is Programador)
            {
                MessageBox.Show("Só um gestor pode criar uma nova tarefa.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frmDetalhesTarefa novaTarefa = new frmDetalhesTarefa();
                novaTarefa.ShowDialog();
            }
        }

        private void btSetDoing_Click(object sender, EventArgs e)
        {
            int index = lstTodo.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Selecione uma tarefa para mudar o estado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string tarefa = lstTodo.Items[index].ToString();
                lstDoing.Items.Add(tarefa);
                lstTodo.Items.RemoveAt(index);
            }
        }

        private void btSetTodo_Click(object sender, EventArgs e)
        {
            int index = lstDoing.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Selecione uma tarefa para mudar o estado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string tarefa = lstDoing.Items[index].ToString();
                lstDoing.Items.RemoveAt(index);
                lstTodo.Items.Add(tarefa);
            }
        }

        private void btSetDone_Click(object sender, EventArgs e)
        {
            int index = lstDoing.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Selecione uma tarefa para mudar o estado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string tarefa = lstDoing.Items[index].ToString();
                lstDoing.Items.RemoveAt(index);
                lstDone.Items.Add(tarefa);
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
