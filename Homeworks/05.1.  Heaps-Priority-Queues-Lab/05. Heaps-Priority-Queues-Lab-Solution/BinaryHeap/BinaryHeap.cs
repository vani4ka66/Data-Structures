using System;
using System.Collections.Generic;
using System.ComponentModel;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get { return this.heap.Count; }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    public T Peek()
    {
        if (this.heap.Count <= 0)
        {
            throw new InvalidOperationException();
        }
        return this.heap[0];
    }

    public T Pull()
    {
        if (this.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        T item = this.heap[0];
        
        this.Swap(0, this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);
        this.HeapifyDown(0);
        
        return item;
    }

    private void HeapifyUp(int index)
    {
        int parent = (index - 1) / 2;
        while (index > 0 && IsGreater(parent, index))  
        {
            this.Swap(index, parent);
            index = parent;
            parent = (index - 1)/2;
        }
    }

    private bool IsGreater(int parentIndex, int index)
    {
        return this.heap[index].CompareTo(this.heap[parentIndex]) > 0;
    }

    private void Swap(int index, int parentIndex)
    {
        T temp = this.heap[index];
        this.heap[index] = this.heap[parentIndex];
        this.heap[parentIndex] = temp;
    }

    private void HeapifyDown(int index)
    {
        while (index < this.heap.Count / 2)
        {
            int child = 2 * index + 1; //Left(index)
            if (HasChild(child + 1) && IsGreater(child, child + 1))
            {
                child++;
            }

            if (IsGreater(child, index))
            {
                break;
            }

            this.Swap(index, child);
            index = child;
        }
    }

    private bool HasChild(int index)
    {
        if (index < this.Count)
        {
            return true;
        }
        return false;
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }

    private int Left(int index)
    {
        return 2 * index + 1;
    }

    private int Right(int index)
    {
        return 2 * index + 2;
    }
}
