using UnityEngine;

namespace Unit.Bullet
{
    public interface IBulletPresenter
    {
        bool CheckEnd();
        void Collide(GameObject target);
        void Move();
        void Tick();
    }
}