```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4239/22H2/2022Update) (VMware)
Intel Core i7-2600K CPU 3.40GHz (Sandy Bridge), 1 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.300
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX
  Job-CESLKO : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX

IterationCount=10  LaunchCount=1  RunStrategy=Throughput  
WarmupCount=1  

```
| Method                    | Mean     | Error     | StdDev    | Ratio | RatioSD | Rank | Allocated | Alloc Ratio |
|-------------------------- |---------:|----------:|----------:|------:|--------:|-----:|----------:|------------:|
| Test_linq                 | 7.601 ms | 1.2594 ms | 0.8330 ms |  1.00 |    0.00 |    4 |      46 B |        1.00 |
| Test_linq_where           | 8.723 ms | 1.8049 ms | 1.1938 ms |  1.16 |    0.19 |    4 |      78 B |        1.70 |
| Test_linq_from            | 7.920 ms | 0.9989 ms | 0.5224 ms |  1.06 |    0.14 |    4 |      78 B |        1.70 |
| Test_structlinq           | 5.776 ms | 0.8517 ms | 0.5634 ms |  0.77 |    0.11 |    2 |      35 B |        0.76 |
| Test_structlinq_ZeroAlloc | 4.924 ms | 1.0890 ms | 0.7203 ms |  0.66 |    0.13 |    2 |       3 B |        0.07 |
| Test_structlinq_func      | 5.477 ms | 0.8844 ms | 0.5263 ms |  0.73 |    0.12 |    2 |      59 B |        1.28 |
| Test_for                  | 5.397 ms | 0.8394 ms | 0.5552 ms |  0.71 |    0.07 |    2 |       3 B |        0.07 |
| Test_foreach              | 5.115 ms | 0.6151 ms | 0.4069 ms |  0.68 |    0.09 |    2 |       3 B |        0.07 |
| Test_IEnumerable          | 6.670 ms | 0.7968 ms | 0.4168 ms |  0.90 |    0.15 |    3 |      46 B |        1.00 |
| Test_IStructEnumerable    | 3.802 ms | 0.4384 ms | 0.2609 ms |  0.51 |    0.05 |    1 |       3 B |        0.07 |
