```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4239/22H2/2022Update) (VMware)
Intel Core i7-2600K CPU 3.40GHz (Sandy Bridge), 1 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.300
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX
  Job-CESLKO : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX

IterationCount=10  LaunchCount=1  RunStrategy=Throughput  
WarmupCount=1  

```
| Method                    | Mean       | Error     | StdDev    | Ratio | RatioSD | Rank | Allocated | Alloc Ratio |
|-------------------------- |-----------:|----------:|----------:|------:|--------:|-----:|----------:|------------:|
| Test_linq                 |   249.0 μs |  22.19 μs |  14.68 μs |  1.00 |    0.00 |    1 |         - |          NA |
| Test_linq_from            | 3,526.0 μs | 670.58 μs | 399.05 μs | 14.41 |    1.99 |    5 |      75 B |          NA |
| Test_structlinq           |   610.1 μs |  81.10 μs |  53.64 μs |  2.46 |    0.23 |    2 |      32 B |          NA |
| Test_structlinq_ZeroAlloc |   638.7 μs | 108.35 μs |  71.66 μs |  2.57 |    0.34 |    2 |         - |          NA |
| Test_for                  |   987.2 μs | 179.40 μs | 118.66 μs |  3.98 |    0.55 |    3 |         - |          NA |
| Test_foreach              |   921.7 μs | 126.91 μs |  75.52 μs |  3.76 |    0.32 |    3 |       1 B |          NA |
| Test_IEnumerable          | 2,232.0 μs | 302.70 μs | 200.22 μs |  8.98 |    0.87 |    4 |      42 B |          NA |
| Test_IStructEnumerable    |   609.6 μs |  65.32 μs |  38.87 μs |  2.49 |    0.19 |    2 |         - |          NA |
