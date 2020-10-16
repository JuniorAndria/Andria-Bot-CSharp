using System;

namespace DatabaseManager.Models
{
    public class GuildText
    {
        public string GuildID { get; set; }
        public string TextChannelID { get; set; }

        public GuildText() { }
        public GuildText(string guildID)
        {
            this.GuildID = guildID;
        }
    }
}
