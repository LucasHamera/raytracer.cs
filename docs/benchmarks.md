#  Benchmark results
## v 1.0 - Reimplementation

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.329 (2004/?/20H1)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET Core SDK=5.0.100-preview.6.20318.15
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
```
``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.836 (1903/May2019Update/19H1)
Intel Core i7-4770 CPU 3.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.6.20318.15
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
```
``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.959 (1909/November2018Update/19H2)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.6.20318.15
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
```

| Processor             | RenderSize | Mean     | Error    | StdDev   |
|---------------------- |----------- |---------:|---------:|---------:|
| AMD Ryzen 7 1700      |        512 | 466.1 ms | 3.10 ms  | 2.75 ms  |
| Intel Core i7-4770    |        512 | 509.7 ms | 10.16 ms | 22.73 ms |
| Intel Core i7-6700HQ  |        512 | 664.7 ms | 16.05 ms | 47.08 ms |

------------
