using Microsoft.EntityFrameworkCore;

namespace ReviewAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }


        public DbSet<Entities.Review> Reviews { get; set; }
    }
}
