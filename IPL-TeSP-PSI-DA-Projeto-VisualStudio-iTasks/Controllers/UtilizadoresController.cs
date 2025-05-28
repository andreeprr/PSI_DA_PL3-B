using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return false;
            }
        }
    }
}
