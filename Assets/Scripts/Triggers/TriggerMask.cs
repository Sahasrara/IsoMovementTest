using System;

namespace Techno
{
    [Flags]
    public enum TriggerMask
    {
        None = 0,
        Player = 1 << 0,
        Testing1 = 1 << 1,
        Testing2 = 1 << 2,
        Testing3 = 1 << 3,
        All = ~0,
    }
}
