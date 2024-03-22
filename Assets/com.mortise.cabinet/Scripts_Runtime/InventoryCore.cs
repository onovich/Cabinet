using System;
using System.Collections.Generic;

namespace MortiseFrame.Cabinet {

    public class InventoryCore {

        InventoryContext ctx;
        public InventoryCore(int capacity) {
            ctx = new InventoryContext(capacity);
        }

        // Capital
        public void IncreaseCapacity(int increaseAmount) {
            ctx.IncreaseCapacity(increaseAmount);
        }

        // Add
        public int AddWithoutIndex(IPackable src) {
            return ctx.AddWithoutIndex(src);
        }

        // Reduce
        public void ReduceByTypeID(int typeID, int count, Action<int> onRemove) {
            ctx.ReduceByTypeID(typeID, count, onRemove);
        }

        public void ReduceByIndex(int index, int count, Action<int> onRemove) {
            ctx.ReduceByIndex(index, count, onRemove);
        }

        // Remove
        public void Remove(int index) {
            ctx.Remove(index);
        }

        // Exchange
        public void ExchangeIndex(int from, int to) {
            ctx.ExchangeIndex(from, to);
        }

        // Count
        public bool HasEnough(int typeID, int count) {
            return ctx.HasEnough(typeID, count);
        }

        // TryGet
        public bool TryGet(int index, out IPackable treasure) {
            return ctx.TryGet(index, out treasure);
        }

        // ForEach
        public void ForEach(Action<int /*index*/, IPackable> action) {
            ctx.ForEach(action);
        }

        // Filter
        public int FilterByTag(int tag, List<IPackable> list) {
            return ctx.FilterByTag(tag, list);
        }

        public int FilterByType(int type, List<IPackable> list) {
            return ctx.FilterByTag(type, list);
        }

        // Sort
        public List<IPackable> SortByType(int type) {
            return ctx.SortByType(type);
        }

        // Clear
        public void Clear() {
            ctx.Clear();
        }

    }

}