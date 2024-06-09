using BenchmarkDotNet.Running;

namespace LinqBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // unconditional sum, list of int
            Benchmark_List_int_unconditional.QuickTest(100, 200000);
            Benchmark_List_int_unconditional.QuickTest(1000000, 20);

            // conditional sum, list of int
            Benchmark_List_int.QuickTest(100, 200000);
            Benchmark_List_int.QuickTest(1000000, 20);

            // conditional sum, list of structs
            Benchmark_List_Sint.QuickTest(100, 200000);
            Benchmark_List_Sint.QuickTest(1000000, 20);

            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(Benchmark_List_int_unconditional),
                typeof(Benchmark_List_int),
                typeof(Benchmark_List_Sint),
            });
            switcher.Run(args);
        }
    }
}
