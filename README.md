#  Ray Tracer in .NET

![compile time render](docs/output.png)

Introduction
------------

This implementation is a port of the C++ code contained in the repository at https://github.com/tcbrindle/raytracer.hpp. The main goal of the project is to create an implementation in .NET that is as efficient as possible. 

TODO
--------
- ~~Reimplementation~~
- Support for multiple cores,
- Support for multiple cores in batch mode,
- Support for SSE instructions,
- Support for AVX instructions, 
- Improved performance after each release.

Benchmarks
------------
- v 1.0 - Reimplementation

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.959 (1909/November2018Update/19H2)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.6.20318.15
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
```
|   Method | RenderSize |     Mean |    Error |   StdDev |
|--------- |----------- |---------:|---------:|---------:|
| Baseline |        512 | 664.7 ms | 16.05 ms | 47.08 ms |

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.329 (2004/?/20H1)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET Core SDK=5.0.100-preview.6.20318.15
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT
```
|   Method | RenderSize |     Mean |   Error |  StdDev |
|--------- |----------- |---------:|--------:|--------:|
| Baseline |        512 | 466.1 ms | 3.10 ms | 2.75 ms |
