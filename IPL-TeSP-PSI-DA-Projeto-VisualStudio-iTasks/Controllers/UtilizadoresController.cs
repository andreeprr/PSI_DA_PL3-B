using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    internal class UtilizadoresController
    {
        public static bool AdicionarGestor(Gestor gestor)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    db.Gestores.Add(gestor);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar gestor: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool AdicionarProgramador(Programador programador)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    db.Programadores.Add(programador);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar programador: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
