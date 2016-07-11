using LiveSplit.ComponentUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.HenkMedals.Util
{
    class BonusLevel : RegularLevel
    {
        public BonusLevel(String name, TimeSpan rainbow, int medals_needed, DeepPointer reference) : base (name, rainbow, medals_needed, reference)
        {
        }

        public new bool IsCompleted(Process p)
        {
            return MemoryReference.Deref<int>(p) > 0;
        }
    }
}
