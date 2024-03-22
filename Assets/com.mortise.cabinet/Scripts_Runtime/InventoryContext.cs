using System;
using System.Collections.Generic;
using MortiseFrame.Abacus;

namespace MortiseFrame.Cabinet {

    internal class InventoryContext {

        IPackable[] all;
        List<IPackable> tempList;
        int[] indexTempArray;
        Dictionary<int/*index*/, int/*tag*/> typeDict;

        internal int capacity;

        internal InventoryContext(int capacity) {
            this.capacity = capacity;
            all = new IPackable[capacity];
            tempList = new List<IPackable>(capacity);
            typeDict = new Dictionary<int, int>();
            indexTempArray = new int[capacity];
        }

        // Capacity
        internal void IncreaseCapacity(int increaseAmount) {
            if (increaseAmount <= 0) {
                CLog.Log("Increase Amount Must Be Positive.");
            }

            var newCapacity = capacity + increaseAmount;
            var newAll = new IPackable[newCapacity];
            for (int i = 0; i < capacity; i++) {
                newAll[i] = all[i];
            }

            all = newAll;
            capacity = newCapacity;

            tempList = new List<IPackable>(newCapacity);
            indexTempArray = new int[newCapacity];
        }

        // Add
        void PlaceInEmptySlot(ref IPackable src, int index) {
            var slotSize = src.SlotSize;
            if (slotSize <= 0 || src.Count <= 0) {
                return;
            }

            // 能放完
            if (src.Count <= src.SlotSize) {
                var remaining = 0;
                var dst = src.Clone();
                src.Count = remaining;
                all[index] = dst;
                return;
            } else {
                var remaining = src.Count - src.SlotSize;
                src.Count = remaining;
                var dst = src.Clone();
                dst.Count = src.SlotSize;
                all[index] = dst;
            }

            if (typeDict.ContainsKey(index)) {
                return;
            }
            typeDict.Add(index, src.Type);
        }

        void MergeWithExistingItems(IPackable src, int index) {
            var slotSize = src.SlotSize;
            if (slotSize <= 0 || src.Count <= 0) {
                return;
            }

            var exist = all[index];

            // 能放完
            if (exist.Count + src.Count <= src.SlotSize) {
                exist.Count += src.Count;
                src.Count = 0;
                return;
            }

            // 放不完
            var remaining = src.Count + exist.Count - src.SlotSize;
            src.Count = remaining;
            exist.Count = src.SlotSize;
        }

        internal int AddWithoutIndex(IPackable src) {
            if (src.SlotSize <= 0) {
                return src.Count;
            }

            for (int i = 0; i < capacity; i++) {
                if (src.Count <= 0) break;
                if (all[i] == null) {
                    PlaceInEmptySlot(ref src, i);
                    if (src.Count <= 0) break;
                }
                if (all[i].TypeID == src.TypeID) {
                    MergeWithExistingItems(src, i);
                    if (src.Count <= 0) break;
                }
            }

            if (src.Count > 0) {
                CLog.Log($"剩余的: {src.Count} {src.TypeID}");
            }
            return src.Count;
        }

        // Exchange
        internal void ExchangeIndex(int from, int to) {
            var temp = all[from];
            all[from] = all[to];
            all[to] = temp;
        }

        // Count
        internal bool HasEnough(int typeID, int count) {
            int total = 0;
            for (int i = 0; i < capacity; i++) {
                if (all[i] != null && all[i].TypeID == typeID) {
                    total += all[i].Count;
                }
            }
            return total >= count;
        }

        internal int Count(int index) {
            return all[index].Count;
        }

        // Reduce
        internal void ReduceByTypeID(int typeID, int count, Action<int> onRemove) {
            for (int i = 0; i < capacity; i++) {
                if (all[i] == null) continue;
                if (all[i].TypeID != typeID) continue;

                int reduction = FMath.Min(count, all[i].Count);
                all[i].Count -= reduction;
                count -= reduction;

                if (all[i].Count <= 0) {
                    all[i] = null;
                    onRemove?.Invoke(i);
                }

                if (count <= 0) break;
            }
        }

        internal void ReduceByIndex(int index, int count, Action<int> onRemove) {
            var treasure = all[index];
            treasure.Count -= count;
            if (treasure.Count <= 0) {
                all[index] = null;
                onRemove?.Invoke(index);
            }
        }

        // Remove
        internal void Remove(int index) {
            all[index] = null;
            if (!typeDict.ContainsKey(index)) {
                CLog.Error($"Remove Error: Index = {index} Item Is Not In TypeDict");
            }
            typeDict.Remove(index);
        }

        // TryGet
        internal bool TryGet(int index, out IPackable treasure) {
            treasure = all[index];
            return treasure != null;
        }

        // ForEach
        internal void ForEach(Action<int /*index*/, IPackable> action) {
            for (int i = 0; i < capacity; i++) {
                if (all[i] == null) continue;
                action(i, all[i]);
            }
        }

        // Filter
        internal int TryFilterByTag(int tag, List<IPackable> list) {
            tempList.Clear();
            int count = 0;
            for (int i = 0; i < capacity; i++) {
                var item = all[i];
                if (item.Tags != tag) {
                    continue;
                }
                tempList.Add(item);
                count++;
            }
            list = tempList;
            return count;
        }

        // Sort
        internal List<IPackable> SortByType(int type) {
            tempList.Clear();
            int count = 0;
            for (int i = 0; i < capacity; i++) {
                if (all[i] != null && all[i].TypeID == type) {
                    tempList.Add(all[i]);
                    count++;
                }
            }

            if (count <= 1) return tempList; // 少于两个元素,不需要排序

            QuickSortFunction.QuickSortList(tempList);
            return tempList;
        }

        // Clear
        internal void Clear() {
            Array.Clear(all, 0, capacity);
            Array.Clear(indexTempArray, 0, capacity);
            tempList.Clear();
        }

    }

}