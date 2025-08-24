using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CafeManager.Infrastructure.Persistence;

public class CafeDbContextFactory : IDesignTimeDbContextFactory<CafeDbContext>
{
    public CafeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CafeDbContext>();

        optionsBuilder.UseSqlServer("Server=localhost;Database=CafeDb;Trusted_Connection=True;TrustServerCertificate=True;");

        return new CafeDbContext(optionsBuilder.Options);
    }
}
