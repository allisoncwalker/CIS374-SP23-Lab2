using System;
using System.Linq;
using System.Reflection;

namespace Lab2
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private T[] array;
        private const int initialSize = 8;

        public int Count { get; private set; }

        public int Capacity => array.Length;

        public bool IsEmpty => Count == 0;


        public MinHeap(T[] initialArray = null)
        {
            array = new T[initialSize];

            if (initialArray == null)
            {
                return;
            }

            foreach (var item in initialArray)
            {
                Add(item);
            }

        }

        /// <summary>
        /// Returns the min item but does NOT remove it.
        /// Time complexity: O(?).
        /// </summary>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            return array[0];
        }

        /// <summary>
        /// Adds given item to the heap.
        /// Time complexity: O(?).
        /// </summary>
        public void Add(T item)
        {
            int nextEmptyIndex = Count;

            array[nextEmptyIndex] = item;

            TrickleUp(nextEmptyIndex);

            Count++;

            // resize if full
            if (Count == Capacity)
            {
                DoubleArrayCapacity();
            }

        }

        public T Extract()
        {
     
                return ExtractMin();
        }

        /// <summary>
        /// Removes and returns the max item in the min-heap.
        /// Time complexity: O( N ).
        /// </summary>
        public T ExtractMax()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T max = array[0];

            // swap root (first) and last element
            Swap(0, Count - 1);

            // "remove" last
            Count--;

            // trickle down from root (first)
            TrickleDown(0);

            return max;

        }

        /// <summary>
        /// Removes and returns the min item in the min-heap.
        /// Time ctexity: O( log(n) ).
        /// </summary>
        public T ExtractMin()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T min = array[0];

            // swap root (first) and last element
            Swap(0, Count - 1);

            // "remove" last
            Count--;

            // trickle down from root (first)
            TrickleDown(0);

            return min;
        }

        /// <summary>
        /// Returns true if the heap contains the given value; otherwise false.
        /// Time complexity: O( N ).
        /// </summary>
        public bool Contains(T value)
        {
            // linear search

            foreach (var item in array)
            {
                if (item.CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;

        }

        // Time Complexity: O( log(n) )
        private void TrickleUp(int index)
        {
            int parentIndex, temp;
            if (index != 0)
            {
                parentIndex = Parent(index);
                if (parentIndex > index)
                {
                    temp = parentIndex;
                    parentIndex = index;
                    index = temp;
                    TrickleUp(0);
                }
            }

        }

        // TODO
        // Time Complexity: O( log(n) )
        private void TrickleDown(int index)
        {
            //int LeftChildIndex = int LeftChild;
            //int RightChildIndex = int RightChild;

            //if (RightChildIndex >= Count)
            //{
            //    if (LeftChildIndex >= Count)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        Mindex = LeftChildIndex;
            //    }
            //}
            //else
            //{
            //    if (LeftChildIndex <= RightChildIndex)
            //    {
            //        Mindex = LeftChildIndex;
            //    }
            //    else
            //    {
            //        Mindex = LeftChildIndex;
            //    }
            //}
            //if (index > Mindex)
            //{
            //    tmp = Mindex;
            //    Mindex = index;
            //    index = tmp;
            //    TrickleDown(Mindex);
            //}

        }

        /// <summary>
        /// Gives the position of a node's parent, the node's position in the heap.
        /// </summary>
        private static int Parent(int position)
        {
            int parentPos = (position -1) / 2;

            return parentPos;
        }

        /// <summary>
        /// Returns the position of a node's left child, given the node's position.
        /// </summary>
        private static int LeftChild(int position)
        {
            int left = 2 * position + 2;
            return left;
        }

        /// <summary>
        /// Returns the position of a node's right child, given the node's position.
        /// </summary>
        private static int RightChild(int position)
        {
            int right = 2 * position + 2 ;
            return right;
        }

        private void Swap(int index1, int index2)
        {
            var temp = array[index1];

            array[index1] = array[index2];
            array[index2] = temp;
        }

        private void DoubleArrayCapacity()
        {
            Array.Resize(ref array, array.Length * 2);
        }


    }
}


