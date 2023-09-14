using BenchmarkDotNet.Attributes;

public class ListBenchmark
{
    private static int N = 100000;

    [Benchmark]
    public void AddToListWithoutCapacity()
    {
        var listWithoutCapacity = new List<int>();
        for (int i = 0; i < N; i++)
        {
            listWithoutCapacity.Add(i);
        }
    }

    [Benchmark]
    public void AddToListWithCapacity()
    {
        var listWithCapacity = new List<int>(N);
        for (int i = 0; i < N; i++)
        {
            listWithCapacity.Add(i);
        }
    }
}