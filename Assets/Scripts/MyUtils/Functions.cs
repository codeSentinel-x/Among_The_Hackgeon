using UnityEngine;

namespace MyUtils.Functions {
    public static class MyRandom {
        public static T GetFromArray<T>(T[] a) {
            return a[Random.Range(0, a.Length)];
        }
    }
    public static class InventoryF {
        public static void Split(int value, out int value1, out int value2) {
            value1 = value / 2;
            value2 = value - value1;

        }
    }
}
