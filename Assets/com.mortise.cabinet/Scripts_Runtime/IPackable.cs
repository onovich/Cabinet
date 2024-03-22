using System;

namespace MortiseFrame.Cabinet {

    public interface IPackable : IComparable<IPackable> {

        int Type { get; }
        int TypeID { get; }
        int Count { get; set; }
        int MaxCount { get; }
        int Weight { get; }
        int SlotSize { get; }
        int Tags { get; }
        DateTime LastUsedTime { get; set; }
        DateTime AddedToInventoryTime { get; }

        IPackable Clone();

    }

}