// C# program to sort two lists of identical randomly generated numbers, and to search said list for a given element.

using System.Diagnostics;

namespace SortingApp
{
    class App
    {
        static void Main(string[] args)
        {

            string format = "{0, -38} {1}";

            
            const int MAXSIZE = 10000;

            int[] myints = new int[MAXSIZE];
            myints = FillArray(myints);
            int[] mybruteforce = myints;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Reset();
            stopwatch.Start();
            myints = MergeSort(myints);
            stopwatch.Stop();
            var elapsedmerge = stopwatch.ElapsedTicks;
           
            
            stopwatch.Reset();
            stopwatch.Start();
            mybruteforce = SelectionSort(mybruteforce);
            stopwatch.Stop();
            var elapsedselection = stopwatch.ElapsedTicks;

            for (int i = 0; i < myints.Length; i++)
            {
                Console.WriteLine(format ,myints[i] , ": MergeSorted");
            }
            Console.WriteLine();
            for (int i = 0; i < mybruteforce.Length; i++)
            {
                Console.WriteLine(format,mybruteforce[i] , ": SelectionSorted");
            }
            
            Console.WriteLine(format, "Ticks elapsed selection sorting: " , elapsedselection);
            Console.WriteLine(format, "Ticks elapsed merge sorting: " , elapsedmerge);
            Console.WriteLine("Search for an element? (Leave blank to skip search) : ");
            var input = Console.ReadLine();
            input.TrimEnd();
            int intout;
            if (input != "")
            {
                int.TryParse(input, out intout);
                stopwatch.Start();
                var pos = BruteSearch(myints, intout);
                stopwatch.Stop();
                Console.WriteLine(format, "Ticks elapsed Dumb searching: " , stopwatch.ElapsedTicks);
                Console.WriteLine(format, "Position:", pos);
                
                stopwatch.Reset();
                stopwatch.Start();
                pos = SmarterSearch(myints, intout);
                stopwatch.Stop();
                Console.WriteLine(format, "Ticks elapsed Smarter searching: " , stopwatch.ElapsedTicks);
                Console.WriteLine(format, "Position:", pos);

                Console.Read();
            }
            
        }

        public static int[] MergeSort(int[] arr)
        {
            //Function that implements merge sort on an integer array
            //Authored:  11/1/2023 3:00 P.M by Nicholas Pullara
            //Edited: 11/16/2023  4:15 P.M by Nicholas Pullara
            //Input: Unsorted integer array arr
            //Output: Sorted integer array arr
            if (arr.Length > 1)
            {
                var arraylength = arr.Length;
                var tempA = new int[arraylength / 2];
                var tempB = new int[arraylength - tempA.Length];
                for (var i = 0; i <= arraylength / 2 - 1; i++) tempA[i] = arr[i];
                for (int i = tempA.Length, j = 0; i <= arraylength - 1; i++, j++) tempB[j] = arr[i];


                tempA = MergeSort(tempA);
                tempB = MergeSort(tempB);

                arr = Merge(tempA, tempB, arr);
            }

            return arr;
        }
        
        private static int[] Merge(int[] B, int[] C, int[] A)
        {
            int i = 0, j = 0, p = 0;
            while (i < B.Length && j < C.Length)
            {
                if (B[i] <= C[j])
                {
                    A[p] = B[i];
                    i++;
                }
                else
                {
                    A[p] = C[j];
                    j++;
                }

                p++;
            }

            while (j < C.Length)
            {
                A[p] = C[j];
                j++;
                p++;
            }

            return A;
        }
        
        public static int[] SelectionSort(int[] arr)
        {
            //Function that implements selection sort on an integer array
            //Authored:  12/3/2023 10:30 A.M by Nicholas Pullara
            //Edited: 12/3/2023 10:30 A.M by Nicholas Pullara
            //Input: Unsorted integer array arr
            //Output: Sorted integer array arr
            for (int i = 0; i < arr.Length; i++)
            {
                var min = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] >= arr[min])
                        continue; //Jetbrains says invert if statement to avoid nesting... that's kinda cool ig.
                    min = j;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }

            }

            return arr;
        }

        public static int[] FillArray(int[] arr)
        {
            //Function fills an integer array with random values
            //Authored:  12/3/2023 10:30 A.M by Nicholas Pullara
            //Edited: 12/3/2023 10:30 A.M by Nicholas Pullara
            //Input: empty array arr
            //Output: filled array arr
            var rand = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next();
            }

            return arr;
        }

        public static int BruteSearch(int[] arr, int key)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == key)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int SmarterSearch(int[] arr, int key)
        {
            if (key < arr[0] || key > arr[arr.Length-1])
            {
                return -1;
            }

            int topdiff = arr[arr.Length - 1] - key;
            int botdiff = key - arr[0];
            
            if (topdiff > botdiff)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == key)
                    {
                        return i;
                    }
                }
            }
            if (topdiff < botdiff)
            {
                for (int i = arr.Length-1; i >= 0; i--)
                {
                    if (arr[i] == key)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        
    }
}

