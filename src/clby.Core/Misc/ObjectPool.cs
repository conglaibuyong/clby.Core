using System;
using System.Collections.Concurrent;

namespace clby.Core.Misc
{
    [Obsolete]
    public sealed class ObjectPool<T> : IDisposable
    {
        private readonly int size;
        private readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
        private Func<T> objectGenerator;
        private Action<T> objectDestructor;

        public ObjectPool(Func<T> objectGenerator, int size = 25, Action<T> objectDestructor = null)
        {
            this.objectGenerator = objectGenerator;
            this.objectDestructor = objectDestructor;
            this.size = size > 0 ? size : 25;
        }
        public void Dispose()
        {
            T t;
            while (queue.Count > 0)
                if (queue.TryDequeue(out t))
                    objectDestructor(t);
        }


        public T Get()
        {
            T ret;
            return queue.Count > 0 && queue.TryDequeue(out ret) ?
                ret :
                objectGenerator();
        }
        public void Put(T item)
        {
            if (queue.Count < size) queue.Enqueue(item);
            else objectDestructor(item);
        }

    }
}
