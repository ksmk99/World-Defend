using System;
using UnityEngine;
using Zenject;

namespace Unit.Bullet
{
    public abstract class ABulletPresenter : ITickable, IBulletPresenter
    {
        protected BulletModel model;
        protected BulletView view;

        public event Func<Collider, Vector3, bool> OnCollide;

        public ABulletPresenter(BulletModel model, BulletView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Reinitialize(BulletRuntimeSettings settings)
        {
            model.Init(settings);

            view.transform.position = model.RuntimeSettings.Position;
            view.transform.rotation = model.RuntimeSettings.Rotation;
        }

        public abstract void Collide(Collider other);

        public void Tick()
        {
            if (CheckEnd())
            {
                Dispose();
            }
            else
            {
                Move();
            }
        }

        public void Move()
        {
            view.transform.position += view.transform.forward * Time.deltaTime * model.Settings.Speed;
        }

        private void Dispose()
        {
            view.Dispose();
        }

        public bool CheckEnd()
        {
            if (!model.CanCollide)
            {
                return true;
            }

            var distance = Vector3.Distance(model.RuntimeSettings.Position, view.transform.position);
            return distance > model.RuntimeSettings.Distance;
        }

        public void SetWeapon(AWeaponPresenter weaponPresenter)
        {
            model.Weapon = weaponPresenter;
        }
    }
}
