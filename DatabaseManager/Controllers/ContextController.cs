using System.Data.SQLite;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DatabaseManager.Controllers
{
    public class ContextController
    {
        private static string getArquive()
        {
            string json = string.Empty;
            using (var fs = File.OpenRead("./config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();
            ConfigJson configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            if (string.IsNullOrEmpty(configJson.ArquivoBanco))
                return Path.Combine(Directory.GetCurrentDirectory(), "db.sqlite");
            return configJson.ArquivoBanco;
        }
        private static SQLiteConnection DbConnection()
        {
            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder
            {
                DataSource = getArquive()
            };
            return new SQLiteConnection(connectionStringBuilder.ConnectionString);
        }
        public static SQLiteConnection getDb()
        {
            return DbConnection();
        }
        private static void CreateDataBaseSQLite()
        {
            try
            {
                string arquive = getArquive();
                if (!Directory.Exists(Path.GetDirectoryName(arquive)))
                    Directory.CreateDirectory(Path.GetDirectoryName(arquive));
                if (File.Exists(arquive))
                    return;
                SQLiteConnection.CreateFile(arquive);
            }
            catch
            {
                throw;
            }
        }
        private static void InitializeTables()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.Connection.Open();
                    cmd.CommandText = @"
                                        CREATE TABLE IF NOT EXISTS 'GuildTextChannel' (
	                                        'ID'	INTEGER,
	                                        'Guild'	TEXT NOT NULL,
	                                        'ChannelID' TEXT NOT NULL,
	                                        PRIMARY KEY('ID' AUTOINCREMENT)
                                        );";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS 'DisabledChannels' (
                                        'ID'    INTEGER NOT NULL,
	                                    'GuildID'   TEXT NOT NULL,
	                                    'ChannelID' TEXT NOT NULL,
	                                    PRIMARY KEY('ID' AUTOINCREMENT)
                                    ); ";
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
        public static void Initialize()
        {
            CreateDataBaseSQLite();
            InitializeTables();
        }
    }
}
