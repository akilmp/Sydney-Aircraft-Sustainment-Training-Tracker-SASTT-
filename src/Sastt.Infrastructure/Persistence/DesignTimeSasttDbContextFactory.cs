using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sastt.Infrastructure.Persistence;

public class DesignTimeSasttDbContextFactory : IDesignTimeDbContextFactory<SasttDbContext>
{
    public SasttDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SasttDbContext>();
        optionsBuilder.UseOracle(
            "User Id=SASTT;Password=Passw0rd!;Data Source=localhost:1521/XEPDB1;");
        return new SasttDbContext(optionsBuilder.Options);
    }
}
