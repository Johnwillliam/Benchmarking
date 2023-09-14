using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

[MaxIterationCount(20)]
[WarmupCount(1)]
public class BoxingBenchmark
{
    private List<int> _intList;
    private List<object> _objList;

    [GlobalSetup]
    public void Setup()
    {
        _intList = new List<int>();
        _objList = new List<object>();
    }

    //[Benchmark]
    public void Boxing_Int()
    {
        _objList.Add(42); // Boxing occurs here
    }

    //[Benchmark]
    public void NoBoxing_Int()
    {
        _intList.Add(42); // No boxing with List<int>
    }

    [Benchmark]
    public void Boxing_Strings()
    {
        for (int i = 0; i < 100_000; i++)
        {
            var test = "Name " + i;
        }
    }

    [Benchmark]
    public void NoBoxing_Strings()
    {
        for (int i = 0; i < 100_000; i++)
        {
            var test = $"Name {i}";
        }
    }
}
