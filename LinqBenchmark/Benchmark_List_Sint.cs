using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using StructLinq;
using CTimers;

namespace LinqBenchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    [SimpleJob(RunStrategy.Throughput, launchCount: 1, iterationCount: 10, warmupCount: 1, invocationCount: -1)]
    public class Benchmark_List_Sint
    {
        #region dataset struct ============
        public struct Sint
        {
            public int _key { get; set; }
            public int _value { get; set; }

            int v0;
            int v1;
            int v2;
            int v3;
            int v4;
            int v5;
            int v6;
            int v7;
            int v8;
            int v9;

            public Sint(int key, int value)
            {
                _key = key;
                _value = value;
            }
        }
        #endregion

        /// <summary>
        /// default dataset size (could be adjusted)
        /// </summary>
        private const int _default_lim = 1000000;

        /// <summary>
        /// dataset
        /// </summary>
        private static List<Sint> _list;

        /// <summary>
        /// one half of max value
        /// </summary>
        private static int onehalf;

        //[Params(100, 1000000)]
        public static int Lim { get => _list.Count; set => InitDataSet(value); }

        public static void InitDataSet(int lim)
        {
            if (_list == null || _list.Count != lim)
            {
                const int max_value = 100;
                onehalf = max_value / 2;

                Random rnd = new Random();
                _list = new List<Sint>(lim);

                for (int i = 0; i < lim; ++i)
                {
                    int num = rnd.Next(max_value);
                    _list.Add(new Sint(i, num));
                }
            }
        }

        static Benchmark_List_Sint()
        {
            InitDataSet(_default_lim);
        }

        [Benchmark(Baseline = true)]
        public int Test_linq()
        {
            int sum = _list
                .Sum(item => item._value > onehalf ? item._value : 0);

            return sum;
        }

        [Benchmark]
        public int Test_linq_where_select()
        {
            int sum = _list
                .Where(item => item._value > onehalf)
                .Select(item => item._value)
                .Sum();
            return sum;
        }

        [Benchmark]
        public int Test_linq_from()
        {
            var selected_vals = from sval in _list where sval._value > onehalf select sval._value;
            int sum = selected_vals.Sum();
            return sum;
        }

        [Benchmark]
        public int Test_structlinq()
        {
            int sum = _list
                .ToStructEnumerable()
                .Sum(item => item._value > onehalf ? item._value : 0);
            return sum;
        }
        #region linq functors ==============
        struct WhereFunc : IFunction<Sint, bool>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly bool Eval(Sint item)
            {
                return item._value > onehalf;
            }
        }
        struct SelectFunc : IFunction<Sint, int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly int Eval(Sint item)
            {
                return item._value;
            }
        }
        struct SumFunc : IFunction<Sint, int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly int Eval(Sint item)
            {
                return item._value > onehalf ? item._value : 0;
            }
        }
        #endregion

        [Benchmark]
        public int Test_structlinq_ZeroAlloc()
        {
            int sum = _list
                .ToStructEnumerable()
                .Sum(item => item._value > onehalf ? item._value : 0, x => x);
            return sum;
        }

        [Benchmark]
        public int Test_structlinq_func()
        {
            var sumfunc = new SumFunc();

            int sum = _list
                .ToStructEnumerable()
                .Sum(sumfunc);
            return sum;
        }

        [Benchmark]
        public int Test_for()
        {
            int sum = 0;
            for (int i = 0; i < _list.Count; ++i)
            {
                int val = _list[i]._value;
                if (val > onehalf)
                    sum += val;
            }
            return sum;
        }

        [Benchmark]
        public int Test_foreach()
        {
            int sum = 0;
            foreach (Sint val in _list)
            {
                if (val._value > onehalf)
                    sum += val._value;
            }
            return sum;
        }

        [Benchmark]
        public int Test_IEnumerable()
        {
            IEnumerable<Sint> list = _list;

            int sum = 0;
            foreach (Sint sval in list)
            {
                int val = sval._value;
                if (val > onehalf)
                    sum += val;
            }
            return sum;
        }

        [Benchmark]
        public int Test_IStructEnumerable()
        {
            var list = _list.ToStructEnumerable();

            int sum = 0;
            foreach (Sint sval in list)
            {
                int val = sval._value;
                if (val > onehalf)
                    sum += val;
            }
            return sum;
        }

        internal static void QuickTest(int datasize, int iterations)
        {
            Console.WriteLine($"Benchmark_List_Sint quick test (datasize {datasize}, {iterations} iterations)");
            Console.WriteLine();

            Benchmark_List_Sint test = new Benchmark_List_Sint();
            Benchmark_List_Sint.Lim = datasize;

            // control sums
            Console.Write(test.Test_linq() + " ");
            Console.Write(test.Test_linq_where_select() + " ");
            Console.Write(test.Test_linq_from() + " ");
            Console.Write(test.Test_structlinq() + " ");
            Console.Write(test.Test_structlinq_ZeroAlloc() + " ");
            Console.Write(test.Test_structlinq_func() + " ");
            Console.Write(test.Test_for() + " ");
            Console.Write(test.Test_foreach() + " ");
            Console.Write(test.Test_IEnumerable() + " ");
            Console.Write(test.Test_IStructEnumerable() + " ");
            Console.WriteLine();
            Console.WriteLine();

            // fast tests
            using (var timer = new CTimer("Test_linq()")) { for (int i = 0; i < iterations; ++i) test.Test_linq(); }
            using (var timer = new CTimer("Test_linq_where_select()")) { for (int i = 0; i < iterations; ++i) test.Test_linq_where_select(); }
            using (var timer = new CTimer("Test_linq_from()")) { for (int i = 0; i < iterations; ++i) test.Test_linq_from(); }
            using (var timer = new CTimer("Test_structlinq()")) { for (int i = 0; i < iterations; ++i) test.Test_structlinq(); }
            using (var timer = new CTimer("Test_structlinq_ZeroAlloc()")) { for (int i = 0; i < iterations; ++i) test.Test_structlinq_ZeroAlloc(); }
            using (var timer = new CTimer("Test_structlinq_func()")) { for (int i = 0; i < iterations; ++i) test.Test_structlinq_func(); }
            using (var timer = new CTimer("Test_for()")) { for (int i = 0; i < iterations; ++i) test.Test_for(); }
            using (var timer = new CTimer("Test_foreach()")) { for (int i = 0; i < iterations; ++i) test.Test_foreach(); }
            using (var timer = new CTimer("Test_IEnumerable()")) { for (int i = 0; i < iterations; ++i) test.Test_IEnumerable(); }
            using (var timer = new CTimer("Test_IStructEnumerable()")) { for (int i = 0; i < iterations; ++i) test.Test_IStructEnumerable(); }
            Console.WriteLine();
        }

        internal static void Run()
        {
            BenchmarkRunner.Run<Benchmark_List_Sint>();
        }
    }
}
