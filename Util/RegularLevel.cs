using LiveSplit.ComponentUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.HenkMedals.Util
{
    class RegularLevel
    {
        public String LevelName { get; set; }
        public TimeSpan RainbowTime { get; set; }
        public int MedalsNeeded { get; set; }
        public DeepPointer MemoryReference { get; set; }

        public RegularLevel(String name, TimeSpan rainbow, int medals_needed, DeepPointer reference)
        {
            LevelName = name;
            RainbowTime = rainbow;
            MedalsNeeded = medals_needed;
            MemoryReference = reference;
        }

        public bool IsCompleted(Process p)
        {
            return MemoryReference.Deref<int>(p) >= MedalsNeeded;
        }
    }
}
