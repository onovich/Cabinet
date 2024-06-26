using System;
using System.Collections.Generic;
using UnityEngine;

namespace TenonKit.Cabinet {

    internal static class CommonExtension {

        internal static TValue GetOrAdd<TKey, TValue>(this SortedList<TKey, TValue> sortedList, TKey key, Func<TValue> valueFactory) {
            if (!sortedList.TryGetValue(key, out TValue value)) {
                value = valueFactory();
                sortedList.Add(key, value);
            }
            return value;
        }

        internal static TValue GetOrAdd<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory) {
            if (!dictionary.TryGetValue(key, out TValue value)) {
                value = valueFactory();
                dictionary.Add(key, value);
            }
            return value;
        }

        internal static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory) {
            if (!dictionary.TryGetValue(key, out TValue value)) {
                value = valueFactory();
                dictionary.Add(key, value);
            }
            return value;
        }

    }

}