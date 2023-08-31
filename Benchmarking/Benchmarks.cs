using BenchmarkDotNet.Attributes;

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
