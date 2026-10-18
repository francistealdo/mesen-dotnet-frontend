using Microsoft.Data.Sqlite;

namespace MesenDotNetFrontend.Infrastructure.Persistence;

public static class InputSettingsRepository
{
    public static Dictionary<string, string> GetAll()
    {
        var result = new Dictionary<string, string>();

        using var connection = new SqliteConnection(AppDatabase.ConnectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
            SELECT Action, KeyName
            FROM InputSettings;
            """;

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            result[
                reader.GetString(0)] =
                reader.GetString(1);
        }

        return result;
    }

    public static void SaveAll(Dictionary<string, string> settings)
    {
        using var connection = new SqliteConnection(AppDatabase.ConnectionString);

        connection.Open();

        using var transaction = connection.BeginTransaction();

        foreach (var setting in settings)
        {
            using var command = connection.CreateCommand();

            command.Transaction = transaction;

            command.CommandText =
                """
                INSERT INTO InputSettings
                (
                    Action,
                    KeyName
                )
                VALUES
                (
                    $action,
                    $keyName
                )
                ON CONFLICT(Action)
                DO UPDATE SET
                    KeyName = excluded.KeyName;
                """;

            command.Parameters.AddWithValue("$action", setting.Key);

            command.Parameters.AddWithValue("$keyName", setting.Value);

            command.ExecuteNonQuery();
        }

        transaction.Commit();
    }
}