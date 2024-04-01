using System;

namespace Helpers
{
    public interface ICustomPool<T>
    {
        public T Create(int id, T prefab, Func<T, T> funcCreate);

        public void Release(int id, T member);
    }
}
