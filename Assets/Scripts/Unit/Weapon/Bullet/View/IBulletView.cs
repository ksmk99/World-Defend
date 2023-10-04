using UnityEngine;

namespace Unit.Bullet
{
    public interface IBulletView
    {
        void OnTriggerEnter(Collider other);
    }
}