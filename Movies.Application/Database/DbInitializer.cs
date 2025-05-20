using Dapper;

namespace Movies.Application.Database;

public class DbInitializer(IDbConnectionFactory dbConnectionFactory)
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task InitializeAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync("""
                                        CREATE TABLE IF NOT EXISTS movies (
                                        id UUID PRIMARY KEY,
                                        slug TEXT NOT NULL,
                                        title TEXT NOT NULL,
                                        autorelease INTEGER NOT NULL);
                                      """);

        await connection.ExecuteAsync("""
                                        create unique index concurrently if not exists movies_slug_idx
                                        on movies
                                        using btree(slug);
                                      """);

        await connection.ExecuteAsync("""
                                        create table if not exists genres (
                                            movieId UUID references movies (Id),
                                            name TEXT not null);
                                      """);
    }
}