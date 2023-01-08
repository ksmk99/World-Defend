using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.Bullet
{
    public interface IBulletView
    {
        void Move();
        void OnTriggerEnter(Collider other);
    }
}