using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    internal class TipotarefaController
    {
        public static bool AdicionarTipoTarefa(TipoTarefa tipoTarefa)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    db.TipoTarefas.Add(tipoTarefa);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) {
                MessageBox.Show($"Erro ao adicionar tipo de tarefa: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            
        }
    }
}
