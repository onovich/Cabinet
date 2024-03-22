using System;
using System.Collections.Generic;

namespace MortiseFrame.Cabinet {

    public static class MathHelper {

        class DefaultComparer<T> : Comparer<T> where T : IComparable<T> {
            public override int Compare(T x, T y) {
                return x.CompareTo(y);
            }
        }

        public static void QuickSort<T>(T[] src, int offset, int count, Comparer<T> comparer = null) where T : IComparable<T> {
            if (count <= 1) return;

            comparer ??= new DefaultComparer<T>();

            int pivotIndex = Partition(src, offset, count, comparer);
            QuickSort(src, offset, pivotIndex - offset, comparer);
            QuickSort(src, pivotIndex + 1, offset + count - pivotIndex - 1, comparer);
        }

        static int Partition<T>(T[] arr, int offset, int count, Comparer<T> comparer) where T : IComparable<T> {
            T pivot = arr[offset];
            int i = offset, j = offset + count - 1;

            while (i < j) {
                while (i < offset + count - 1 && comparer.Compare(arr[i], pivot) <= 0) i++;
                while (j > offset && comparer.Compare(arr[j], pivot) > 0) j--;

                if (i < j) {
                    Swap(arr, i, j);
                }
            }

            Swap(arr, offset, j);
            return j;
        }

        static void Swap<T>(T[] arr, int i, int j) where T : IComparable<T> {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

    }

}