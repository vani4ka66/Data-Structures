using System;
using System.Runtime.CompilerServices;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        for (int i = arr.Length/2; i >= 0; i--)
        {
            HeapifyDown(arr, i, arr.Length);
        }

        for (int i = arr.Length - 1; i > 0; i--)
        {
            Swap(0, i, arr);
            HeapifyDown(arr, 0, i);
        }
    }

    private static void HeapifyDown(T[] arr, int index, int length)
    {
        while (index < length/2)
        {
            int child = 2 * index + 1; //Left(index)
            if (child + 1 < length && IsGreater(arr, child + 1, child))
            {
                child++;
            }

            if (IsGreater(arr, index, child))
            {
                break;
            }

            Swap(child, index, arr);
            index = child;
        }
    }

    private static bool IsGreater(T[] arr, int a, int b)
    {
        return arr[a].CompareTo(arr[b]) > 0;
    }

    private static void Swap(int a, int b, T[] arr)
    {
        T temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }

    private static bool HasChild(int index, int length)
    {
        if (index < length)
        {
            return true;
        }
        return false;
    }
}
