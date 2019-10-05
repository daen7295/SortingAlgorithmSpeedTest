using System;
using System.Diagnostics;
using System.Threading;

namespace EE
{
    class Program
    {
        static void Main()
        {
            // all combinations of long or short, varied or similar, and partially sorted or not
            // sort takes 3 booleans (is the data long, is the data highly varied, is the data partially sorted)
            Sort(false, false, false);
            Sort(false, false, true);
            Sort(false, true, false);
            Sort(false, true, true);
            Sort(true, false, false);
            Sort(true, false, true);
            Sort(true, true, false);
            Sort(true, true, true);
            Console.Beep();
        }

        // sort data through each algorithms that is long/short, high/low variation, and partially/not sorted
        private static void Sort(bool len, bool var, bool sort)
        {
            Random rand = new Random();
            int length, variation;
            // set values based on the booleans
            if (len)
                length = 1000000;
            else
                length = 10000;
            if (var)
                variation = 2000000000;
            else
                variation = 1000;

            // fill the arrays; there are two because if they are partially sorted, a heap must be supplied to HeapSort
            int[] x = new int[length];
            for (int i = 0; i < x.Length; i++)
                x[i] = rand.Next(0, variation);
            int[] z = new int[length];
            for (int i = 0; i < z.Length; i++)
                z[i] = rand.Next(0, variation);

            // if it must be partially sorted, sort the list and add 10% more random on the end
            int[] y, w;
            if (sort)
            {
                ShellSort(x, false);
                // heap sort is here to create a partially sorted heap to test with rather than giving it a small to large array
                HeapSort(z, false);
                // partially sorted small to large
                y = new int[(int)(length + length * 0.1)];
                for (int i = 0; i < x.Length; i++)
                    y[i] = x[i];
                for (int i = x.Length; i < y.Length; i++)
                    y[i] = rand.Next(0, variation);
                // partially sorted heap
                w = new int[(int)(length + length * 0.1)];
                for (int i = 0; i < z.Length; i++)
                    w[i] = z[i];
                for (int i = z.Length; i < w.Length; i++)
                    w[i] = rand.Next(0, variation);
            }
            else
            {
                w = new int[x.Length];
                y = new int[x.Length];
                for (int i = 0; i < x.Length; i++)
                    w[i] = y[i] = x[i];
            }

            // creates all the arrays to be sorted
            int[] a = new int[y.Length];
            int[] c = new int[y.Length];
            int[] d = new int[y.Length];
            int[] e = new int[y.Length];
            int[] b = new int[w.Length];

            for (int i = 0; i < y.Length; i++)
            {
                a[i] = c[i] = d[i] = e[i] = y[i];
                b[i] = w[i];
            }
            
            Console.WriteLine("\nLong?: {0}\tHigh Variation?: {1}\tPartially Sorted?: {2}\n", len, var, sort);
            InsertionSort(a);
            HeapSort(b);
            MergeSort(c);
            ShellSort(d);
            PigeonholeSort(e);
        }

        static void InsertionSort(int[] data, bool write = true)
        {
            // start timer
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // do this for each item in the list
            for (int i = 1; i < data.Length; i++)
            {
                // compare to those previous
                for (int j = i; j != 0 && data[j] < data[j - 1]; j--)
                {
                    // swap if it is smaller
                    int temp = data[j];
                    data[j] = data[j - 1];
                    data[j - 1] = temp;
                }
            }
            // output timer
            sw.Stop();
            if (write)
                Console.WriteLine("InsertionSort: {0} ms", sw.ElapsedMilliseconds);
        }

        static void HeapSort(int[] data, bool write = true)
        {
            // start timer
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            // heapify each index of the array
            int i = data.Length - 1;
            while (i >= 0)
                Heapify(data, i--);

            // output timer
            sw.Stop();
            if (write)
                Console.WriteLine("HeapSort: {0} ms", sw.ElapsedMilliseconds);
        }
        // turns an index and its children into a heap and lets the node float down if necessary using recursion
        static void Heapify(int[] data, int index)
        {
            int temp;
            // if the node has two children
            if (index * 2 + 2 < data.Length)
            {
                // is the first child the largest of the three?
                if (data[index] < data[index * 2 + 1] && data[index * 2 + 1] >= data[index * 2 + 2])
                {
                    // swap and heapify the node that was lowered
                    temp = data[index];
                    data[index] = data[index * 2 + 1];
                    data[index * 2 + 1] = temp;
                    Heapify(data, index * 2 + 1);
                }
                // is the second child largest?
                if (data[index] < data[index * 2 + 2])
                {
                    // swap and heapify the node that was lowered
                    temp = data[index];
                    data[index] = data[index * 2 + 2];
                    data[index * 2 + 2] = temp;
                    Heapify(data, index * 2 + 2);
                }
            }
            // if the node has one child
            else if (index * 2 + 1 < data.Length)
            {
                // is the child larger?
                if (data[index] < data[index * 2 + 1])
                {
                    // swap
                    temp = data[index];
                    data[index] = data[index * 2 + 1];
                    data[index * 2 + 1] = temp;
                }
            }
            // do nothing if the node has zero children
        }

        static void MergeSort(int[] data, bool write = true)
        {
            // start timer
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Merge(data, 0, data.Length - 1);

            // output timer
            sw.Stop();
            if (write)
                Console.WriteLine("MergeSort: {0} ms", sw.ElapsedMilliseconds);
        }
        static void Merge(int[] data, int l, int r)
        {
            if (l < r)
            {
                int w = l;
                int m = (l + r) / 2;
                // split the array into two and sort them using recursion
                Merge(data, l, m);
                Merge(data, m + 1, r);

                // map both sorted arrays onto a new array
                int[] sorted = new int[r - l + 1];
                int z = m + 1;
                int c = 0;
                while (l <= m && z <= r)
                {
                    if (data[l] < data[z])
                        sorted[c++] = data[l++];
                    else
                        sorted[c++] = data[z++];
                }
                // put the remaining integers into the end of the sorted array
                while (l <= m)
                    sorted[c++] = data[l++];
                while (z <= r)
                    sorted[c++] = data[z++];
                // map the sorted array back onto the original
                for (int u = 0; w + u <= r; u++)
                    data[w + u] = sorted[u];
            }
        }

        static void ShellSort(int[] data, bool write = true)
        {
            // start timer
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // for each pass, gap lowers until it is one
            for (int gap = data.Length / 2; gap > 0; gap /= 2)
            {
                // insertion sort on each smaller list created by the gap (i.e. every nth term)
                for (int i = gap; i < data.Length; i++)
                {
                    for (int j = i; j > gap - 1 && data[j] < data[j - gap]; j -= gap)
                    {
                        int temp = data[j];
                        data[j] = data[j - gap];
                        data[j - gap] = temp;
                    }
                }
            }
            // output timer
            sw.Stop();
            if (write)
                Console.WriteLine("ShellSort: {0} ms", sw.ElapsedMilliseconds);
        }

        static void PigeonholeSort(int[] data, bool write = true)
        {
            // start timer
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // find max and min
            int max = data[0];
            int i, c;
            c = 0;
            for (i = 0; i < data.Length; i++)
                if (data[i] > max)
                    max = data[i];
            int min = data[0];
            for (i = 0; i < data.Length; i++)
                if (data[i] < min)
                    min = data[i];

            // make new array and increment it for values found in the data set
            int[] pigeon = new int[max - min + 1];
            for (i = 0; i < data.Length; i++)
                pigeon[data[i] - min]++;

            // use the pigeon array to fill the original with sorted data
            for (i = 0; i < pigeon.Length; i++)
                while (pigeon[i]-- > 0)
                    data[c++] = i + min;

            // output timer
            sw.Stop();
            if (write)
                Console.WriteLine("PigeonholeSort: {0} ms", sw.ElapsedMilliseconds);
        }
    }
}