using FinaceiroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinaceiroAPI.Data
{
    public class FinanceiroAPIContext : DbContext
    {

        public FinanceiroAPIContext(DbContextOptions<FinanceiroAPIContext> context)
            :base(context) 
        {
            
        }

        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

    }
}
