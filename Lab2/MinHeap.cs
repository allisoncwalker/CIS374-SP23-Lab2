using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

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

        //Finished
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

        //Finished
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

        /// Finished
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

            int maxIndex = (Count - 1) / 2 + 1;
            T max = array[maxIndex];
            for (int i = maxIndex + 1; i < Count; i++)
            {
                if (array[i].CompareTo(max) < 0)
                {
                    max = array[i];
                    maxIndex = i;
                }
            }


            // swap root (first) and last element
            Swap(maxIndex, Count - 1);

            // "remove" last
            Count--;

            TrickleUp(maxIndex);

            return max;

        }

        /// Finished
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

        //Finished
        /// <summary>
        /// Returns true if the heap contains the given value; otherwise false.
        /// Time complexity: O( N ).
        /// </summary>
        public bool Contains(T value)
        {
            // linear search

            for (int i = 0; i < Count; i++)
            {
                if (array[i].CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;

        }

        // Finished
        /// <summary>
        /// Updates the first element with the given value from the heap.
        /// Time complexity: O( ? )
        /// </summary>
        public void Update(T oldValue, T newValue)
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            if (!Contains(oldValue))
            {
                throw new Exception("Not in Heap");
            }
                //int index = (Count - 1) / 2 + 1;
                int index = 0;
                //T min = array[index];
                for (int i = 0; i < Count; i++)
                {
                    if (array[i].CompareTo(oldValue) == 0)
                    {
                        array[i] = newValue;
                        index = i;
                    }
                }
            if (array[index].CompareTo(array[Parent(index)]) < 0)
            {

                //Swap(index, Count - 1);
                //Count--;
                TrickleUp(index);
                //return min;
            }
            else
            {
                TrickleDown(index);
            }

        }

        /// Finished
        /// <summary>
        /// Removes the first element with the given value from the heap.
        /// Time complexity: O( ? )
        /// </summary>
        public void Remove(T value)
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            int index = 0;
            //T min = array[mIndex];
            for (int i = 0; i < Count; i++)
            {
                if (array[i].CompareTo(value) == 0)
                {
                    //min = array[i];
                    index = i;
                }
            }

            Swap(index, Count - 1);

            Count--;

            TrickleUp(0);

            //return MinHeap;


        }
        //Finished
        // Time Complexity: O( log(n) )
        private void TrickleUp(int index)
        {
            //parentIndex = Parent(index);
            if (array[index].CompareTo(array[Parent(index)]) < 0)
            {
                Swap(index, Parent(index));
                TrickleUp(Parent(index));

            }
        }

        // Finished
        // Time Complexity: O( log(n) )
        private void TrickleDown(int index)
        {
            if (LeftChild(index) >= Count)
            {
                return;
            }
            if (array[index].CompareTo(array[LeftChild(index)]) > 0 && LeftChild(index) < Count)
            {
                if (array[LeftChild(index)].CompareTo(array[RightChild(index)]) < 0 || RightChild(index) >= Count)
                {
                    Swap(index, LeftChild(index));
                    TrickleDown(LeftChild(index));
                }
            }
            if (array[index].CompareTo(array[RightChild(index)]) > 0 && RightChild(index) < Count)
            {
                if (array[RightChild(index)].CompareTo(array[LeftChild(index)]) < 0)
                {
                    Swap(index, RightChild(index));
                    TrickleDown(RightChild(index));
                }
            }
        }


        //Finished
        /// <summary>
        /// Gives the position of a node's parent, the node's position in the heap.
        /// </summary>
        private static int Parent(int position)
        {
            return ((position -1) / 2);

        }

        //Finished
        /// <summary>
        /// Returns the position of a node's left child, given the node's position.
        /// </summary>
        private static int LeftChild(int position)
        {
            return (2 * position + 1);
        }

        //Finished
        /// <summary>
        /// Returns the position of a node's right child, given the node's position.
        /// </summary>
        private static int RightChild(int position)
        {
            return (2 * position + 2);
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


