using Microsoft.EntityFrameworkCore;

public class AppDataContext : DbContext
{
    public DbSet<Funcionario> Funcionarios {get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=GustavoZavatti_FabricioMilano.db");
    }
}