using Microsoft.Data.Sqlite;

namespace MesenDotNetFrontend.Infrastructure.Persistence;

public static class SaveStateRepository
{
    public static long Insert(string romName, string? romHash, string? name, byte[] stateData, byte[]? screenshotData)
    {
        using var connection = new SqliteConnection(AppDatabase.ConnectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
            INSERT INTO SaveStates
            (
                RomName,
                RomHash,
                Name,
                StateData,
                ScreenshotData,
                CreatedAt
            )
            VALUES
            (
                $romName,
                $romHash,
                $name,
                $stateData,
                $screenshotData,
                $createdAt
            );

            SELECT last_insert_rowid();
            """;

        command.Parameters.AddWithValue("$romName", romName);

        command.Parameters.AddWithValue("$romHash", (object?)romHash ?? DBNull.Value);

        command.Parameters.AddWithValue("$name", (object?)name ?? DBNull.Value);

        command.Parameters.AddWithValue("$stateData", stateData);

        command.Parameters.AddWithValue("$screenshotData", (object?)screenshotData ?? DBNull.Value);

        command.Parameters.AddWithValue("$createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        var result = command.ExecuteScalar();

        return Convert.ToInt64(result);
    }

    public static byte[]? GetLatestQuickSave(string romName)
    {
        using var connection = new SqliteConnection(AppDatabase.ConnectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
        SELECT StateData
        FROM SaveStates
        WHERE RomName = $romName
          AND Name = 'Quick Save'
        ORDER BY Id DESC
        LIMIT 1;
        """;

        command.Parameters.AddWithValue("$romName", romName);

        var result = command.ExecuteScalar();

        if (result is not byte[] data)
        {
            return null;
        }

        return data;
    }

    public static List<SaveStateListItem> GetByRom(string romName)
    {
        var result = new List<SaveStateListItem>();

        using var connection = new SqliteConnection(AppDatabase.ConnectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
        SELECT
            Id,
            RomName,
            Name,
            ScreenshotData,
            CreatedAt
        FROM SaveStates
        WHERE RomName = $romName
        ORDER BY CreatedAt DESC;
        """;

        command.Parameters.AddWithValue("$romName", romName);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            result.Add(new SaveStateListItem
            {
                Id = reader.GetInt64(0),

                RomName = reader.GetString(1),

                Name = reader.IsDBNull(2) ? null : reader.GetString(2),

                ScreenshotData = reader.IsDBNull(3) ? null : (byte[])reader[3],

                CreatedAt = DateTime.Parse(reader.GetString(4))
            });
        }

        return result;
    }

    public static byte[]? GetStateData(long id)
    {
        using var connection = new SqliteConnection(AppDatabase.ConnectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
        SELECT StateData
        FROM SaveStates
        WHERE Id = $id;
        """;

        command.Parameters.AddWithValue("$id", id);

        return command.ExecuteScalar() as byte[];
    }

    public static void Delete(long id)
    {
        using var connection = new SqliteConnection(AppDatabase.ConnectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
        DELETE FROM SaveStates
        WHERE Id = $id;
        """;

        command.Parameters.AddWithValue("$id", id);

        command.ExecuteNonQuery();
    }
}