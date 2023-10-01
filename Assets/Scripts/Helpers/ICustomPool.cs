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
        void Init(int prespawnCount, Transform parent = null);
        T Get();
        void Release(T member);
    }
}
