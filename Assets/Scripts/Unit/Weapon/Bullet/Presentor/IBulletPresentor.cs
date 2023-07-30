using UnityEngine;

namespace Unit.Bullet
{
    public interface IBulletPresentor
    {
        bool CheckEnd();
        void Collide(Collider other);
        void Move();
    }
}
