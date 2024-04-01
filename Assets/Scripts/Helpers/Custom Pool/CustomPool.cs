using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class CustomPool<T> : ICustomPool<T>
        where T : MonoBehaviour
    {
        private Dictionary<int, Queue<T>> memberPool = new Dictionary<int, Queue<T>>();

        public T Create(int id, T prefab, Func<T, T> funcCreate)
        {
            if (!memberPool.ContainsKey(id))
            {
                memberPool.Add(id, new Queue<T>());
            }

            if (memberPool[id].Count == 0)
            {
                T member = funcCreate.Invoke(prefab);
                return member;
            }

            return memberPool[id].Dequeue();
        }

        public void Release(int id, T member)
        {
            if (memberPool[id].Contains(member))
            {
                return;
            }

            memberPool[id].Enqueue(member);
        }
    }
}
