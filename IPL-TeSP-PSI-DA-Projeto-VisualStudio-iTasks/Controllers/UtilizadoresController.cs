using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
                return false;
            }
        }

        public static List<Utilizador> ObterProgramadores()
        {
            using (var db = new iTasksContext())
            {
                return db.Programadores.ToList<Utilizador>();
            }
        }

        public static List<Utilizador> ObterGestores()
        {
            using (var db = new iTasksContext())
            {
                return db.Gestores.ToList<Utilizador>();
            }
        }
    }
}
