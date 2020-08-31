﻿using System;
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

        private static SupportSettings Load()
        {
            lock (lck)
            {
                return JsonConvert.DeserializeObject<SupportSettings>(File.ReadAllText("SupportCore5.json"));
            }
        }

        public void Save()
        {
            lock (lck)
            {
                File.WriteAllText("SupportCore5.json", JsonConvert.SerializeObject(this, Formatting.Indented));
            }
        }

        public static void SaveMemory()
        {
            lock (lck)
            {
                File.WriteAllText("SupportCore5.json", JsonConvert.SerializeObject(ins, Formatting.Indented));
            }
        }


        public string AutoReply { get; set; } = "Not configured";
    }
}
