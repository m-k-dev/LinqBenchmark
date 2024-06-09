```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4239/22H2/2022Update) (VMware)
Intel Core i7-2600K CPU 3.40GHz (Sandy Bridge), 1 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.300
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX
  Job-CESLKO : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX

IterationCount=10  LaunchCount=1  RunStrategy=Throughput  
WarmupCount=1  

```
| Method                    | Mean      | Error     | StdDev    | Ratio | RatioSD | Rank | Allocated | Alloc Ratio |
|-------------------------- |----------:|----------:|----------:|------:|--------:|-----:|----------:|------------:|
| Test_linq                 | 13.347 ms | 3.6241 ms | 2.3971 ms |  1.00 |    0.00 |    7 |      92 B |        1.00 |
| Test_linq_where_select    | 12.794 ms | 2.3476 ms | 1.3970 ms |  0.99 |    0.15 |    7 |     284 B |        3.09 |
| Test_linq_from            | 12.360 ms | 1.4885 ms | 0.9845 ms |  0.95 |    0.15 |    7 |     278 B |        3.02 |
| Test_structlinq           |  8.141 ms | 0.6471 ms | 0.4280 ms |  0.63 |    0.11 |    5 |      35 B |        0.38 |
| Test_structlinq_ZeroAlloc |  7.268 ms | 0.4344 ms | 0.2873 ms |  0.56 |    0.08 |    4 |       3 B |        0.03 |
| Test_structlinq_func      |  8.053 ms | 0.9921 ms | 0.6562 ms |  0.62 |    0.12 |    5 |      62 B |        0.67 |
| Test_for                  |  6.429 ms | 0.0331 ms | 0.0173 ms |  0.52 |    0.06 |    2 |       3 B |        0.03 |
| Test_foreach              |  6.802 ms | 0.0709 ms | 0.0422 ms |  0.53 |    0.08 |    3 |       3 B |        0.03 |
| Test_IEnumerable          | 11.578 ms | 1.7136 ms | 1.1334 ms |  0.89 |    0.14 |    6 |      86 B |        0.93 |
| Test_IStructEnumerable    |  6.117 ms | 0.1264 ms | 0.0836 ms |  0.47 |    0.08 |    1 |       3 B |        0.03 |
