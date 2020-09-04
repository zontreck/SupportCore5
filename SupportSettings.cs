using System;
using System.IO;
using Newtonsoft.Json;


namespace SupportCore5
{
    public sealed class SupportSettings
    {
        private static readonly object lck = new object();
        private static SupportSettings ins;

        static SupportSettings()
        {

        }

        public static SupportSettings Instance
        {
            get
            {
                if (ins != null) return ins;
                else
                {
                    lock (lck)
                    {
                        if (ins == null) ins = Load();

                        return ins;
                    }
                }
            }
        }

        private static readonly object filelock = new object();

        private static SupportSettings Load()
        {
            lock (filelock)
            {
                if (File.Exists("SupportCore5.json")) File.Move("SupportCore5.json", Path.Combine("BotData", "SupportCore5.json"));
                if (File.Exists("BotData/SupportCore5.json"))
                    return JsonConvert.DeserializeObject<SupportSettings>(File.ReadAllText("BotData/SupportCore5.json"));
                else return new SupportSettings();
            }
        }

        public void Save()
        {
            lock (filelock)
            {
                File.WriteAllText("BotData/SupportCore5.json", JsonConvert.SerializeObject(this, Formatting.Indented));
            }
        }

        public static void SaveMemory()
        {
            lock (filelock)
            {
                File.WriteAllText("BotData/SupportCore5.json", JsonConvert.SerializeObject(ins, Formatting.Indented));
            }
        }


        public string AutoReply { get; set; } = "Not configured";
    }
}
