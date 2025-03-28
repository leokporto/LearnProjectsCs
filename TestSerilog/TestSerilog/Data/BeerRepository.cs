using System.Data;
using Dapper;
using Npgsql;
using TestSerilog.Contracts;
using TestSerilog.Entity;

namespace TestSerilog.Data;

public class BeerRepository : IBeerRepository
{
    private readonly string _connectionString;

    public BeerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Beer> List()
    {
        IEnumerable<Beer> beers = new List<Beer>();

        using (IDbConnection conn = new NpgsqlConnection(_connectionString))
        {
            conn.Open();

            string sql = "SELECT * FROM \"Beers\" Limit 100";
            beers = conn.Query<Beer>(sql);
        }

        return beers;
    }
}