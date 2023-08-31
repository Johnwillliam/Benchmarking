using BenchmarkDotNet.Attributes;

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

    [Benchmark]
    public void Boxing_Int()
    {
        _objList.Add(42); // Boxing occurs here
    }

    [Benchmark]
    public void NoBoxing_Int()
    {
        _intList.Add(42); // No boxing with List<int>
    }
}
