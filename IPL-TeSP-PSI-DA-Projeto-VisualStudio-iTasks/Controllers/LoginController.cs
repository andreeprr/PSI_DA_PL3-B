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
            
        }
        public static Utilizador Login(string username, string password)
        {
            using (var db = new iTasksContext())
            {
                var utilizador = db.Utilizadores.FirstOrDefault(u => u.username == username && u.password == password);
                return utilizador;
            }
        }
    }
}
