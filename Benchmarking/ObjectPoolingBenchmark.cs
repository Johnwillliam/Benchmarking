using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.ObjectPool;

namespace Benchmarking
{
    public class PooledObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ObjectPoolingBenchmark
    {
        private ObjectPool<PooledObject> systemPool;

        [GlobalSetup]
        public void Setup()
        {
            systemPool = new DefaultObjectPool<PooledObject>(new DefaultPooledObjectPolicy<PooledObject>());
        }

        [Benchmark]
        public void CreateObjectWithoutPool()
        {
            var obj = new PooledObject
            {
                Id = 1,
                Name = "Object 1"
            };
        }

        [Benchmark]
        public void CreateObjectWithPool()
        {
            var obj = systemPool.Get();
            obj.Id = 1;
            obj.Name = "Object 1";
            systemPool.Return(obj);
        }

        [Benchmark]
        public void CreateObjectLoopedWithoutPool()
        {
            for (int i = 0; i < 100_000; i++)
            {
                var obj = new PooledObject
                {
                    Id = 1,
                    Name = "Object 1"
                };
            }     
        }

        [Benchmark]
        public void CreateObjectLoopedWithPool()
        {
            for (int i = 0; i < 100_000; i++)
            {
                var obj = systemPool.Get();
                obj.Id = 1;
                obj.Name = "Object 1";
                systemPool.Return(obj);
            }   
        }
    }

}
