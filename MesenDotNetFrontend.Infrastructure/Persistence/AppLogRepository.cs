using Microsoft.Data.Sqlite;

namespace MesenDotNetFrontend.Infrastructure.Persistence;

public static class AppLogRepository
{
    public static void Write(string? rom = null, string? category = null, string? description = null)
    {
        using var connection = new SqliteConnection(AppDatabase.ConnectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
            INSERT INTO Log
            (
                createdAt,
                Rom,
                Category,
                Description
            )
            VALUES
            (
                $createdAt,
                $rom,
                $category,
                $description
            );
            """;

        command.Parameters.AddWithValue("$createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        command.Parameters.AddWithValue("$rom", (object?)rom ?? DBNull.Value);

        command.Parameters.AddWithValue("$category", (object?)category ?? DBNull.Value);

        command.Parameters.AddWithValue("$description", (object?)description ?? DBNull.Value);

        command.ExecuteNonQuery();
    }
}