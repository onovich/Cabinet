using System.Collections.Generic;

namespace TenonKit.Cabinet {

    class PackableComparer : Comparer<IPackable> {
        public override int Compare(IPackable x, IPackable y) {
            return x.Count.CompareTo(y.Count);
        }
    }

}