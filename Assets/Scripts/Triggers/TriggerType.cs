using System;

namespace Techno
{
    [Flags]
    public enum TriggerType
    {
        None = 0,
        Exit = 1 << 0,
        Enter = 1 << 1,
        All = ~0,
    }
}
