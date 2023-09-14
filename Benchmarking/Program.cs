using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Diagnosers;
using System.Diagnostics;
using Benchmarking;
using BenchmarkDotNet.Attributes;

public class Program
{
    static void Main(string[] args)
    {
        BenchMarkDotNetMeasurement();

        //var boxingBenchMark = new BoxingBenchmark();
        //boxingBenchMark.Setup();
        //DateTimeMeasurement(boxingBenchMark.Boxing_Int);
        //DateTimeMeasurement(boxingBenchMark.NoBoxing_Int);

        //StopWatchMeasurement(boxingBenchMark.Boxing_Int);
        //StopWatchMeasurement(boxingBenchMark.NoBoxing_Int);

        //TimeSpanMeasurement(boxingBenchMark.Boxing_Int);
        //TimeSpanMeasurement(boxingBenchMark.NoBoxing_Int);
    }

    private static void BenchMarkDotNetMeasurement()
    {
        var config = DefaultConfig.Instance
                    .AddJob(Job
                         .MediumRun
                         .WithLaunchCount(1)
                         .WithToolchain(InProcessEmitToolchain.Instance))
                    .AddDiagnoser(MemoryDiagnoser.Default)
                    .WithOptions(ConfigOptions.DisableOptimizationsValidator);
        //BenchmarkRunner.Run<Benchmarks>(config);
        //BenchmarkRunner.Run<StringConcatenationBenchmarks>(config);
        //BenchmarkRunner.Run<BoxingBenchmark>(config);
        //BenchmarkRunner.Run<ReflectionVsExpressionTreesBenchMark>(config);
        //BenchmarkRunner.Run<StackAllocPerfTest>();
        //BenchmarkRunner.Run<FieldsVsPropertiesBenchmark>();
        //BenchmarkRunner.Run<SpanVsListBenchmark>();
        //BenchmarkRunner.Run<ObjectPoolingBenchmark>(config);
        BenchmarkRunner.Run<ListBenchmark>(config);
    }

    private static void DateTimeMeasurement(Action function)
    {
        DateTime startTime = DateTime.Now;

        // Code to measure
        function.Invoke();

        DateTime endTime = DateTime.Now;
        TimeSpan duration = endTime - startTime;

        Console.WriteLine($"Time taken: {duration.TotalMilliseconds} ms");
    }

    private static void StopWatchMeasurement(Action function)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Code to measure
        function.Invoke();

        stopwatch.Stop();
        TimeSpan duration = stopwatch.Elapsed;

        Console.WriteLine($"Time taken: {duration.TotalMilliseconds} ms");
    }

    private static void TimeSpanMeasurement(Action function)
    {
        TimeSpan duration;
        DateTime startTime = DateTime.Now;

        // Code to measure
        function.Invoke();

        DateTime endTime = DateTime.Now;
        duration = endTime - startTime;

        Console.WriteLine($"Time taken: {duration.TotalMilliseconds} ms");
    }
}