```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4239/22H2/2022Update) (VMware)
Intel Core i7-2600K CPU 3.40GHz (Sandy Bridge), 1 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.300
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX
  Job-LIKNPA : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX

IterationCount=10  LaunchCount=1  RunStrategy=Throughput  
WarmupCount=1  

```
| Method                    | Mean      | Error     | StdDev    | Ratio | RatioSD | Rank | Gen0   | Allocated | Alloc Ratio |
|-------------------------- |----------:|----------:|----------:|------:|--------:|-----:|-------:|----------:|------------:|
| Test_linq                 | 321.09 ns | 73.780 ns | 48.801 ns |  1.00 |    0.00 |    8 | 0.0095 |      40 B |        1.00 |
| Test_linq_where           | 378.43 ns | 75.542 ns | 49.966 ns |  1.21 |    0.27 |    9 | 0.0172 |      72 B |        1.80 |
| Test_linq_from            | 332.09 ns | 43.686 ns | 25.997 ns |  1.04 |    0.20 |    8 | 0.0172 |      72 B |        1.80 |
| Test_structlinq           | 204.48 ns | 23.267 ns | 13.846 ns |  0.64 |    0.11 |    5 | 0.0076 |      32 B |        0.80 |
| Test_structlinq_ZeroAlloc | 151.20 ns | 10.274 ns |  6.796 ns |  0.48 |    0.07 |    4 |      - |         - |        0.00 |
| Test_structlinq_func      | 225.80 ns | 35.009 ns | 23.156 ns |  0.72 |    0.11 |    6 | 0.0134 |      56 B |        1.40 |
| Test_for                  | 138.86 ns | 22.568 ns | 14.928 ns |  0.44 |    0.05 |    3 |      - |         - |        0.00 |
| Test_foreach              | 124.12 ns | 20.597 ns | 13.624 ns |  0.39 |    0.07 |    2 |      - |         - |        0.00 |
| Test_IEnumerable          | 270.33 ns | 49.743 ns | 29.601 ns |  0.85 |    0.15 |    7 | 0.0095 |      40 B |        1.00 |
| Test_IStructEnumerable    |  83.73 ns |  4.803 ns |  2.858 ns |  0.26 |    0.04 |    1 |      - |         - |        0.00 |
