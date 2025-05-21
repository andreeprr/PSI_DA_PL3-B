using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace iTasks.Models
{
    public class iTasksContext : DbContext
    {
        public DbSet<TipoTarefa> TipoTarefas { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Utilizador> Utilizadores{ get; set; }
        public DbSet<Programador> Programadores { get; set; }
        public DbSet<Gestor> Gestores { get; set; }
    }
}
