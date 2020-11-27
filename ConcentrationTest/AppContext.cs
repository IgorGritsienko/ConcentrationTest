using System.Data.Entity;

namespace ConcentrationTest
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }   // название переменной должно быть таким же, как и назавние таблицы
        public DbSet<Stat> Stats { get; set; }

        public AppContext() : base("DefaultConnection") { }    // передача в родительский конструктор DefaultConnection
    }
}