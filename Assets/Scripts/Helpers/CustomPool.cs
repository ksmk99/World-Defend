using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public class CustomPool<T> 
          where T : MonoBehaviour
    {
        private readonly T spawnObject;
        private readonly int maxSize;

        private Queue<T> memberPool;

        public CustomPool(T spawnObject, int maxSize)
        {
            if (spawnObject == null)
            {
                throw new ArgumentNullException();
            }

            this.spawnObject = spawnObject;
            this.maxSize = maxSize;

            memberPool = new Queue<T>();
        }

        public void Init(int prespawnCount, Transform parent = null)
        {
            if (prespawnCount > maxSize)
            {
                prespawnCount = maxSize;
            }

            PrespawnObjects(prespawnCount, parent);
        }

        public T Get()
        {
            if (memberPool.Count == 0)
            {
                var member = Create();
                return member;
            }
            else if (memberPool.Count >= maxSize)
            {
                ReleaseMembers(memberPool.Count - maxSize);
            }

            return memberPool.Dequeue();
        }

        public void Release(T member)
        {
            memberPool.Enqueue(member);
            member.enabled = false;
        }

        private void PrespawnObjects(int amount, Transform parent)
        {
            for (int i = 0; i < amount; i++)
            {
                var member = Create();
                memberPool.Enqueue(member);
                member.transform.parent = parent;
                member.enabled = false;
            }
        }

        private T Create()
        {
            var member = GameObject.Instantiate(spawnObject);
            return member;
        }

        private void ReleaseMembers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var entry = memberPool.Dequeue();
                GameObject.Destroy(entry.gameObject);
            }
        }
    }
}
