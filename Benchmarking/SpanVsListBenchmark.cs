using BenchmarkDotNet.Attributes;

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

    //[Benchmark]
    public int SumUsingList()
    {
        int sum = 0;
        for (int i = 0; i < _dataList.Count; i++)
        {
            sum += _dataList[i];
        }
        return sum;
    }

    //[Benchmark]
    public int SumUsingSpanIterate()
    {
        int sum = 0;
        foreach (var value in _dataArray.AsSpan())
        {
            sum += value;
        }
        return sum;
    }

    //[Benchmark]
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