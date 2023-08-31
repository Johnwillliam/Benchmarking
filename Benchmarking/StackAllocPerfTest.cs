using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

[MemoryDiagnoser]
public class StackAllocPerfTest
{
    const int _iterations = 1000;

    [Benchmark(Baseline = true)]
    public int Array()
    {
        var total = 0;
        for (var i = 0; i < _iterations; i++)
        {
            var a = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            total += a[9];
        }
        return total;
    }

    [Benchmark]
    public unsafe int StackAlloc()
    {
        var total = 0;
        for (var i = 0; i < _iterations; i++)
        {
            var a = stackalloc int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            total += a[9];
        }
        return total;
    }

    [Benchmark()]
    public int SpanArray()
    {
        var total = 0;
        for (var i = 0; i < _iterations; i++)
        {
            Span<int> a = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            total += a[9];
        }
        return total;
    }

    [Benchmark]
    public int SpanStackAlloc()
    {
        var total = 0;
        for (var i = 0; i < _iterations; i++)
        {
            Span<int> a = stackalloc int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            total += a[9];
        }
        return total;
    }

}
