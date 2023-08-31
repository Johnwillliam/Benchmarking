using BenchmarkDotNet.Attributes;

public class FieldsVsPropertiesBenchmark
{
    private int _field;
    private int Property { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _field = 0;
        Property = 0;
    }

    [Benchmark]
    public void AccessField()
    {
        int value = _field;
    }

    [Benchmark]
    public void AccessProperty()
    {
        int value = Property;
    }

    [Benchmark]
    public void SetField()
    {
        _field = 42;
    }

    [Benchmark]
    public void SetProperty()
    {
        Property = 42;
    }
}
