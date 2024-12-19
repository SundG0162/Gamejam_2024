using System;
using UnityEngine;

namespace BSM.Utils
{
    public static class ExtansionMethods
    {
        public static void RotateArray<T>(this T[] TArray, int count)
        {
            if (TArray == null || TArray.Length == 0) return;

            count %= TArray.Length;
            if (count == 0) return;

            if (count > 0)
                PushArray(TArray, count);
            else
                PullArray(TArray, -count);
        }

        public static void PushArray<T>(this T[] TArray, int count)
        {
            int length = TArray.Length;
            count %= length;

            if (count == 0) return;

            T[] temp = new T[count];
            Array.Copy(TArray, length - count, temp, 0, count);
            Array.Copy(TArray, 0, TArray, count, length - count);
            Array.Copy(temp, 0, TArray, 0, count);
        }

        public static void PullArray<T>(this T[] TArray, int count)
        {
            int length = TArray.Length;
            count %= length;

            if (count == 0) return;

            T[] temp = new T[count];
            Array.Copy(TArray, 0, temp, 0, count);
            Array.Copy(TArray, count, TArray, 0, length - count);
            Array.Copy(temp, 0, TArray, length - count, count);
        }
    }
}
