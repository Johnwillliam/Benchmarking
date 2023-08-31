using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

public class Program
{
    static void Main(string[] args)
    {
        var config = DefaultConfig.Instance
                    .AddJob(Job
                         .MediumRun
                         .WithLaunchCount(1)
                         .WithToolchain(InProcessEmitToolchain.Instance))
                    .AddDiagnoser(MemoryDiagnoser.Default);
        //BenchmarkRunner.Run<Benchmarks>(config);
        //BenchmarkRunner.Run<BoxingBenchmark>(config);
        //BenchmarkRunner.Run<StackAllocPerfTest>();
        //BenchmarkRunner.Run<FieldsVsPropertiesBenchmark>();
        BenchmarkRunner.Run<SpanVsListBenchmark>();
    }
}
public class PointClass
{
    public int X { get; set; }
    public int Y { get; set; }
    public PointClass(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public struct PointStruct
{
    public int X { get; set; }
    public int Y { get; set; }
    public PointStruct(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public readonly struct PointReadonlyStruct
{
    public int X { get; init; }
    public int Y { get; init; }
    public PointReadonlyStruct(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public sealed record SealedRecord(int Value);

public record Record(int Value);

public class SealedClass
{
    public int Value { get; }
    public SealedClass(int value)
    {
        Value = value;
    }
}

public class NonSealedClass
{
    public int Value { get; }
    public NonSealedClass(int value)
    {
        Value = value;
    }
}

public class SpanVsListBenchmark
{
    private int[] _dataArray;
    private List<int> _dataList;

    [Params(100, 1000, 10000)] // Vary the data size
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        _dataArray = new int[N];
        _dataList = new List<int>(N);

        for (int i = 0; i < N; i++)
        {
            _dataArray[i] = i;
            _dataList.Add(i);
        }
    }

    [Benchmark]
    public int SumUsingSpan()
    {
        int sum = 0;
        for (int i = 0; i < _dataArray.Length; i++)
        {
            sum += _dataArray[i];
        }
        return sum;
    }

    [Benchmark]
    public int SumUsingList()
    {
        int sum = 0;
        for (int i = 0; i < _dataList.Count; i++)
        {
            sum += _dataList[i];
        }
        return sum;
    }

    [Benchmark]
    public int SumUsingSpanIterate()
    {
        int sum = 0;
        foreach (var value in _dataArray.AsSpan())
        {
            sum += value;
        }
        return sum;
    }

    [Benchmark]
    public int SumUsingListForEach()
    {
        int sum = 0;
        foreach (var value in _dataList)
        {
            sum += value;
        }
        return sum;
    }
}