using System;
using Bot;
using Bot.NonCommands;
using OpenMetaverse;
using Bot.CommandSystem;

namespace SupportCore5
{
    public class Support : BaseCommands, nCMD
    {
        [NotCommand(SourceType = Destinations.DEST_AGENT)]
        public void handle(string text, UUID User, string agentName, Destinations src, UUID originator)
        {
            if (SupportSettings.Instance.IgnoreAuthorizedUsers)
            {
                if (MainConfiguration.Instance.BotAdmins.ContainsKey(User)) return;
            }
            if(agentName == "Object")
            {
                return;
            } else
            {
                if (!SupportCore.Singleton.SupportActive)
                {
                    // Read the auto response from the auto reply text doc

                    MHE(src, originator, SupportSettings.Instance.AutoReply);
                }
            }
        }


        [CommandGroup("setsupportmsg", 5, "setsupportmsg [array:string] - Sets the support message (auto reply when support not active)", Bot.Destinations.DEST_AGENT | Bot.Destinations.DEST_LOCAL | Bot.Destinations.DEST_GROUP)]
        public void setSupMsg(UUID client, int level, string[] additionalArgs,
                                Destinations source,
                                UUID agentKey, string agentName)
        {
            string TheString = "";
            foreach (string s in additionalArgs)
            {
                TheString += s + " ";
            }

            if (TheString.EndsWith(' ')) TheString.TrimEnd(' ');

            SupportSettings.Instance.AutoReply = TheString;
            MHE(source, client, "Auto reply set");
            SupportSettings.SaveMemory();
        }



        [CommandGroup("ignoreAuthedUsers", 5, "ignoreauthedusers - Ignores all users that are authorized on the bot, and will not send them the auto response", Bot.Destinations.DEST_AGENT | Bot.Destinations.DEST_LOCAL | Bot.Destinations.DEST_GROUP)]
        public void ignoreAuthedUsers(UUID client, int level, string[] additionalArgs,
                                Destinations source,
                                UUID agentKey, string agentName)
        {
            SupportSettings.Instance.IgnoreAuthorizedUsers = true;
            MHE(source, client, "Ignoring authorized users!");
            SupportSettings.SaveMemory();
        }


        [CommandGroup("unignoreAuthedUsers", 5, "unignoreauthedusers - UnIgnores all users that are authorized on the bot", Bot.Destinations.DEST_AGENT | Bot.Destinations.DEST_LOCAL | Bot.Destinations.DEST_GROUP)]
        public void unignoreAuthedUsers(UUID client, int level, string[] additionalArgs,
                                Destinations source,
                                UUID agentKey, string agentName)
        {
            SupportSettings.Instance.IgnoreAuthorizedUsers = false;
            MHE(source, client, "UnIgnoring authorized users!");
            SupportSettings.SaveMemory();
        }
    }
}
