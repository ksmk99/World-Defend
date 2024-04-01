using System;
using UnityEngine;
using Zenject;

namespace Unit.Bullet
{
    public class BulletPresenter : ITickable, IBulletPresenter
    {
        private BulletModel model;
        private BulletView view;

        public event Func<Collider, Vector3, bool> OnCollide;

        public BulletPresenter(BulletModel model, BulletView view)
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

        public void Collide(Collider other)
        {
            if (!model.CanCollide)
            {
                return;
            }

            var isSuccess = model.Weapon.BulletCollide(other, view.transform.position);
            model.CanCollide = !isSuccess;
            if(isSuccess)
            {
                Debug.Log("Succes " + other);
            }
        }

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

        public void SetWeapon(WeaponPresenter weaponPresenter)
        {
            model.Weapon = weaponPresenter;
        }
    }
}
