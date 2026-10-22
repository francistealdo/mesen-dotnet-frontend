using Microsoft.Data.Sqlite;

namespace MesenDotNetFrontend.Infrastructure.Persistence;

public static class AppDatabase
{
    public static string DatabasePath { get; } =
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
            "MesenDotNetFrontend",
            "mesen-dotnet-frontend.db");

    public static string ConnectionString { get; } =
        new SqliteConnectionStringBuilder
        {
            DataSource = DatabasePath,
            Mode = SqliteOpenMode.ReadWriteCreate
        }
        .ToString();

    public static void Initialize()
    {
        var directory = Path.GetDirectoryName(DatabasePath);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using var connection = new SqliteConnection(ConnectionString);

        connection.Open();

        CreateLogTable(connection);
        CreateInputSettingsTable(connection);
        CreateSaveStatesTable(connection);
    }

    #region Tables

    private static void CreateLogTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS Log
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                CreatedAt TEXT NOT NULL,
                Rom TEXT NULL,
                Category TEXT NULL,
                Description TEXT NULL
            );
            """;

        command.ExecuteNonQuery();
    }

    private static void CreateInputSettingsTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS InputSettings
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Action TEXT NOT NULL UNIQUE,
                KeyName TEXT NOT NULL
            );
            """;

        command.ExecuteNonQuery();
    }

    private static void CreateSaveStatesTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS SaveStates
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                RomName TEXT NOT NULL,
                RomHash TEXT NULL,
                Name TEXT NULL,
                StateData BLOB NOT NULL,
                ScreenshotData BLOB NULL,
                CreatedAt TEXT NOT NULL
            );;
            """;

        command.ExecuteNonQuery();
    }

    #endregion
}

