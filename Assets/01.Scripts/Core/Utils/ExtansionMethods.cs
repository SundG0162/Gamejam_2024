using System;
using UnityEngine;

namespace BSM.Utils
{
    public static class ExtansionMethods
    {
        /// <summary>
        /// Rotate the array by count.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TArray">Target Array</param>
        /// <param name="count">Count</param>
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

        /// <summary>
        /// Push the array by count.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TArray">Target Array</param>
        /// <param name="count">Count</param>
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

        /// <summary>
        /// Puul the array by count.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TArray">Target Array</param>
        /// <param name="count">Count</param>
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

        /// <summary>
        /// Rotate the array by count. Use InPlace Algorithm.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TArray">Target Array</param>
        /// <param name="count">Count</param>
        public static void RotateArrayInPlace<T>(this T[] TArray, int count)
        {
            if(TArray == null || TArray.Length == 0) return;

            count %= TArray.Length;
            if (count == 0) return;

            if (count > 0)
                PushArrayInPlace(TArray, count);
            else
                PullArrayInPlace(TArray, -count);
        }

        public static void PushArrayInPlace<T>(this T[] TArray, int count)
        {
            int length = TArray.Length;
            count %= length;
            if (count == 0) return;

            TArray.Reverse(0, length - 1);
            TArray.Reverse(0, count - 1);
            TArray.Reverse(count, length - 1);
        }

        public static void PullArrayInPlace<T>(this T[] TArray, int count)
        {
            int length = TArray.Length;
            count %= length;
            if (count == 0) return;

            TArray.Reverse(0, count - 1);
            TArray.Reverse(count, length - 1);
            TArray.Reverse(0, length - 1);
        }

        private static void Reverse<T>(this T[] TArray, int start, int end)
        {
            while (start < end)
            {
                T temp = TArray[start];
                TArray[start] = TArray[end];
                TArray[end] = temp;
                start++;
                end--;
            }
        }
    }
}
