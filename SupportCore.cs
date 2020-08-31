using System;
namespace SupportCore5
{
    public sealed class SupportCore
    {
        private static readonly object lck = new object();
        private static SupportCore ins;
        static SupportCore()
        {

        }

        public static SupportCore Singleton
        {
            get
            {
                if (ins != null) return ins;
                else
                {
                    lock (lck)
                    {
                        if (ins == null) ins = new SupportCore();

                        return ins;
                    }
                }
            }
        }


        public bool SupportActive = false;
    }
}
