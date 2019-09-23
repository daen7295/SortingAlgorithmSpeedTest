# SortingAlgorithmSpeedTest
I wrote this as part of my Extended Essay on sorting algorithms for the IB programme.

Summary: Sorts long int arrays with different sorting algorithms and times them to see which one works the fastest

Note on the testing criteria:
The code tests two values for each of 3 characteristics
  1. The data is either 10000 ints long or 1000000 ints long
  2. The data has a range of 0 to 999 or 0 to 1999999999
  3. The data is ~90% sorted or completely random

If you want to add more algorithms:
  1. Make a new int array in the "Sort" method, they follow a, b, c, etc.
  2. Set it equal to "y" with the other arrays
  3. Add a new method that performs the algorithm and call it on the new variable. Make sure to start a Stopwatch at the beginning and stop it at the end and output the execution time.

A note about partially sorted data:
Partially sorted data is data sorted with shell sort (fastest in my experience) and has 10% random data added to the end.
If an algorithm does not sort from smallest to largest, such as heapsort, which sorts into heaps, it should be supplied with
data that is sorted into its data structure. To do this there is an if block in "Sort" that runs the algorithm with no output
and adds random integers to the end. To use an algorithm that does not sort into heaps or smallest to largest, create a new
array in that if block and sort it with the algorithm and add the random ints. Then set whatever array is to be sorted for the
test to equal the partially sorted one from the if block.
