using Microsoft.EntityFrameworkCore;

namespace WG_Random_Task_assigner.Object_Folder.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {   
        }
        public DbSet<Shoppinglist> shoppinglist {get; set;}
    }
}
