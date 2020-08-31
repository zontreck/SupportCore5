using System;
using System.Collections.Generic;
using System.Text;
using Bot;
using OpenMetaverse;


namespace SupportCore5
{
    class SupportPlugin : IProgram
    {
        public string ProgramName
        {
            get
            {
                return "SupportCore5";
            }
        }

        public float ProgramVersion => 1.0f;

        public void getTick()
        {
        }

        public void LoadConfiguration()
        {
        }

        public void onIMEvent(object sender, InstantMessageEventArgs e)
        {
            BotSession.Instance.grid.Self.IM -= onIMEvent;
            MessageFactory.Post(Destinations.DEST_LOCAL, "Deregistered SupportCore5.SupportPlugin.onIMEvent", UUID.Zero);
        }

        public void passArguments(string data)
        {
        }

        public void run()
        {
            // initialize / initiate the settings and singletons
            SupportCore.Singleton.SupportActive = false;
            SupportSettings.Instance.Save(); // read then save, or init then save
            MessageFactory.Post(Destinations.DEST_LOCAL, "Support Core5 initialized", UUID.Zero);
        }
    }
}
