using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    internal class LoginController
    {
        public LoginController(string username, string password) 
        {
            using (var db = new iTasksContext())
            {
                var utilizador = db.Utilizadores.FirstOrDefault(u => u.username == username && u.password == password);
                if (utilizador != null)
                {
                    // Login bem-sucedido
                    MessageBox.Show("Login bem-sucedido!");
                    // Aqui você pode abrir o formulário principal ou fazer outra ação
                    frmKanban kanban = new frmKanban();
                    kanban.Show();
                }
                else
                {
                    // Login falhou
                    MessageBox.Show("Nome de utilizador ou palavra-passe incorretos.");
                }
            }
        }
    }
}
