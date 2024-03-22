using System;
using System.Collections.Generic;

namespace MortiseFrame.Cabinet {
    internal static class MathHelper {
        internal class DefaultComparer<T> : Comparer<T> where T : IComparable<T> {
            public override int Compare(T x, T y) {
                return x.CompareTo(y);
            }
        }

        internal static void QuickSort<T>(List<T> src, Comparer<T> comparer = null) where T : IComparable<T> {
            comparer ??= new DefaultComparer<T>();
            QuickSortInternal(src, 0, src.Count - 1, comparer);
        }

        static void QuickSortInternal<T>(List<T> src, int left, int right, Comparer<T> comparer) where T : IComparable<T> {
            if (left < right) {
                int pivotIndex = Partition(src, left, right, comparer);
                QuickSortInternal(src, left, pivotIndex - 1, comparer);
                QuickSortInternal(src, pivotIndex + 1, right, comparer);
            }
        }

        static int Partition<T>(List<T> arr, int left, int right, Comparer<T> comparer) where T : IComparable<T> {
            T pivot = arr[left];
            int i = left - 1;
            int j = right + 1;

            while (true) {
                do {
                    i++;
                } while (comparer.Compare(arr[i], pivot) < 0);

                do {
                    j--;
                } while (comparer.Compare(arr[j], pivot) > 0);

                if (i >= j) return j;

                Swap(arr, i, j);
            }
        }

        static void Swap<T>(List<T> src, int i, int j) {
            T temp = src[i];
            src[i] = src[j];
            src[j] = temp;
        }
    }

}