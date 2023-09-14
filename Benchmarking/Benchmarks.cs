using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
public class Benchmarks
{
    [Benchmark]
    public void CreateSealedClass()
    {
        for (int i = 0; i < 1000000; i++)
        {
            var obj = new SealedClass(i);
        }
    }

    [Benchmark]
    public void CreateNonSealedClass()
    {
        for (int i = 0; i < 1000000; i++)
        {
            var obj = new NonSealedClass(i);
        }
    }

    [Benchmark]
    public void CreateRecord()
    {
        for (int i = 0; i < 1000000; i++)
        {
            var obj = new Record(i);
        }
    }

    [Benchmark]
    public void CreateSealedRecord()
    {
        for (int i = 0; i < 1000000; i++)
        {
            var obj = new SealedRecord(i);
        }
    }

    [Benchmark]
    public void CreateClassesBenchmark()
    {
        var a1 = new PointClass[10_000_000];
        for (int i = 0; i < a1.Length; i++)
        {
            a1[i] = new PointClass(i, i);
        }
    }

    [Benchmark]
    public void CreateStructsBenchmark()
    {
        var a1 = new PointStruct[10_000_000];
        for (int i = 0; i < a1.Length; i++)
        {
            a1[i] = new PointStruct(i, i);
        }
    }

    [Benchmark]
    public void CreateReadonlyStructsBenchmark()
    {
        var a1 = new PointReadonlyStruct[10_000_000];
        for (int i = 0; i < a1.Length; i++)
        {
            a1[i] = new PointReadonlyStruct(i, i);
        }
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