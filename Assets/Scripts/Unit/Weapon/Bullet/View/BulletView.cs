using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;

namespace Unit.Bullet
{
    public class BulletView : MonoBehaviour,
        IBulletView,
        IPoolable<BulletRuntimeSettings, IMemoryPool>,
        IDisposable
    {
        public float Speed;

        private IMemoryPool pool;

        private BulletRuntimeSettings settings;
        private bool canCollide;

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerView>(out var view))
            {
                var isSuccess = view.TryAddEffects(settings.Effects, settings.Team);
                canCollide = !isSuccess;
            }
        }

        public bool CheckEnd()
        {
            var distance = Vector3.Distance(settings.Position, transform.position);
            return distance > settings.Distance && canCollide;
        }

        public void Move()
        {
            transform.position += transform.forward * Time.deltaTime * Speed;
        }

        public void OnSpawned(BulletRuntimeSettings p1, IMemoryPool pool)
        {
            this.settings = p1;
            this.pool = pool;

            transform.position = p1.Position;
            transform.rotation = p1.Rotation;

            gameObject.SetActive(true);
        }

        public void OnDespawned()
        {
            pool = null;
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<BulletRuntimeSettings, BulletView>
        {
        }
    }
}