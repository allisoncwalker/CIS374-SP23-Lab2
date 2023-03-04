using System;
using System.Linq;
using System.Reflection;


namespace Lab2
{
    public class MaxHeap<T> where T : IComparable<T>
    {
        private T[] array;
        private int initialSize = 8;

        public int Count { get; private set; } = 0;

        public int Capacity => array.Length;

        public bool IsEmpty => Count == 0;

        public MaxHeap(T[] initialArray = null)
        {
            array = new T[initialSize];

            // Add initial value if present
            if (initialArray != null && initialArray.Length > 0)
            {
                foreach (var item in initialArray)
                {
                    Add(item);
                }
            }
        }

        //Finished
        /// <summary>
        /// Returns the min item but does NOT remove it.
        /// Time complexity: O(1).
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

            TrickleUp(Count);

            Count++;

            // Resize array if full
            if (Count == array.Length)
            {
                DoubleArrayCapacity();
            }
        }

        public T Extract()
        {
            return ExtractMax();
        }

        // Finished
        /// <summary>
        /// Removes and returns the max item in the max-heap.
        /// Time complexity: O(?).
        /// </summary>
        public T ExtractMax()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T max = array[0];

            // swap max with last element
            Swap(0, Count - 1);

            // remove last element
            Count--;

            // trickle down from root
            TrickleDown(0);

            return max;
        }

        /// Finished
        /// <summary>
        /// Removes and returns the min item in the max-heap.
        /// Time complexity: O(?).
        /// </summary>
        public T ExtractMin()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            int mIndex = (Count - 1) / 2 + 1;
            T min = array[mIndex];
            for (int i = mIndex + 1; i < Count; i++)
            {
                if (array[i].CompareTo(min) < 0)
                {
                    min = array[i];
                    mIndex = i;
                }
            }

            Swap(mIndex, Count - 1);

            Count--;

            TrickleUp(mIndex);

            return min;
        }

        //Finished
        /// <summary>
        /// Returns true if the heap contains the given value; otherwise false.
        /// Time complexity: O(N).
        /// </summary>
        public bool Contains(T value)
        {
            // linear search
            for(int i = 0; i< Count; i++)
            //foreach (var item in array)
            {
                if (array[i].CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;

        }
        /// Finished
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

                //Swap(index, Count - 1);
                //Count--;
                //TrickleUp(index);
                //return min;
            
            if(array[index].CompareTo(array[Parent(index)]) < 0)
            {
                TrickleUp(index);
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

        // Finished
        private void TrickleUp(int index)
        {
            //if (index != 0)
            //var parentIndex = Parent(index);
            if (array[index].CompareTo(array[Parent(index)]) > 0)
            {
                Swap(index, Parent(index));
                TrickleUp( Parent(index));
            }
        }

        // Finished
        private void TrickleDown(int index)
        {
            if (LeftChild(index) >= Count)
            {
                return;
            }

            if (array[index].CompareTo(array[LeftChild(index)]) < 0 && LeftChild(index) < Count)
            {
                if (array[LeftChild(index)].CompareTo(array[RightChild(index)]) > 0 || RightChild(index) >= Count)
                {
                    Swap(index, LeftChild(index));
                    TrickleDown(LeftChild(index));

                }
            }
            if (array[index].CompareTo(array[RightChild(index)]) < 0 && RightChild(index) < Count)
            {
                if (array[RightChild(index)].CompareTo(array[LeftChild(index)]) > 0)
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
            return (position - 1) / 2;

        }

        //Finished
        /// <summary>
        /// Returns the position of a node's left child, given the node's position.
        /// </summary>
        private static int LeftChild(int position)
        {
            return (2 * position + 2);

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