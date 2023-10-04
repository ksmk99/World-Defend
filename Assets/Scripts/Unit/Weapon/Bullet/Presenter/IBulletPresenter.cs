using UnityEngine;

namespace Unit.Bullet
{
    public interface IBulletPresenter
    {
        bool CheckEnd();
        void Collide(Collider other);
        void Move();
        void Tick();
    }
}