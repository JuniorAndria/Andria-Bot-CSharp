using DatabaseManager.Models;
using System;
using System.Data.SQLite;
using System.Reflection;
using System.Threading.Tasks;

namespace DatabaseManager.Controllers
{
    public static class DisabledChannelsController
    {
        
        public static async Task Select(this DisabledChannels disabled)
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
                    parameter.Value = disabled.GuildID;
                    cmd.Parameters.Add(parameter);
                    parameter = new SQLiteParameter();
                    parameter.ParameterName = "@ChannelID";
                    parameter.DbType = System.Data.DbType.String;
                    parameter.Value = disabled.TextChannelID;
                    cmd.Parameters.Add(parameter);
                    cmd.CommandText = "SELECT COUNT(1)Existe FROM DisabledChannels WHERE guildid = @GuildID AND ChannelID = @ChannelID";
                    disabled.isDisabled = (Int64)await cmd.ExecuteScalarAsync() > 0 ? true : false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exceção no método {MethodBase.GetCurrentMethod().Name} com a mensagem: {ex.Message}");
            }
            finally
            {
            }
        }
        public static async Task Insert(this DisabledChannels disabled)
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
                    parameter.Value = disabled.GuildID;
                    cmd.Parameters.Add(parameter);
                    parameter = new SQLiteParameter();
                    parameter.ParameterName = "@ChannelID";
                    parameter.DbType = System.Data.DbType.String;
                    parameter.Value = disabled.TextChannelID;
                    cmd.Parameters.Add(parameter);
                    cmd.CommandText = "INSERT INTO DisabledChannels('GuildID', 'ChannelID') VALUES (@GuildID, @ChannelID)";
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exceção no método {MethodBase.GetCurrentMethod().Name} com a mensagem: {ex.Message}");
            }
        }
        public static async Task Delete(this DisabledChannels disabled)
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
                    parameter.Value = disabled.GuildID;
                    cmd.Parameters.Add(parameter);
                    parameter = new SQLiteParameter();
                    parameter.ParameterName = "@ChannelID";
                    parameter.DbType = System.Data.DbType.String;
                    parameter.Value = disabled.TextChannelID;
                    cmd.Parameters.Add(parameter);
                    cmd.CommandText = "DELETE FROM DisabledChannels WHERE guildid = @GuildID AND ChannelID = @ChannelID";
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exceção no método {MethodBase.GetCurrentMethod().Name} com a mensagem: {ex.Message}");
                throw ex;
            }
        }
    }
}
