using System.Threading.Tasks;
using Dapper;
using Testcontainers.Oracle;
using Xunit;

namespace Sastt.IntegrationTests;

public class OracleRepositoryTests : IAsyncLifetime
{
    private readonly OracleContainer _oracle = new OracleBuilder().Build();

    public async Task InitializeAsync()
    {
        await _oracle.StartAsync();
        using var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_oracle.GetConnectionString());
        await connection.ExecuteAsync("CREATE TABLE Aircraft (TailNumber VARCHAR2(20) PRIMARY KEY)");
    }

    public async Task DisposeAsync()
    {
        await _oracle.DisposeAsync();
    }

    [Fact]
    public async Task Can_insert_and_read_aircraft()
    {
        using var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_oracle.GetConnectionString());
        await connection.ExecuteAsync("INSERT INTO Aircraft (TailNumber) VALUES ('SY-001')");
        var tail = await connection.QuerySingleAsync<string>("SELECT TailNumber FROM Aircraft");
        Assert.Equal("SY-001", tail);
    }
}
