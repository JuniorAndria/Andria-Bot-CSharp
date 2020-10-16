using DatabaseManager.Models;
using System;
using System.Data.SQLite;
using System.Reflection;
using System.Threading.Tasks;

namespace DatabaseManager.Controllers
{
    public static class GuildTextController
    {
        public static async Task Select(this GuildText guildText)
        {
            try
            {
                using (SQLiteConnection con = ContextController.getDb())
                {
                    try
                    {
                        con.Open();
                        if(con.State != System.Data.ConnectionState.Open)
                        {
                            throw new SQLiteException("Não foi possível abrir conexão com o banco!");
                        }
                    }
                    catch(SQLiteException ex)
                    {
                        throw ex;
                    }
                    SQLiteCommand cmd = con.CreateCommand();
                    SQLiteParameter parameter = new SQLiteParameter();
                    parameter.ParameterName = "@GuildID";
                    parameter.DbType = System.Data.DbType.String;
                    parameter.Value = guildText.GuildID;
                    cmd.Parameters.Add(parameter);
                    cmd.CommandText = "SELECT ChannelID FROM GuildTextChannel WHERE Guild = @GuildID";
                    try
                    {
                        guildText.TextChannelID = (string)await cmd.ExecuteScalarAsync();
                    }
                    catch(SQLiteException ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw ex;
                    }
                    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exceção no método {MethodBase.GetCurrentMethod().Name} com a mensagem: {ex.Message}");
                throw ex;
            }
            finally
            {
            }
        }
        public static async Task Insert(this GuildText guildText)
        {
            try
            {
                using (SQLiteConnection con = ContextController.getDb())
                {
                    try
                    {
                        con.Open();
                        if (con.State != System.Data.ConnectionState.Open)
                        {
                            throw new SQLiteException("Não foi possível abrir conexão com o banco!");
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        throw ex;
                    }
                    SQLiteCommand cmd = con.CreateCommand();
                    SQLiteParameter parameter = new SQLiteParameter();
                    parameter.ParameterName = "@GuildID";
                    parameter.DbType = System.Data.DbType.String;
                    parameter.Value = guildText.GuildID;
                    cmd.Parameters.Add(parameter);
                    parameter = new SQLiteParameter()
                    {
                        ParameterName = "@ChannelID",
                        DbType = System.Data.DbType.String,
                        Value = guildText.TextChannelID
                    };
                    cmd.Parameters.Add(parameter);
                    cmd.CommandText = "INSERT INTO GuildTextChannel('Guild', 'ChannelID') VALUES (@GuildID, @ChannelID)";
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exceção no método {MethodBase.GetCurrentMethod().Name} com a mensagem: {ex.Message}");
                throw ex;
            }
        }
        public static async Task Update(this GuildText guildText)
        {
            try
            {
                using (SQLiteConnection con = ContextController.getDb())
                {
                    try
                    {
                        con.Open();
                        if (con.State != System.Data.ConnectionState.Open)
                        {
                            throw new SQLiteException("Não foi possível abrir conexão com o banco!");
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        throw ex;
                    }
                    SQLiteCommand cmd = con.CreateCommand();
                    SQLiteParameter parameter = new SQLiteParameter();
                    parameter.ParameterName = "@GuildID";
                    parameter.DbType = System.Data.DbType.UInt64;
                    parameter.Value = guildText.GuildID;
                    cmd.Parameters.Add(parameter);
                    parameter = new SQLiteParameter()
                    {
                        ParameterName = "@ChannelID",
                        DbType = System.Data.DbType.UInt64,
                        Value = guildText.TextChannelID
                    };
                    cmd.Parameters.Add(parameter);
                    cmd.CommandText = "UPDATE GuildTextChannel SET ChannelID = @ChannelID WHERE Guild = @GuildID";
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exceção no método {MethodBase.GetCurrentMethod().Name} com a mensagem: {ex.Message}");
            }
        }
    }
}
