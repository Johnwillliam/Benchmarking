using System.Linq.Expressions;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace Benchmarking
{
    public class ReflectionVsExpressionTreesBenchMark
    {
        private List<IFunctionExecutor> _objects;
        private readonly List<Action> _functions = new();

        [GlobalSetup]
        public void Setup()
        {
            _objects = new List<IFunctionExecutor>
            {
                new ObjectA(),
                new ObjectB()
            };

            foreach (var obj in _objects)
            {
                // Using Expression Trees to execute the ExecuteFunction method
                _functions.Add(CreateExecuteFunctionExpression(obj));
            }
        }

        [Benchmark]
        public void RunUsingReflection()
        {
            foreach (var obj in _objects)
            {
                // Using Reflection to execute the ExecuteFunction method
                MethodInfo method = obj.GetType().GetMethod("ExecuteFunction");
                method?.Invoke(obj, null);
            }
        }

        [Benchmark]
        public void RunUsingExpressionTrees()
        {
            foreach(var func in _functions)
            {
                func.Invoke();
            }
        }


        private static Action CreateExecuteFunctionExpression(IFunctionExecutor obj)
        {
            Expression<Action> expr = Expression.Lambda<Action>(
                Expression.Call(
                    Expression.Convert(Expression.Constant(obj), obj.GetType()),
                    "ExecuteFunction",
                    Type.EmptyTypes
                )
            );

            return expr.Compile();
        }
    }

    public interface IFunctionExecutor
    {
        void ExecuteFunction();
    }

    class ObjectA : IFunctionExecutor
    {
        public void ExecuteFunction()
        {
            Console.WriteLine("ObjectA's ExecuteFunction");
        }
    }

    class ObjectB : IFunctionExecutor
    {
        public void ExecuteFunction()
        {
            Console.WriteLine("ObjectB's ExecuteFunction");
        }
    }
}

public class BM
{
    private MethodInfo _mi;
    private Func<int, int> _fp;

    [GlobalSetup]
    public void Setup()
    {
        _mi = typeof(SampleClass).GetMethod("Increment");
        var paramExpr = Expression.Parameter(typeof(int), "arg1");
        Expression callExpr = Expression.Call(typeof(SampleClass).GetMethod("Increment"), paramExpr);
        _fp = Expression.Lambda<Func<int, int>>(callExpr, paramExpr).Compile();
    }

    [Arguments(2), Benchmark]
    public int InvokeUsingReflection(int value)
    {
        return (int)_mi.Invoke(null, new object[] { value });
    }

    [Arguments(2), Benchmark(Baseline = true)]
    public int InvokeUsingExpressionTree(int value)
    {
        return _fp.Invoke(value);
    }
}

public class SampleClass
{
    public static int Increment(int arg1)
    {
        return arg1 + 1;
    }
}
