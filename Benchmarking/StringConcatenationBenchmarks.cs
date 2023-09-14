using System.Text;
using BenchmarkDotNet.Attributes;

namespace Benchmarking
{
    public class StringConcatenationBenchmarks
    {
        [Params(1000)] // Change this value depending on the size of your data
        public int N { get; set; }

        [Benchmark]
        public string ConcatenateWithPlusOperator()
        {
            string result = "";
            for (int i = 0; i < N; i++)
            {
                result += i;
            }
            return result;
        }

        [Benchmark]
        public string ConcatenateWithPlusOperatorAndToString()
        {
            string result = "";
            for (int i = 0; i < N; i++)
            {
                result += i.ToString();
            }
            return result;
        }

        [Benchmark]
        public string ConcatenateWithStringInterpolation()
        {
            string result = "";
            for (int i = 0; i < N; i++)
            {
                result += $"{i}";
            }
            return result;
        }

        [Benchmark]
        public string ConcatenateWithStringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < N; i++)
            {
                sb.Append(i);
            }
            return sb.ToString();
        }

        [Benchmark]
        public string ConcatenateWithLinq()
        {
            string result = "";
            for (int i = 0; i < N; i++)
            {
                result = result.Concat(i.ToString()).ToString();
            }
            return result;
        }

        [Benchmark]
        public string ConcatenateWithStringConcat()
        {
            string result = "";
            for (int i = 0; i < N; i++)
            {
                result = string.Concat(result, i);
            }
            return result;
        }
    }
}
