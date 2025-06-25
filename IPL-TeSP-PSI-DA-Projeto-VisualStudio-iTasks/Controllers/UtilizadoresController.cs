using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
                    // Certifica-se de que o gestor está anexado ao programador
                    db.Gestores.Attach(programador.gestor); 
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

        public static bool AtualizarGestor(Gestor gestor)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    var gestorDb = db.Gestores.FirstOrDefault(g => g.id == gestor.id);
                    if (gestorDb == null)
                        return false;

                    gestorDb.nome = gestor.nome; 
                    gestorDb.username = gestor.username; 
                    gestorDb.password = gestor.password; 
                    gestorDb.departamento = gestor.departamento; 
                    gestorDb.GereUtilizadores = gestor.GereUtilizadores; 


                    db.SaveChanges(); // Certifica-se de que as alterações são salvas na base de dados
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar gestor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool AtualizarProgramador(Programador programador)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    var programadorDb = db.Programadores.FirstOrDefault(p => p.id == programador.id);
                    if (programadorDb == null)
                        return false;

                    programadorDb.nome = programador.nome; 
                    programadorDb.username = programador.username; 
                    programadorDb.password = programador.password; 
                    programadorDb.NivelExperiencia = programador.NivelExperiencia; 

                    programadorDb.gestor = db.Gestores.Find(programador.gestor.id);


                    db.SaveChanges(); // Certifica-se de que as alterações são salvas na base de dados
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar programador: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static List<Utilizador> ObterGestores()
        {
            using (var db = new iTasksContext())
            {
                //Devolve a lista de gestores
                return db.Gestores.ToList<Utilizador>();
            }
        }

        public static List<Programador> ObterProgramadoresPorGestor(Utilizador utilizador)
        {
            using (var db = new iTasksContext())
            {
                //Devolve a lista de programadores associados ao respetivo gestor
                return db.Programadores.Where(programador=> programador.gestor.id == utilizador.id).ToList();
            }
        }

        public static List<Utilizador> ObterProgramadores()
        {
            using (var db = new iTasksContext())
            {
                //Devolve a lista de programadores
                return db.Programadores.Include("Gestor").ToList<Utilizador>();
            }
        }

        public static bool EliminarGestor(Gestor gestor)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    // Anexa o gestor ao contexto, caso não esteja
                    db.Gestores.Attach(gestor);
                    // Busca os programadores associados a este gestor
                    var programadoresAssociados = db.Programadores.Where(p => p.gestor.id == gestor.id).ToList();
                    // Remove os programadores associados
                    db.Programadores.RemoveRange(programadoresAssociados);
                    //Remove o Gestor
                    db.Gestores.Remove(gestor);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao eliminar gestor: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool EliminarProgramador(Programador programador)
        {
            try
            {
                using (var db = new iTasksContext())
                {
                    // Anexa o programador ao contexto para garantir que ele está sendo rastreado
                    db.Programadores.Attach(programador);

                    // Remove o programador anexado ao contexto
                    db.Programadores.Remove(programador);

                    // Salva a exclusão no banco
                    db.SaveChanges();
                }
                return true; // Exclusão bem-sucedida
            }
            catch (Exception ex)
            {
                // Exibe erro se falhar a exclusão
                MessageBox.Show($"Erro ao eliminar programador: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Exclusão falhou
            }
        }
    }
}
