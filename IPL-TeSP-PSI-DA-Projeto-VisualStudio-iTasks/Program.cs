using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTasks.Models;

namespace iTasks
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Abrir ligação com a base de dados
            using (var db = new iTasksContext())
            {

                if (db.Utilizadores.FirstOrDefault(prog => prog.username == "João") == null)
                {
                    var programador = new Programador
                    {
                        nome = "João",
                        username = "joao123",
                        password = "password123",
                        NivelExperiencia = NivelExperiencia.Junior

                    };
                    db.Programadores.Add(programador);
                }
                if (db.Utilizadores.FirstOrDefault(gest => gest.username == "Manuel") == null)
                {
                    var gestor = new Gestor
                    {
                        nome = "Manuel",
                        username = "manuel123",
                        password = "password456",
                        departamento = Departamento.IT
                    };
                    db.Gestores.Add(gestor);
                }
                db.SaveChanges();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());



        }
    }
}
