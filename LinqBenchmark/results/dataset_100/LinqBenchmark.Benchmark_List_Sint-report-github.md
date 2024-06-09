```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4239/22H2/2022Update) (VMware)
Intel Core i7-2600K CPU 3.40GHz (Sandy Bridge), 1 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.300
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX
  Job-LIKNPA : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX

IterationCount=10  LaunchCount=1  RunStrategy=Throughput  
WarmupCount=1  

```
| Method                    | Mean      | Error     | StdDev   | Ratio | RatioSD | Rank | Gen0   | Allocated | Alloc Ratio |
|-------------------------- |----------:|----------:|---------:|------:|--------:|-----:|-------:|----------:|------------:|
| Test_linq                 | 712.77 ns | 121.17 ns | 80.15 ns |  1.00 |    0.00 |    6 | 0.0191 |      80 B |        1.00 |
| Test_linq_where_select    | 767.84 ns | 134.08 ns | 88.68 ns |  1.09 |    0.18 |    6 | 0.0648 |     272 B |        3.40 |
| Test_linq_from            | 748.11 ns |  73.88 ns | 38.64 ns |  1.07 |    0.16 |    6 | 0.0648 |     272 B |        3.40 |
| Test_structlinq           | 254.18 ns |  24.69 ns | 14.69 ns |  0.36 |    0.05 |    5 | 0.0076 |      32 B |        0.40 |
| Test_structlinq_ZeroAlloc | 226.73 ns |  22.60 ns | 13.45 ns |  0.32 |    0.04 |    4 |      - |         - |        0.00 |
| Test_structlinq_func      | 277.33 ns |  49.66 ns | 29.55 ns |  0.39 |    0.06 |    5 | 0.0134 |      56 B |        0.70 |
| Test_for                  | 136.31 ns |  19.02 ns | 12.58 ns |  0.19 |    0.03 |    2 |      - |         - |        0.00 |
| Test_foreach              | 169.43 ns |  22.66 ns | 13.49 ns |  0.24 |    0.03 |    3 |      - |         - |        0.00 |
| Test_IEnumerable          | 731.03 ns | 142.69 ns | 94.38 ns |  1.03 |    0.16 |    6 | 0.0191 |      80 B |        1.00 |
| Test_IStructEnumerable    |  92.75 ns |  18.59 ns | 12.29 ns |  0.13 |    0.03 |    1 |      - |         - |        0.00 |
