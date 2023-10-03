using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public interface ICustomPool<T>
    {
        public T Create(int id, T prefab, Func<T, T> funcCreate);

        public void Release(int id, T member);
    }
}
