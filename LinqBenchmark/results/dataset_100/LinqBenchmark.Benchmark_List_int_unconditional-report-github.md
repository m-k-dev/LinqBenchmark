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
| Test_linq                 |  22.22 ns |  1.395 ns |  0.923 ns |  1.00 |    0.00 |    1 |      - |         - |          NA |
| Test_linq_from            | 408.88 ns | 41.358 ns | 27.356 ns | 18.41 |    1.15 |    7 | 0.0172 |      72 B |          NA |
| Test_structlinq           |  75.38 ns |  6.392 ns |  4.228 ns |  3.40 |    0.24 |    3 | 0.0076 |      32 B |          NA |
| Test_structlinq_ZeroAlloc |  63.79 ns |  6.332 ns |  4.189 ns |  2.88 |    0.26 |    2 |      - |         - |          NA |
| Test_for                  |  89.80 ns | 12.602 ns |  7.499 ns |  4.06 |    0.44 |    4 |      - |         - |          NA |
| Test_foreach              | 102.13 ns | 14.392 ns |  8.564 ns |  4.62 |    0.44 |    5 |      - |         - |          NA |
| Test_IEnumerable          | 238.03 ns | 39.782 ns | 26.313 ns | 10.76 |    1.51 |    6 | 0.0095 |      40 B |          NA |
| Test_IStructEnumerable    |  63.87 ns |  9.312 ns |  5.541 ns |  2.89 |    0.32 |    2 |      - |         - |          NA |
