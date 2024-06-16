using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using CTimers;
using StructLinq;

namespace LinqBenchmark;

[MemoryDiagnoser]
[RankColumn]
[SimpleJob(RunStrategy.Throughput, 1, iterationCount: 10, warmupCount: 1, invocationCount: -1)]
public class Benchmark_List_int
{
    /// <summary>
    ///     default dataset size (could be adjusted)
    /// </summary>
    private const int _default_lim = 1000000;

    /// <summary>
    ///     dataset
    /// </summary>
    private static List<int> _list;

    /// <summary>
    ///     one half of max value
    /// </summary>
    private static int onehalf;

	static Benchmark_List_int()
	{
		InitDataSet(_default_lim);
	}

	//[Params(100, 1000000)]
	public static int Lim
	{
		get => _list.Count;
		set => InitDataSet(value);
	}

	public static void InitDataSet(int lim)
	{
		if (_list == null || _list.Count != lim)
		{
			const int max_value = 100;
			onehalf = max_value / 2;

			var rnd = new Random();
			_list = new List<int>(lim);

			for (int i = 0; i < lim; ++i)
			{
				int num = rnd.Next(max_value);
				_list.Add(num);
			}
		}
	}

	[Benchmark(Baseline = true)]
	public int Test_linq()
	{
		int sum = _list
			.Sum(item => item > onehalf ? item : 0);

		return sum;
	}

	[Benchmark]
	public int Test_linq_where()
	{
		int sum = _list
			.Where(item => item > onehalf)
			.Sum();
		return sum;
	}

	[Benchmark]
	public int Test_linq_from()
	{
		var selected_vals = from val in _list where val > onehalf select val;
		int sum = selected_vals.Sum();
		return sum;
	}

	[Benchmark]
	public int Test_structlinq()
	{
		int sum = _list
			.ToStructEnumerable()
			.Sum(item => item > onehalf ? item : 0);
		return sum;
	}

	[Benchmark]
	public int Test_structlinq_ZeroAlloc()
	{
		int sum = _list
			.ToStructEnumerable()
			.Sum(item => item > onehalf ? item : 0, x => x);
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
			int val = _list[i];
			if (val > onehalf)
				sum += val;
		}

		return sum;
	}

	[Benchmark]
	public int Test_foreach()
	{
		int sum = 0;
		foreach (int val in _list)
			if (val > onehalf)
				sum += val;
		return sum;
	}

	[Benchmark]
	public int Test_IEnumerable()
	{
		IEnumerable<int> list = _list;

		int sum = 0;
		foreach (int val in list)
			if (val > onehalf)
				sum += val;
		return sum;
	}

	[Benchmark]
	public int Test_IStructEnumerable()
	{
		var list = _list.ToStructEnumerable();

		int sum = 0;
		foreach (int val in list)
			if (val > onehalf)
				sum += val;
		return sum;
	}

	internal static void QuickTest(int datasize, int iterations)
	{
		Console.WriteLine($"Benchmark_List_int quick test (datasize {datasize}, {iterations} iterations)");
		Console.WriteLine();

		var test = new Benchmark_List_int();
		Lim = datasize;

		// control sums
		Console.Write(test.Test_linq() + " ");
		Console.Write(test.Test_linq_where() + " ");
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

		// quick tests
		using (var timer = new CTimer("Test_linq()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_linq();
		}

		using (var timer = new CTimer("Test_linq_where_select()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_linq_where();
		}

		using (var timer = new CTimer("Test_linq_from()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_linq_from();
		}

		using (var timer = new CTimer("Test_structlinq()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_structlinq();
		}

		using (var timer = new CTimer("Test_structlinq_ZeroAlloc()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_structlinq_ZeroAlloc();
		}

		using (var timer = new CTimer("Test_structlinq_func()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_structlinq_func();
		}

		using (var timer = new CTimer("Test_for()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_for();
		}

		using (var timer = new CTimer("Test_foreach()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_foreach();
		}

		using (var timer = new CTimer("Test_IEnumerable()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_IEnumerable();
		}

		using (var timer = new CTimer("Test_IStructEnumerable()"))
		{
			for (int i = 0; i < iterations; ++i) test.Test_IStructEnumerable();
		}

		Console.WriteLine();
	}

	internal static void Run()
	{
		BenchmarkRunner.Run<Benchmark_List_int>();
	}

	#region linq functors ==============

	private struct WhereFunc : IFunction<int, bool>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Eval(int item)
		{
			return item > onehalf;
		}
	}

	private struct SumFunc : IFunction<int, int>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly int Eval(int item)
		{
			return item > onehalf ? item : 0;
		}
	}

	#endregion
}