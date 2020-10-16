using System;

namespace DatabaseManager.Models
{
    public class DisabledChannels
    {
        public int ID { get; private set; }
        public string GuildID { get; private set; }
        public string TextChannelID { get; private set; }
        public bool isDisabled { get; set; }
        public DisabledChannels() { }
        public DisabledChannels(string GuildID, string TextChannelID, int id = 0)
        {
            this.ID = id;
            this.GuildID = GuildID;
            this.TextChannelID = TextChannelID;
        }
    }
}
